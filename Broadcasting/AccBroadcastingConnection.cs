using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Reactive.Disposables;
using Acc.Lib.Broadcasting.Messages;

namespace Acc.Lib.Broadcasting;

public class AccBroadcastingConnection
{
    private readonly AccBroadcastingMessageHandler broadcastingMessageHandler;
    private readonly CompositeDisposable subscriptionSink = new();

    private IPEndPoint ipEndPoint;
    private bool isConnected;
    private bool isDisposed;
    private bool isStopped;
    private Task listenerTask;
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

    public Task Connect()
    {
        this.subscriptionSink.Add(this.broadcastingMessageHandler.ConnectionStateChanges
                                      .Subscribe(this.OnNextConnectionStateChange));
        this.udpClient = new UdpClient();
        this.udpClient.Client.ReceiveTimeout = 5000;

        try
        {
            this.WaitForConnection();
            this.listenerTask = this.HandleMessages();
            return this.listenerTask;
        }
        catch(Exception exception)
        {
            this.LogMessage(exception.Message);
            Debug.WriteLine(exception.Message);
            throw;
        }
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
            this.Shutdown();
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
                this.Shutdown();
            }
            catch(Exception exception)
            {
                this.LogMessage(exception.Message);
                Debug.WriteLine(exception);
            }
        }

        this.isDisposed = true;
    }

    private async Task HandleMessages()
    {
        this.broadcastingMessageHandler.RequestConnection(this.DisplayName,
                                                          this.ConnectionPassword,
                                                          this.UpdateInterval,
                                                          this.CommandPassword);
        while(!this.isStopped && this.isConnected)
        {
            try
            {
                await this.ProcessNextMessage();
            }
            catch(ObjectDisposedException)
            {
                break;
            }
            catch(Exception exception)
            {
                this.LogMessage(exception.Message);
                Debug.WriteLine(exception);
            }
        }
    }

    private void LogMessage(string message)
    {
        this.broadcastingMessageHandler.LogMessage(message);
    }

    private void OnNextConnectionStateChange(ConnectionState connectionState)
    {
        this.isConnected = connectionState.IsConnected;
        this.LogMessage($"Connection State Changed: {connectionState}");
    }

    private async Task ProcessNextMessage()
    {
        var udpReceiveResult = await this.udpClient.ReceiveAsync();
        await using var stream = new MemoryStream(udpReceiveResult.Buffer);
        using var reader = new BinaryReader(stream);
        this.broadcastingMessageHandler.ProcessMessage(reader);
    }

    private void Shutdown()
    {
        this.broadcastingMessageHandler.Disconnect();
        this.subscriptionSink?.Dispose();
        this.udpClient?.Close();
        this.udpClient?.Dispose();
        this.udpClient = null;
    }

    private async void WaitForConnection()
    {
        this.udpClient.Connect(this.ipEndPoint);

        while(!this.isStopped && !this.isConnected)
        {
            try
            {
                this.LogMessage("Attempting connection to ACC...");
                var tmp = new byte[1];
                this.udpClient.Client.Send(tmp);
                this.udpClient.Receive(ref this.ipEndPoint);
                this.isConnected = this.udpClient.Client.Connected;

                await Task.Delay(TimeSpan.FromSeconds(3));
            }
            catch(SocketException socketException)
            {
                Debug.WriteLine($"SocketException {socketException.ErrorCode} {socketException.Message}");
                this.LogMessage(socketException.ErrorCode == 10054
                                    ? $"ACC not found at {this.ConnectionIdentifier}, retrying..."
                                    : $"A connection to {this.ConnectionIdentifier} was made but timed out.");
            }
            catch(Exception exception)
            {
                this.LogMessage($"Unexpected error: {exception.Message}");
            }
        }
    }
}