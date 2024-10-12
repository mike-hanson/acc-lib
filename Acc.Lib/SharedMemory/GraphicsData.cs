namespace Acc.Lib.SharedMemory;

/// <summary>
/// Some code in this namespace is based on code from https://github.com/RiddleTime/Race-Element, which is in turn based on code from other open source repositories
/// </summary>
public class GraphicsData
{
    public GraphicsData(GraphicsPage graphicsPage)
    {
        this.TimeStamp = DateTime.UtcNow;
        this.PacketId = graphicsPage.PacketId;
        this.Status = graphicsPage.Status;
        this.SessionType = graphicsPage.SessionType;
        this.CurrentTime = graphicsPage.CurrentTime;
        this.LastTime = graphicsPage.LastTime;
        this.BestTime = graphicsPage.BestTime;
        this.Split = graphicsPage.Split;
        this.CompletedLaps = graphicsPage.CompletedLaps;
        this.Position = graphicsPage.Position;
        this.CurrentTimeMs = graphicsPage.CurrentTimeMs;
        this.LastTimeMs = graphicsPage.LastTimeMs;
        this.BestTimeMs = graphicsPage.BestTimeMs;
        this.SessionTimeLeft = graphicsPage.SessionTimeLeft;
        this.DistanceTravelled = graphicsPage.DistanceTraveled;
        this.IsInPits = graphicsPage.IsInPits;
        this.CurrentSectorIndex = graphicsPage.CurrentSectorIndex;
        this.LastSectorTime = graphicsPage.LastSectorTime;
        this.NumberOfLaps = graphicsPage.NumberOfLaps;
        this.TyreCompound = graphicsPage.TyreCompound;
        this.NormalizedCarPosition = graphicsPage.NormalizedCarPosition;
        this.ActiveCars = graphicsPage.ActiveCars;
        this.CarCoordinates = graphicsPage.CarCoordinates;
        this.CarIds = graphicsPage.CarIds;
        this.PlayerCarId = graphicsPage.PlayerCarID;
        this.PenaltyTime = graphicsPage.PenaltyTime;
        this.Flag = graphicsPage.Flag;
        this.PenaltyType = graphicsPage.PenaltyType;
        this.IdealLineOn = graphicsPage.IdealLineOn;
        this.IsInPitLane = graphicsPage.IsInPitLane;
        this.SurfaceGrip = graphicsPage.SurfaceGrip;
        this.MandatoryPitDone = graphicsPage.MandatoryPitDone;
        this.WindSpeed = graphicsPage.WindSpeed;
        this.WindDirection = graphicsPage.WindDirection;
        this.IsSetupMenuVisible = graphicsPage.IsSetupMenuVisible;
        this.MainDisplayIndex = graphicsPage.MainDisplayIndex;
        this.SecondaryDisplayIndex = graphicsPage.SecondaryDisplayIndex;
        this.TractionControl = graphicsPage.TC;
        this.TractionControlCut = graphicsPage.TCCut;
        this.EngineMap = graphicsPage.EngineMap;
        this.Abs = graphicsPage.ABS;
        this.FuelPerLap = graphicsPage.AverageFuelPerLap;
        this.RainLights = graphicsPage.RainLights;
        this.FlashingLights = graphicsPage.FlashingLights;
        this.LightsStage = graphicsPage.LightsStage;
        this.ExhaustTemperature = graphicsPage.ExhaustTemperature;
        this.WiperLevel = graphicsPage.WiperLV;
        this.DriverStintTotalTimeLeft = graphicsPage.DriverStintTotalTimeLeft;
        this.DriverStintTimeLeft = graphicsPage.DriverStintTimeLeft;
        this.RainTyres = graphicsPage.RainTyres;
        this.SessionIndex = graphicsPage.SessionIndex;
        this.UsedFuelSinceRefuel = graphicsPage.UsedFuelSinceRefuel;
        this.DeltaLapTime = graphicsPage.DeltaLapTime;
        this.DeltaLapTimeMs = graphicsPage.DeltaLapTimeMillis;
        this.EstimatedLapTime = graphicsPage.EstimatedLapTime;
        this.EstimatedLapTimeMs = graphicsPage.EstimatedLapTimeMillis;
        this.IsDeltaPositive = graphicsPage.IsDeltaPositive;
        this.SplitTimeMs = graphicsPage.SplitTimeMillis;
        this.IsValidLap = graphicsPage.IsValidLap;
        this.FuelEstimatedLaps = graphicsPage.FuelEstimatedLaps;
        this.TrackStatus = graphicsPage.TrackStatus;
        this.MandatoryPitStopsLeft = graphicsPage.MandatoryPitStopsLeft;
        this.IsLeftIndicatorOn = graphicsPage.IndicatorLeftOn;
        this.IsRightIndicatorOn = graphicsPage.IndicatorRightOn;
        this.GlobalYellow = graphicsPage.GlobalYellow;
        this.GlobalYellowSector1 = graphicsPage.GlobalYellowSector1;
        this.GlobalYellowSector2 = graphicsPage.GlobalYellowSector2;
        this.GlobalYellowSector3 = graphicsPage.GlobalYellowSector3;
        this.GlobalWhite = graphicsPage.GlobalWhite;
        this.MfdTyreSet = graphicsPage.mfdTyreSet;
        this.MfdFuelToAdd = graphicsPage.mfdFuelToAdd;
        this.MfdTyrePressureLf = graphicsPage.mfdTyrePressureLF;
        this.MfdTyrePressureRf = graphicsPage.mfdTyrePressureRF;
        this.MfdTyrePressureLr = graphicsPage.mfdTyrePressureLR;
        this.MfdTyrePressureRr = graphicsPage.mfdTyrePressureRR;
        this.TrackGripStatus = graphicsPage.trackGripStatus;
        this.RainIntensity = graphicsPage.rainIntensity;
        this.RainIntensityIn10Min = graphicsPage.rainIntensityIn10min;
        this.RainIntensityIn30Min = graphicsPage.rainIntensityIn30min;
        this.CurrentTyreSet = graphicsPage.currentTyreSet;
        this.StrategyTyreSet = graphicsPage.strategyTyreSet;
        this.GapAheadMs = graphicsPage.gapAheadMillis;
        this.GapBehindMs = graphicsPage.gapBehindMillis;
    }

