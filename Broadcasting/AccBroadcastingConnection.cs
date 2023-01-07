using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Acc.Lib.Broadcasting.Messages;

namespace Acc.Lib.Broadcasting;

public class AccBroadcastingConnection
{
    private readonly AccBroadcastingMessageHandler broadcastingMessageHandler;
    private readonly CompositeDisposable subscriptionSink = new();

    private IDisposable connectionStateSubscription;
    private IDisposable dispatchedMessagesSubscription;
    private IPEndPoint ipEndPoint;
    private bool isConnected;
    private bool isDisposed;
    private bool isStopped;
    private IDisposable messageProcessingSubscription;
    private UdpClient udpClient;

    public AccBroadcastingConnection(string ipAddress,
                                     int port,
                                     string displayName,
                                     string connectionPassword,
                                     string commandPassword,
                                     int updateInterval)
    {
        this.IpAddress = ipAddress;
        this.Port = port;
        this.DisplayName = displayName;
        this.ConnectionPassword = connectionPassword;
        this.CommandPassword = commandPassword;
        this.UpdateInterval = updateInterval;
        this.ConnectionIdentifier = $"{this.IpAddress}:{this.Port}";
        this.ipEndPoint = IPEndPoint.Parse(this.ConnectionIdentifier);

        this.broadcastingMessageHandler =
            new AccBroadcastingMessageHandler(this.ConnectionIdentifier);
    }

    public IObservable<BroadcastingEvent> BroadcastingEvents =>
        this.broadcastingMessageHandler.BroadcastingEvents;
    public string CommandPassword { get; }
    public string ConnectionIdentifier { get; }
    public string ConnectionPassword { get; }
    public string DisplayName { get; }
    public IObservable<EntryListUpdate> EntryListUpdates =>
        this.broadcastingMessageHandler.EntryListUpdates;
    public string IpAddress { get; }
    public IObservable<string> LogMessages => this.broadcastingMessageHandler.LogMessages;
    public int Port { get; }
    public IObservable<RealtimeCarUpdate> RealTimeCarUpdates =>
        this.broadcastingMessageHandler.RealTimeCarUpdates;
    public IObservable<RealtimeUpdate> RealTimeUpdates =>
        this.broadcastingMessageHandler.RealTimeUpdates;
    public IObservable<TrackDataUpdate> TrackDataUpdates =>
        this.broadcastingMessageHandler.TrackDataUpdates;
    public int UpdateInterval { get; }

    public void Connect()
    {
        this.subscriptionSink.Add(this.broadcastingMessageHandler.ConnectionStateChanges
                                      .Subscribe(this.OnNextConnectionStateChange));
        this.udpClient = new UdpClient();
        this.udpClient.Client.ReceiveTimeout = 5000;

        Observable.Interval(TimeSpan.FromSeconds(2))
                  .TakeWhile(p => !this.isStopped && !this.isConnected)
                  .Subscribe(this.OnNextConnectionRetry, this.OnConnectionError, this.OnConnectCompleted);
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
        this.Dispose(true);
    }

    public void Stop()
    {
        this.isStopped = true;
        if(this.isConnected)
        {
            this.ShutdownAsync();
        }
    }

    protected virtual void Dispose(bool disposing)
    {
        if(this.isDisposed)
        {
            return;
        }

        if(disposing)
        {
            try
            {
                this.ShutdownAsync();
            }
            catch(Exception exception)
            {
                this.LogMessage(exception.Message);
                Debug.WriteLine(exception);
            }
        }

        this.isDisposed = true;
    }

    private void LogMessage(string message)
    {
        this.broadcastingMessageHandler.LogMessage(message);
    }

    private void OnConnectCompleted()
    {
        if(!this.isConnected)
        {
            return;
        }

        this.subscriptionSink.Add(this.broadcastingMessageHandler.DispatchedMessages
                                      .Subscribe(this.OnNextDispatchedMessage));

        this.broadcastingMessageHandler.RequestConnection(this.DisplayName,
                                                          this.ConnectionPassword,
                                                          this.UpdateInterval,
                                                          this.CommandPassword);
        this.subscriptionSink.Add(Observable
                                             .Using(() =>
                                                        new UdpClient(new IPEndPoint(IPAddress.Any,
                                                         20000)),
                                                    udpc => Observable
                                                            .Defer(() =>
                                                                Observable
                                                                    .FromAsync(udpc
                                                                        .ReceiveAsync))
                                                            .Repeat())
                                             .TakeWhile(p => !this.isStopped && this.isConnected)
                                             .Subscribe(this.OnNextUdpReceiveResult,
                                                        this.OnUdpClientError));
    }

    private void OnConnectionError(Exception exception)
    {
        this.LogMessage($"Unexpected error trying to connect to {this.IpAddress}:{this.Port}: {exception.Message}");
    }

    private void OnNextConnectionRetry(long interval)
    {
        this.udpClient.Connect(this.IpAddress, this.Port);

        try
        {
            this.LogMessage("Attempting connection to ACC...");
            var tmp = new byte[1];
            this.udpClient.Client.Send(tmp);
            this.udpClient.Receive(ref this.ipEndPoint);
            this.isConnected = this.udpClient.Client.Connected;
        }
        catch(SocketException socketException)
        {
            if(socketException.ErrorCode == 10054)
            {
                this.LogMessage($"ACC not found at {this.ConnectionIdentifier}, retrying...");
            }
            else
            {
                this.LogMessage($"A connection to {this.ConnectionIdentifier} was made but timed out.");
                throw;
            }
        }
    }

    private void OnNextConnectionStateChange(ConnectionState connectionState)
    {
        this.isConnected = connectionState.IsConnected;
        this.LogMessage($"Connection State Changed: {connectionState}");
    }

    private void OnNextDispatchedMessage(byte[] message)
    {
        this.udpClient?.Send(message);
    }

    private void OnNextUdpReceiveResult(UdpReceiveResult receiveResult)
    {
        using var stream = new MemoryStream(receiveResult.Buffer);
        using var reader = new BinaryReader(stream);
        this.broadcastingMessageHandler.ProcessMessage(reader);
    }

    private void OnUdpClientError(Exception exception)
    {
        this.LogMessage(exception.Message);
        Debug.WriteLine(exception);
    }

    private void ShutdownAsync()
    {
        this.broadcastingMessageHandler.Disconnect();
        this.subscriptionSink?.Dispose();
        this.udpClient?.Close();
        this.udpClient?.Dispose();
        this.udpClient = null;
    }
}