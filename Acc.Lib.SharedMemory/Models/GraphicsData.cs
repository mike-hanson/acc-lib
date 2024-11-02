using System;
using Acc.Lib.SharedMemory.ACC;
using Acc.Lib.SharedMemory.Enums;

namespace Acc.Lib.SharedMemory.Models;

/// <summary>
/// Some code in this namespace is based on code from https://github.com/RiddleTime/Race-Element, which is in turn based on code from other open source repositories
/// </summary>
public class GraphicsData(GraphicsPage graphicsPage)
{
    public int Abs { get; } = graphicsPage.ABS;

    public int ActiveCars { get; } = graphicsPage.ActiveCars;

    public string BestTime { get; } = graphicsPage.BestTime;

    public int BestTimeMs { get; } = graphicsPage.BestTimeMs;

    public AccRtVector3d[] CarCoordinates { get; } = graphicsPage.CarCoordinates;

    public int[] CarIds { get; } = graphicsPage.CarIds;

    public int CompletedLaps { get; } = graphicsPage.CompletedLaps;

    public int CurrentSectorIndex { get; } = graphicsPage.CurrentSectorIndex;

    public string CurrentTime { get; } = graphicsPage.CurrentTime;

    public int CurrentTimeMs { get; } = graphicsPage.CurrentTimeMs;

    public int CurrentTyreSet { get; } = graphicsPage.currentTyreSet;

    public string DeltaLapTime { get; } = graphicsPage.DeltaLapTime;

    public int DeltaLapTimeMs { get; } = graphicsPage.DeltaLapTimeMillis;

    public float DistanceTravelled { get; } = graphicsPage.DistanceTraveled;

    public int DriverStintTimeLeft { get; } = graphicsPage.DriverStintTimeLeft;

    public int DriverStintTotalTimeLeft { get; } = graphicsPage.DriverStintTotalTimeLeft;

    public int EngineMap { get; } = graphicsPage.EngineMap;

    public string EstimatedLapTime { get; } = graphicsPage.EstimatedLapTime;

    public int EstimatedLapTimeMs { get; } = graphicsPage.EstimatedLapTimeMillis;

    public float ExhaustTemperature { get; } = graphicsPage.ExhaustTemperature;

    public AccRtFlagType Flag { get; } = graphicsPage.Flag;

    public int FlashingLights { get; } = graphicsPage.FlashingLights;

    public float FuelEstimatedLaps { get; } = graphicsPage.FuelEstimatedLaps;

    public float FuelPerLap { get; } = graphicsPage.AverageFuelPerLap;

    public int GapAheadMs { get; } = graphicsPage.gapAheadMillis;

    public int GapBehindMs { get; } = graphicsPage.gapBehindMillis;

    public bool GlobalWhite { get; } = graphicsPage.GlobalWhite;

    public bool GlobalYellow { get; } = graphicsPage.GlobalYellow;

    public bool GlobalYellowSector1 { get; } = graphicsPage.GlobalYellowSector1;

    public bool GlobalYellowSector2 { get; } = graphicsPage.GlobalYellowSector2;

    public bool GlobalYellowSector3 { get; } = graphicsPage.GlobalYellowSector3;

    public bool IdealLineOn { get; } = graphicsPage.IdealLineOn;

    public bool IsDeltaPositive { get; } = graphicsPage.IsDeltaPositive;

    public bool IsInPitLane { get; } = graphicsPage.IsInPitLane;

    public bool IsInPits { get; } = graphicsPage.IsInPits;

    public bool IsLeftIndicatorOn { get; } = graphicsPage.IndicatorLeftOn;

    public bool IsRightIndicatorOn { get; } = graphicsPage.IndicatorRightOn;

    public bool IsSetupMenuVisible { get; } = graphicsPage.IsSetupMenuVisible;

    public bool IsValidLap { get; } = graphicsPage.IsValidLap;

    public int LastSectorTime { get; } = graphicsPage.LastSectorTime;

    public string LastTime { get; } = graphicsPage.LastTime;

