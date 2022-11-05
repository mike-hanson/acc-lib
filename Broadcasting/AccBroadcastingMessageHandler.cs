using System;
using System.Diagnostics;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Acc.Lib.Broadcasting.Messages;

namespace Acc.Lib.Broadcasting;

public class AccBroadcastingMessageHandler
{
    public const int BroadcastingProtocolVersion = 4;

    private readonly Subject<BroadcastingEvent> broadcastingEventSubject = new();
    private readonly Subject<ConnectionState> connectionStateChangeSubject = new();
    private readonly IList<CarInfo> entryList = new List<CarInfo>();
    private readonly Subject<EntryListUpdate> entryListUpdateSubject = new();
    private readonly Action<string> logger;
    private readonly Subject<RealtimeCarUpdate> realTimeCarUpdateSubject = new();
    private readonly Subject<RealtimeUpdate> realTimeUpdateSubject = new();
    private readonly Subject<TrackDataUpdate> trackDataUpdateSubject = new();
    private DateTime lastEntryListRequest = DateTime.UtcNow;

    public AccBroadcastingMessageHandler(string connectionIdentifier,
        Action<byte[]> sendMessageHandler,
        Action<string> logger)
    {
        if(string.IsNullOrEmpty(connectionIdentifier))
        {
            throw new ArgumentException(
                "No connection identifier provided.  A unique identifier is required for managing connections. IP Address and Port is a good identifier");
        }

        this.logger = logger;

        this.ConnectionIdentifier = connectionIdentifier;
        this.SendMessageHandler = sendMessageHandler ?? throw new ArgumentException(
                                      "A callback for sending messages must be provided.  This handler does not deal with network actions.");
    }

    public IObservable<BroadcastingEvent> BroadcastingEvents =>
        this.broadcastingEventSubject.AsObservable();
    public string ConnectionIdentifier { get; }
    public IObservable<ConnectionState> ConnectionStateChanges =>
        this.connectionStateChangeSubject.AsObservable();
    public IObservable<EntryListUpdate> EntryListUpdates =>
        this.entryListUpdateSubject.AsObservable();
    public IObservable<RealtimeCarUpdate> RealTimeCarUpdates =>
        this.realTimeCarUpdateSubject.AsObservable();
    public IObservable<RealtimeUpdate> RealTimeUpdates => this.realTimeUpdateSubject.AsObservable();
    public IObservable<TrackDataUpdate> TrackDataUpdates =>
        this.trackDataUpdateSubject.AsObservable();
    public Action<byte[]> SendMessageHandler { get; set; }
    private int ConnectionId { get; set; }

    public void Disconnect()
    {
        using var stream = new MemoryStream();
        using var writer = new BinaryWriter(stream);
        writer.Write((byte)OutboundMessageTypes.UnregisterCommandApplication);
        this.SendMessageHandler(stream.ToArray());
    }

    public void ProcessMessage(BinaryReader reader)
    {
        var messageType = (InboundMessageType)reader.ReadByte();
        switch(messageType)
        {
            case InboundMessageType.BroadcastingEvent:
                this.ProcessBroadCastingEventMessage(reader);
                break;
            case InboundMessageType.EntryList:
                this.ProcessEntryListMessage(reader);
                break;
            case InboundMessageType.EntryListCar:
                this.ProcessEntryListCarMessage(reader);
                break;
            case InboundMessageType.RealtimeUpdate:
                this.ProcessRealtimeUpdateMessage(reader);
                break;
            case InboundMessageType.RealtimeCarUpdate:
                this.ProcessRealtimeCarUpdateMessage(reader);
                break;
            case InboundMessageType.RegistrationResult:
                this.ProcessRegistrationResultMessage(reader);
                break;
            case InboundMessageType.TrackData:
                this.ProcessTrackDataMessage(reader);
                break;
            default:
                this.LogMessage("Unknown message type");
                break;
        }
    }

    public void RequestHUDPage(string hudPage)
    {
        using var stream = new MemoryStream();
        using var writer = new BinaryWriter(stream);
        writer.Write((byte)OutboundMessageTypes.ChangeHudPage);
        writer.Write(this.ConnectionId);
        writer.WriteString(hudPage);

        this.SendMessageHandler(stream.ToArray());
    }

    public void RequestInstantReplay(float startSessionTime,
        float durationMS,
        int initialFocusedCarIndex = -1,
        string initialCameraSet = "",
        string initialCamera = "")
    {
        using var ms = new MemoryStream();
        using var bw = new BinaryWriter(ms);
        bw.Write((byte)OutboundMessageTypes.InstantReplayRequest);
        bw.Write(this.ConnectionId);

        bw.Write(startSessionTime);
        bw.Write(durationMS);
        bw.Write(initialFocusedCarIndex);

        bw.WriteString(initialCameraSet);
        bw.WriteString(initialCamera);

        this.SendMessageHandler(ms.ToArray());
    }

    public void SetCamera(string cameraSet, string camera)
    {
        this.SetFocusInternal(null, cameraSet, camera);
    }

    public void SetFocus(ushort carIndex)
    {
        this.SetFocusInternal(carIndex, null, null);
    }

    public void SetFocus(ushort carIndex, string cameraSet, string camera)
    {
        this.SetFocusInternal(carIndex, cameraSet, camera);
    }

