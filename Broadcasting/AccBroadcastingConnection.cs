using System;
using System.Data;
using System.Diagnostics;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Reactive.Disposables;
using Acc.Lib.Broadcasting.Messages;
using ConnectionState = Acc.Lib.Broadcasting.Messages.ConnectionState;

namespace Acc.Lib.Broadcasting;

public class AccBroadcastingConnection
{
	private readonly AccBroadcastingMessageHandler broadcastingMessageHandler;
	private readonly IPEndPoint ipEndPoint;
	private readonly CompositeDisposable subscriptionSink = new();

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
	public IObservable<ConnectionState> ConnectionStateChanges =>
		this.broadcastingMessageHandler.ConnectionStateChanges;
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

	public async void Connect()
	{
		this.subscriptionSink.Add(this.broadcastingMessageHandler.ConnectionStateChanges
		                              .Subscribe(this.OnNextConnectionStateChange));
		this.subscriptionSink.Add(this.broadcastingMessageHandler.DispatchedMessages
		                              .Subscribe(this.OnNextDispatchedMessage));

		this.udpClient = new UdpClient();
		this.udpClient.Client.ReceiveTimeout = 5000;
		this.udpClient.Connect(this.ipEndPoint);

		try
		{
			await this.WaitForPortToBeAvailable();
			this.listenerTask = this.HandleMessages();
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
		this.LogMessage("Connecting to ACC...");
		this.broadcastingMessageHandler.RequestConnection(this.DisplayName,
		                                                  this.ConnectionPassword,
		                                                  this.UpdateInterval,
		                                                  this.CommandPassword);

		this.isConnected = true;
		this.LogMessage("Connected to ACC Session...");

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

	private void OnNextDispatchedMessage(byte[] message)
	{
		this.udpClient?.Send(message);
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
        this.LogMessage("Disconnecting from ACC Broadcasting API...");
        this.broadcastingMessageHandler.Disconnect();
		this.subscriptionSink?.Dispose();
		this.udpClient?.Close();
		this.udpClient?.Dispose();
		this.udpClient = null;
	}

	private async Task WaitForPortToBeAvailable()
	{
		this.LogMessage("Waiting for ACC session to start...");
		var isPortAvailable = false;
		while(!isPortAvailable)
		{
			isPortAvailable = IPGlobalProperties.GetIPGlobalProperties()
			                                    .GetActiveUdpListeners()
			                                    .Any(p => p.Port == this.Port);
			await Task.Delay(TimeSpan.FromSeconds(10));
		}
	}
}