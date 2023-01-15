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
    private readonly Subject<byte[]> dispatchedMessagesSubject = new();
    private readonly IList<CarInfo> entryList = new List<CarInfo>();
    private readonly Subject<EntryListUpdate> entryListUpdateSubject = new();
    private readonly Subject<string> logMessagesSubject = new();
    private readonly Subject<RealtimeCarUpdate> realTimeCarUpdateSubject = new();
    private readonly Subject<RealtimeUpdate> realTimeUpdateSubject = new();
    private readonly Subject<TrackDataUpdate> trackDataUpdateSubject = new();

    private DateTime lastEntryListRequest = DateTime.UtcNow;

    public AccBroadcastingMessageHandler(string connectionIdentifier)
    {
        if(string.IsNullOrEmpty(connectionIdentifier))
        {
            throw new
                ArgumentException("No connection identifier provided.  A unique identifier is required for managing connections. IP Address and Port is a good identifier");
        }

        this.ConnectionIdentifier = connectionIdentifier;
    }

    public IObservable<BroadcastingEvent> BroadcastingEvents =>
        this.broadcastingEventSubject.AsObservable();
    public string ConnectionIdentifier { get; }
    public IObservable<ConnectionState> ConnectionStateChanges =>
        this.connectionStateChangeSubject.AsObservable();
    public IObservable<byte[]> DispatchedMessages => this.dispatchedMessagesSubject.AsObservable();
    public IObservable<EntryListUpdate> EntryListUpdates =>
        this.entryListUpdateSubject.AsObservable();
    public IObservable<string> LogMessages => this.logMessagesSubject.AsObservable();
    public IObservable<RealtimeCarUpdate> RealTimeCarUpdates =>
        this.realTimeCarUpdateSubject.AsObservable();
    public IObservable<RealtimeUpdate> RealTimeUpdates => this.realTimeUpdateSubject.AsObservable();
    public IObservable<TrackDataUpdate> TrackDataUpdates =>
        this.trackDataUpdateSubject.AsObservable();

    private int ConnectionId { get; set; }

    public void Disconnect()
    {
        using var stream = new MemoryStream();
        using var writer = new BinaryWriter(stream);
        writer.Write((byte)OutboundMessageTypes.UnregisterCommandApplication);
        this.DispatchMessage(stream.ToArray());
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

        this.DispatchMessage(stream.ToArray());
    }

    public void RequestInstantReplay(float startSessionTime,
                                     float durationMS,
                                     int initialFocusedCarIndex = -1,
                                     string initialCameraSet = "",
                                     string initialCamera = "")
    {
        using var stream = new MemoryStream();
        using var writer = new BinaryWriter(stream);
        writer.Write((byte)OutboundMessageTypes.InstantReplayRequest);
        writer.Write(this.ConnectionId);

        writer.Write(startSessionTime);
        writer.Write(durationMS);
        writer.Write(initialFocusedCarIndex);

        writer.WriteString(initialCameraSet);
        writer.WriteString(initialCamera);

        this.DispatchMessage(stream.ToArray());
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

    internal void LogMessage(string message)
    {
        this.logMessagesSubject.OnNext(message);
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

        this.DispatchMessage(stream.ToArray());
    }

    private void DispatchMessage(byte[] message)
    {
        this.dispatchedMessagesSubject.OnNext(message);
    }

    private void ProcessBroadCastingEventMessage(BinaryReader binaryReader)
    {
        var eventData = binaryReader.ReadBroadcastingEvent();
        eventData.CarData = this.entryList.FirstOrDefault(e => e.CarIndex == eventData.CarId)!;
        this.broadcastingEventSubject.OnNext(eventData);
        Debug.WriteLine(eventData.ToString());
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

        var update = new EntryListUpdate(this.ConnectionIdentifier, carInfo);
        this.entryListUpdateSubject.OnNext(update);
        Debug.WriteLine(update.ToString());
    }

    private void ProcessEntryListMessage(BinaryReader reader)
    {
        this.entryList.Clear();

        var connectionId = reader.ReadInt32();
        var carEntryCount = reader.ReadUInt16();
        for(var i = 0; i < carEntryCount; i++)
        {
            var carInfo = new CarInfo(reader.ReadUInt16());
            this.entryList.Add(carInfo);
            Debug.WriteLine(carInfo.ToString());
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
            Debug.WriteLine($"Car {update.CarIndex}|{update.DriverIndex} not known, requesting new Entry List");
        }
        else
        {
            this.realTimeCarUpdateSubject.OnNext(update);
            Debug.WriteLine(update.ToString());
        }
    }

    private void ProcessRealtimeUpdateMessage(BinaryReader reader)
    {
        var update = reader.ReadRealtimeUpdate();
        this.realTimeUpdateSubject.OnNext(update);
        Debug.WriteLine(update.ToString());
    }

    private void ProcessRegistrationResultMessage(BinaryReader reader)
    {
        this.ConnectionId = reader.ReadInt32();
        var connectionSuccess = reader.ReadByte() > 0;
        var isReadonly = reader.ReadByte() == 0;
        var errMsg = reader.ReadString();

        var connectionState = new ConnectionState(this.ConnectionId,
                                                  connectionSuccess,
                                                  isReadonly,
                                                  errMsg);
        this.connectionStateChangeSubject.OnNext(connectionState);
        Debug.WriteLine(connectionState.ToString());

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
        Debug.WriteLine(update.ToString());
    }

    private void RequestEntryList()
    {
        using var stream = new MemoryStream();
        using var writer = new BinaryWriter(stream);
        writer.Write((byte)OutboundMessageTypes.RequestEntryList);
        writer.Write(this.ConnectionId);

        this.DispatchMessage(stream.ToArray());
    }

    private void RequestTrackData()
    {
        using var stream = new MemoryStream();
        using var writer = new BinaryWriter(stream);
        writer.Write((byte)OutboundMessageTypes.RequestTrackData);
        writer.Write(this.ConnectionId);

        this.DispatchMessage(stream.ToArray());
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

        this.DispatchMessage(stream.ToArray());
    }
}