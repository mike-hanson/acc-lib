using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using Acc.Lib.Messages;

namespace Acc.Lib;

public class AccConnection
{
  private readonly Action<string> logger;
  private readonly AccMessageHandler messageHandler;
  private IDisposable connectionStateSubscription;
  private bool isConnected;
  private bool isDisposed;
  private Task listenerTask;
  private UdpClient udpClient;
  private IPEndPoint ipEndPoint;

  public AccConnection(string ipAddress,
    int port,
    string displayName,
    string connectionPassword,
    string commandPassword,
    int updateInterval,
    Action<string> logger)
  {
    this.logger = logger;
    this.IpAddress = ipAddress;
    this.Port = port;
    this.DisplayName = displayName;
    this.ConnectionPassword = connectionPassword;
    this.CommandPassword = commandPassword;
    this.UpdateInterval = updateInterval;
    this.ConnectionIdentifier = $"{this.IpAddress}:{this.Port}";
    this.ipEndPoint = IPEndPoint.Parse(this.ConnectionIdentifier);

    this.messageHandler = new AccMessageHandler(this.ConnectionIdentifier, this.Send, logger);
  }

  public IObservable<BroadcastingEvent> BroadcastingEvents =>
    this.messageHandler.BroadcastingEvents;

  public string CommandPassword { get; }
  public string ConnectionIdentifier { get; }
  public string ConnectionPassword { get; }
  public string DisplayName { get; }
  public IObservable<EntryListUpdate> EntryListUpdates => this.messageHandler.EntryListUpdates;
  public string IpAddress { get; }
  public int Port { get; }
  public IObservable<RealtimeCarUpdate> RealTimeCarUpdates =>
    this.messageHandler.RealTimeCarUpdates;
  public IObservable<RealtimeUpdate> RealTimeUpdates => this.messageHandler.RealTimeUpdates;
  public IObservable<TrackDataUpdate> TrackDataUpdates => this.messageHandler.TrackDataUpdates;
  public int UpdateInterval { get; }

  public Task Connect()
  {
    this.connectionStateSubscription =
      this.messageHandler.ConnectionStateChanges.Subscribe(this.HandleConnectionStateChange);
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
      Debug.WriteLine(exception);
      throw;
    }
  }

  // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
  // ~ACCUdpRemoteClient() {
  //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
  //   Dispose(false);
  // }

  public void Dispose()
  {
    this.connectionStateSubscription.Dispose();
    this.Dispose(true);
    // GC.SuppressFinalize(this);
  }

  public async Task ShutdownAsync()
  {
    if(this.listenerTask is { IsCompleted: false })
    {
      this.messageHandler.Disconnect();
      this.udpClient?.Close();
      this.udpClient = null;
      await this.listenerTask;
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
        if(this.udpClient != null)
        {
          this.udpClient.Close();
          this.udpClient.Dispose();
          this.udpClient = null;
        }
      }
      catch(Exception exception)
      {
        this.LogMessage(exception.Message);
        Debug.WriteLine(exception);
      }
    }

    // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
    // TODO: set large fields to null.

    this.isDisposed = true;
  }

  private void HandleConnectionStateChange(ConnectionState connectionState)
  {
    this.isConnected = connectionState.IsConnected;
    this.LogMessage($"Connection State Changed: {connectionState}");
  }

  private async Task HandleMessages()
  {
    this.messageHandler.RequestConnection(this.DisplayName,
      this.ConnectionPassword,
      this.UpdateInterval,
      this.CommandPassword);
    while(this.udpClient != null)
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
    this.logger?.Invoke(message);
  }

  private async Task ProcessNextMessage()
  {
    var udpPacket = await this.udpClient.ReceiveAsync();
    await using var stream = new MemoryStream(udpPacket.Buffer);
    using var reader = new BinaryReader(stream);
    this.messageHandler.ProcessMessage(reader);
  }

  private void Send(byte[] payload)
  {
    this.udpClient.Send(payload);
  }

  private void WaitForConnection()
  {
    try
    {
      this.udpClient.Connect(this.IpAddress, this.Port);
    }
    catch(Exception exception)
    {
      this.LogMessage(
        $"Unexpected error trying to connect to {this.IpAddress}:{this.Port}: {exception.Message}");
      return;
    }

    while(!this.isConnected)
    {
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
          this.isConnected = true;
        }
      }
      catch(Exception exception)
      {
        this.LogMessage($"Unexpected error: {exception.Message}");
      }
    }
  }
}