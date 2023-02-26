using System;

namespace Acc.Lib.SharedMemory;

public class AccTelemetryLap
{
    public AccTelemetryLap(StaticData staticData, GraphicsData graphicsData)
    {
        this.CarId = staticData.CarModel;
        this.DriverName = $"{staticData.PlayerName[..1]}. {staticData.PlayerSurname}";
        this.CompletedLaps = graphicsData.CompletedLaps;
        this.FuelPerLap = graphicsData.FuelPerLap;
        this.IsOnline = staticData.IsOnline;
        this.SessionTimeLeft = graphicsData.SessionTimeLeft;
        this.SessionType = graphicsData.SessionType.ToFriendlyName();
        this.TimeStamp = graphicsData.TimeStamp;
        this.TrackId = staticData.Track;
    }

    public string CarId { get; }

    public int CompletedLaps { get; }

    public string DriverName { get; }

    public float FuelPerLap { get; }

    public bool IsOnline { get; }

    public float SessionTimeLeft { get; }

    public string SessionType { get; }

    public DateTime TimeStamp { get; }

    public string TrackId { get; }

    public override string ToString()
    {
        return
            $"Telemetry Lap: Track ID: {this.TrackId} Car ID: {this.CarId} Driver: {this.DriverName} Is Online: {this.IsOnline} Completed Laps: {this.CompletedLaps}";
    }
}