    public int Abs { get; }

    public int ActiveCars { get; }

    public string BestTime { get; }

    public int BestTimeMs { get; }

    public AccRtVector3d[] CarCoordinates { get; }

    public int[] CarIds { get; }

    public int CompletedLaps { get; }

    public int CurrentSectorIndex { get; }

    public string CurrentTime { get; }

    public int CurrentTimeMs { get; }

    public int CurrentTyreSet { get; }

    public string DeltaLapTime { get; }

    public int DeltaLapTimeMs { get; }

    public float DistanceTravelled { get; }

    public int DriverStintTimeLeft { get; }

    public int DriverStintTotalTimeLeft { get; }

    public int EngineMap { get; }

    public string EstimatedLapTime { get; }

    public int EstimatedLapTimeMs { get; }

    public float ExhaustTemperature { get; }

    public AccRtFlagType Flag { get; }

    public int FlashingLights { get; }

    public float FuelEstimatedLaps { get; }

    public float FuelPerLap { get; }

    public int GapAheadMs { get; }

    public int GapBehindMs { get; }

    public bool GlobalWhite { get; }

    public bool GlobalYellow { get; }

    public bool GlobalYellowSector1 { get; }

    public bool GlobalYellowSector2 { get; }

    public bool GlobalYellowSector3 { get; }

    public bool IdealLineOn { get; }

    public bool IsDeltaPositive { get; }

    public bool IsInPitLane { get; }

    public bool IsInPits { get; }

    public bool IsLeftIndicatorOn { get; }

    public bool IsRightIndicatorOn { get; }

    public bool IsSetupMenuVisible { get; }

    public bool IsValidLap { get; }

    public int LastSectorTime { get; }

    public string LastTime { get; }

    public int LastTimeMs { get; }

    public int LightsStage { get; }

    public int MainDisplayIndex { get; }

    public bool MandatoryPitDone { get; }

    public int MandatoryPitStopsLeft { get; }

    public float MfdFuelToAdd { get; }

    public float MfdTyrePressureLf { get; }

    public float MfdTyrePressureLr { get; }

    public float MfdTyrePressureRf { get; }

    public float MfdTyrePressureRr { get; }

    public int MfdTyreSet { get; }

    public float NormalizedCarPosition { get; }

    public int NumberOfLaps { get; }

    public int PacketId { get; }

    public float PenaltyTime { get; }

    public AccRtPenaltyShortcut PenaltyType { get; }

    public int PlayerCarId { get; }

    public int Position { get; }

    public AccRtRainIntensity RainIntensity { get; }

    public AccRtRainIntensity RainIntensityIn10Min { get; }

    public AccRtRainIntensity RainIntensityIn30Min { get; }

    public int RainLights { get; }

    public int RainTyres { get; }

    public int SecondaryDisplayIndex { get; }

    public int SessionIndex { get; }

    public float SessionTimeLeft { get; }

    public AccRtSessionType SessionType { get; }

    public string Split { get; }

    public int SplitTimeMs { get; }

    public AccRtStatus Status { get; }

    public int StrategyTyreSet { get; }

    public float SurfaceGrip { get; }

    public DateTime TimeStamp { get; }

    public AccRtTrackGripStatus TrackGripStatus { get; }

    public string TrackStatus { get; }

    public int TractionControl { get; }

    public int TractionControlCut { get; }

    public string TyreCompound { get; }

    public float UsedFuelSinceRefuel { get; }

    public float WindDirection { get; }

    public float WindSpeed { get; }

    public int WiperLevel { get; }

    public override string ToString()
    {
        return
            $"Graphics Data: Time Stamp: {this.TimeStamp:hh:mm:ss.ffff}, Current Time MS: {this.CurrentTimeMs}, Last Time MS: {this.LastTimeMs}, Current Sector Index: {this.CurrentSectorIndex}, Split Time Ms: {this.SplitTimeMs}, In Pit lane: {this.IsInPitLane}, In Pits: {this.IsInPits}";
    }
}