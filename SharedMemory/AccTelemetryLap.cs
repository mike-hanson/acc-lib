using System;

namespace Acc.Lib.SharedMemory;

public class AccTelemetryLap
{
    public AccTelemetryLap(StaticData staticData, GraphicsData graphicsData)
    {
        this.CarId = staticData.CarModel;
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

    public float FuelPerLap { get; }

    public bool IsOnline { get; }

    public float SessionTimeLeft { get; }

    public string SessionType { get; }

    public DateTime TimeStamp { get; }

    public string TrackId { get; }
}