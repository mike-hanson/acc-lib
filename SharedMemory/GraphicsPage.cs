using System.Runtime.InteropServices;

namespace Acc.Lib.SharedMemory;

/// <summary>
/// Some code in this namespace is based on code from https://github.com/RiddleTime/Race-Element, which is in turn based on code from other open source repositories
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Unicode), Serializable]
public class GraphicsPage
{
    public int PacketId;
    public AccRtStatus Status;
    public AccRtSessionType SessionType;

    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 15)]
    public string CurrentTime;

    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 15)]
    public string LastTime;

    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 15)]
    public string BestTime;

    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 15)]
    public string Split;

    public int CompletedLaps;
    public int Position;
    public int CurrentTimeMs;
    public int LastTimeMs;
    public int BestTimeMs;
    public float SessionTimeLeft;
    public float DistanceTraveled;

    [MarshalAs(UnmanagedType.Bool)]
    public bool IsInPits;

    public int CurrentSectorIndex;
    public int LastSectorTime;
    public int NumberOfLaps;

    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 33)]
    public string TyreCompound;

    [Obsolete]
    public float ReplayTimeMultiplier;
    public float NormalizedCarPosition;

    public int ActiveCars;

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 60)]
    public AccRtVector3d[] CarCoordinates;

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 60)]
    public int[] CarIds;

    public int PlayerCarID;

    public float PenaltyTime;
    public AccRtFlagType Flag;

    public AccRtPenaltyShortcut PenaltyType;

    [MarshalAs(UnmanagedType.Bool)]
    public bool IdealLineOn;

    [MarshalAs(UnmanagedType.Bool)]
    public bool IsInPitLane;
    public float SurfaceGrip;

    [MarshalAs(UnmanagedType.Bool)]
    public bool MandatoryPitDone;

    public float WindSpeed;
    public float WindDirection;

    [MarshalAs(UnmanagedType.Bool)]
    public bool IsSetupMenuVisible;

    public int MainDisplayIndex;
    public int SecondaryDisplayIndex;
    public int TC;
    public int TCCut;
    public int EngineMap;
    public int ABS;
    public float AverageFuelPerLap;
    public int RainLights;
    public int FlashingLights;
    public int LightsStage;
    public float ExhaustTemperature;
    public int WiperLV;
    public int DriverStintTotalTimeLeft;
    public int DriverStintTimeLeft;
    public int RainTyres;

    public int SessionIndex;
    public float UsedFuelSinceRefuel;

    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 15)]
    public string DeltaLapTime;

    public int DeltaLapTimeMillis;

    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 15)]
    public string EstimatedLapTime;
    public int EstimatedLapTimeMillis;

    [MarshalAs(UnmanagedType.Bool)]
    public bool IsDeltaPositive;
    public int SplitTimeMillis;

    [MarshalAs(UnmanagedType.Bool)]
    public bool IsValidLap;
    public float FuelEstimatedLaps;

    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 33)]
    public string TrackStatus;

    public int MandatoryPitStopsLeft;
    float ClockTimeOfDaySeconds;

    [MarshalAs(UnmanagedType.Bool)]
    public bool IndicatorLeftOn;

    [MarshalAs(UnmanagedType.Bool)]
    public bool IndicatorRightOn;

    [MarshalAs(UnmanagedType.Bool)]
    public bool GlobalYellow;

    [MarshalAs(UnmanagedType.Bool)]
    public bool GlobalYellowSector1;

    [MarshalAs(UnmanagedType.Bool)]
    public bool GlobalYellowSector2;

    [MarshalAs(UnmanagedType.Bool)]
    public bool GlobalYellowSector3;

    [MarshalAs(UnmanagedType.Bool)]
    public bool GlobalWhite;

    [MarshalAs(UnmanagedType.Bool)]
    public bool GreenFlag;

    [MarshalAs(UnmanagedType.Bool)]
    public bool GlobalChequered;

    [MarshalAs(UnmanagedType.Bool)]
    public bool GlobalRed;

    public int mfdTyreSet;
    public float mfdFuelToAdd;

    public float mfdTyrePressureLF;
    public float mfdTyrePressureRF;
    public float mfdTyrePressureLR;
    public float mfdTyrePressureRR;
    public AccRtTrackGripStatus trackGripStatus;
    public AccRtRainIntensity rainIntensity;
    public AccRtRainIntensity rainIntensityIn10min;
    public AccRtRainIntensity rainIntensityIn30min;
    public int currentTyreSet;
    public int strategyTyreSet;
    public int gapAheadMillis;
    public int gapBehindMillis;


    public static readonly int Size = Marshal.SizeOf(typeof(GraphicsPage));
    public static readonly byte[] Buffer = new byte[Size];
};