    public int LastTimeMs { get; } = graphicsPage.LastTimeMs;

    public int LightsStage { get; } = graphicsPage.LightsStage;

    public int MainDisplayIndex { get; } = graphicsPage.MainDisplayIndex;

    public bool MandatoryPitDone { get; } = graphicsPage.MandatoryPitDone;

    public int MandatoryPitStopsLeft { get; } = graphicsPage.MandatoryPitStopsLeft;

    public float MfdFuelToAdd { get; } = graphicsPage.mfdFuelToAdd;

    public float MfdTyrePressureLf { get; } = graphicsPage.mfdTyrePressureLF;

    public float MfdTyrePressureLr { get; } = graphicsPage.mfdTyrePressureLR;

    public float MfdTyrePressureRf { get; } = graphicsPage.mfdTyrePressureRF;

    public float MfdTyrePressureRr { get; } = graphicsPage.mfdTyrePressureRR;

    public int MfdTyreSet { get; } = graphicsPage.mfdTyreSet;

    public float NormalizedCarPosition { get; } = graphicsPage.NormalizedCarPosition;

    public int NumberOfLaps { get; } = graphicsPage.NumberOfLaps;

    public int PacketId { get; } = graphicsPage.PacketId;

    public float PenaltyTime { get; } = graphicsPage.PenaltyTime;

    public AccRtPenaltyShortcut PenaltyType { get; } = graphicsPage.PenaltyType;

    public int PlayerCarId { get; } = graphicsPage.PlayerCarID;

    public int Position { get; } = graphicsPage.Position;

    public AccRtRainIntensity RainIntensity { get; } = graphicsPage.rainIntensity;

    public AccRtRainIntensity RainIntensityIn10Min { get; } = graphicsPage.rainIntensityIn10min;

    public AccRtRainIntensity RainIntensityIn30Min { get; } = graphicsPage.rainIntensityIn30min;

    public int RainLights { get; } = graphicsPage.RainLights;

    public int RainTyres { get; } = graphicsPage.RainTyres;

    public int SecondaryDisplayIndex { get; } = graphicsPage.SecondaryDisplayIndex;

    public int SessionIndex { get; } = graphicsPage.SessionIndex;

    public float SessionTimeLeft { get; } = graphicsPage.SessionTimeLeft;

    public AccRtSessionType SessionType { get; } = graphicsPage.SessionType;

    public string Split { get; } = graphicsPage.Split;

    public int SplitTimeMs { get; } = graphicsPage.SplitTimeMillis;

    public AccRtStatus Status { get; } = graphicsPage.Status;

    public int StrategyTyreSet { get; } = graphicsPage.strategyTyreSet;

    public float SurfaceGrip { get; } = graphicsPage.SurfaceGrip;

    public DateTime TimeStamp { get; } = DateTime.UtcNow;

    public AccRtTrackGripStatus TrackGripStatus { get; } = graphicsPage.trackGripStatus;

    public string TrackStatus { get; } = graphicsPage.TrackStatus;

    public int TractionControl { get; } = graphicsPage.TC;

    public int TractionControlCut { get; } = graphicsPage.TCCut;

    public string TyreCompound { get; } = graphicsPage.TyreCompound;

    public float UsedFuelSinceRefuel { get; } = graphicsPage.UsedFuelSinceRefuel;

    public float WindDirection { get; } = graphicsPage.WindDirection;

    public float WindSpeed { get; } = graphicsPage.WindSpeed;

    public int WiperLevel { get; } = graphicsPage.WiperLV;

    public override string ToString()
    {
        return
            $"Graphics Data: Time Stamp: {this.TimeStamp:hh:mm:ss.ffff}, Current Time MS: {this.CurrentTimeMs}, Last Time MS: {this.LastTimeMs}, Current Sector Index: {this.CurrentSectorIndex}, Split Time Ms: {this.SplitTimeMs}, In Pit lane: {this.IsInPitLane}, In Pits: {this.IsInPits}";
    }
}