    internal void RequestConnection(string displayName,
        string connectionPassword,
        int updateInterval,
        string commandPassword)
    {
        using var stream = new MemoryStream();
        using var writer = new BinaryWriter(stream);
        writer.Write((byte)OutboundMessageTypes.RegisterCommandApplication);
        writer.Write((byte)BroadcastingProtocolVersion);

        writer.WriteString(displayName);
        writer.WriteString(connectionPassword ?? string.Empty);
        writer.Write(updateInterval);
        writer.WriteString(commandPassword ?? string.Empty);

        this.SendMessageHandler(stream.ToArray());
    }

    private void LogMessage(string message)
    {
        this.logger?.Invoke(message);
    }

    private void ProcessBroadCastingEventMessage(BinaryReader binaryReader)
    {
        var eventData = binaryReader.ReadBroadcastingEvent();
        eventData.CarData = this.entryList.FirstOrDefault(e => e.CarIndex == eventData.CarId)!;
        this.broadcastingEventSubject.OnNext(eventData);
    }

    private void ProcessEntryListCarMessage(BinaryReader reader)
    {
        var carId = reader.ReadUInt16();

        var carInfo = this.entryList.SingleOrDefault(x => x.CarIndex == carId);
        if(carInfo == null)
        {
            Debug.WriteLine($"Entry list update for unknown carIndex {carId}");
            return;
        }

        reader.UpdateCarInfo(carInfo);

        this.entryListUpdateSubject.OnNext(new EntryListUpdate(this.ConnectionIdentifier, carInfo));
    }

    private void ProcessEntryListMessage(BinaryReader reader)
    {
        this.entryList.Clear();

        var connectionId = reader.ReadInt32();
        var carEntryCount = reader.ReadUInt16();
        for(var i = 0; i < carEntryCount; i++)
        {
            this.entryList.Add(new CarInfo(reader.ReadUInt16()));
        }
    }

    private void ProcessRealtimeCarUpdateMessage(BinaryReader reader)
    {
        if(!this.entryList.Any())
        {
            return;
        }

        var update = reader.ReadRealtimeCarUpdate();
        var carEntry = this.entryList.FirstOrDefault(x => x.CarIndex == update.CarIndex);
        if(carEntry == null || carEntry.Drivers.Count != update.DriverCount)
        {
            if(!((DateTime.Now - this.lastEntryListRequest).TotalSeconds > 1))
            {
                return;
            }

            this.lastEntryListRequest = DateTime.Now;
            this.RequestEntryList();
            Debug.WriteLine(
                $"CarUpdate {update.CarIndex}|{update.DriverIndex} not known, will ask for new EntryList");
        }
        else
        {
            this.realTimeCarUpdateSubject.OnNext(update);
        }
    }

    private void ProcessRealtimeUpdateMessage(BinaryReader reader)
    {
        var update = reader.ReadRealtimeUpdate();
        this.realTimeUpdateSubject.OnNext(update);
    }

    private void ProcessRegistrationResultMessage(BinaryReader reader)
    {
        this.ConnectionId = reader.ReadInt32();
        var connectionSuccess = reader.ReadByte() > 0;
        var isReadonly = reader.ReadByte() == 0;
        var errMsg = reader.ReadString();

        this.connectionStateChangeSubject.OnNext(new ConnectionState(this.ConnectionId,
            connectionSuccess,
            isReadonly,
            errMsg));

        if(!connectionSuccess)
        {
            return;
        }

        this.RequestEntryList();
        this.RequestTrackData();
    }

    private void ProcessTrackDataMessage(BinaryReader reader)
    {
        var connectionId = reader.ReadInt32();
        var update = reader.ReadTrackDataUpdate(this.ConnectionIdentifier);
        this.trackDataUpdateSubject.OnNext(update);
    }

    private void RequestEntryList()
    {
        using var stream = new MemoryStream();
        using var writer = new BinaryWriter(stream);
        writer.Write((byte)OutboundMessageTypes.RequestEntryList);
        writer.Write(this.ConnectionId);

        this.SendMessageHandler(stream.ToArray());
    }

    private void RequestTrackData()
    {
        using var stream = new MemoryStream();
        using var writer = new BinaryWriter(stream);
        writer.Write((byte)OutboundMessageTypes.RequestTrackData);
        writer.Write(this.ConnectionId);

        this.SendMessageHandler(stream.ToArray());
    }

    private void SetFocusInternal(ushort? carIndex, string cameraSet, string camera)
    {
        using var stream = new MemoryStream();
        using var writer = new BinaryWriter(stream);
        writer.Write((byte)OutboundMessageTypes.ChangeFocus);
        writer.Write(this.ConnectionId);

        if(!carIndex.HasValue)
        {
            writer.Write((byte)0);
        }
        else
        {
            writer.Write((byte)1);
            writer.Write(carIndex.Value);
        }

        if(string.IsNullOrEmpty(cameraSet) || string.IsNullOrEmpty(camera))
        {
            writer.Write((byte)0);
        }
        else
        {
            writer.Write((byte)1);
            writer.WriteString(cameraSet);
            writer.WriteString(camera);
        }

        this.SendMessageHandler(stream.ToArray());
    }
}