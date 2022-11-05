using System.Runtime.InteropServices;

namespace Acc.Lib.SharedMemory;

[StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Unicode), Serializable]
public class StaticDataPage
{
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 15)]
    public string SharedMemoryVersion;

    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 15)]
    public string AssettoCorsaVersion;

    public int NumberOfSessions;
    public int NumberOfCars;

    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 33)]
    public string CarModel;

    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 33)]
    public string Track;

    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 33)]
    public string PlayerName;

    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 33)]
    public string PlayerSurname;

    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 33)]
    public string PlayerNickname;

    public int SectorCount;

    // car static info
    [Obsolete]
    public float MaxTorque;
    [Obsolete]
    public float MaxPower;
    public int MaxRpm;
    public float MaxFuel;

    [Obsolete]
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public float[] SuspensionMaxTravel;

    [Obsolete]
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public float[] TyreRadius;

    public float MaxTurboBoost;

    [Obsolete]
    public float AirTemperature;

    [Obsolete]
    public float RoadTemperature;

    [MarshalAs(UnmanagedType.Bool)]
    public bool PenaltiesEnabled;
    public float AidFuelRate;
    public float AidTireRate;
    public float AidMechanicalDamage;
    [MarshalAs(UnmanagedType.Bool)]
    public bool AidAllowTyreBlankets;
    public float AidStability;
    [MarshalAs(UnmanagedType.Bool)]
    public bool AidAutoClutch;
    [MarshalAs(UnmanagedType.Bool)]
    public bool AidAutoBlip;

    [Obsolete]
    [MarshalAs(UnmanagedType.Bool)]
    public bool HasDRS;

    [Obsolete]
    [MarshalAs(UnmanagedType.Bool)]
    public bool HasERS;

    [Obsolete]
    [MarshalAs(UnmanagedType.Bool)]
    public bool HasKERS;
    [Obsolete]
    public float KersMaxJoules;
    [Obsolete]
    public int EngineBrakeSettingsCount;
    [Obsolete]
    public int ErsPowerControllerCount;
    [Obsolete]
    public float TrackSplineLength;
    [Obsolete]
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 33)]
    public string TrackConfiguration;
    [Obsolete]
    public float ErsMaxJ;
    [Obsolete]
    [MarshalAs(UnmanagedType.Bool)]
    public bool IsTimedRace;
    [Obsolete]
    [MarshalAs(UnmanagedType.Bool)]
    public bool HasExtraLap;
    [Obsolete]
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 33)]
    public string CarSkin;
    [Obsolete]
    public int ReversedGridPositions;

    public int PitWindowStart;
    public int PitWindowEnd;


    [MarshalAs(UnmanagedType.Bool)]
    public bool isOnline;

    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 33)]
    public string DryTyresName;

    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 33)]
    public string WetTyresName;

    public static readonly int Size = Marshal.SizeOf(typeof(StaticDataPage));
    public static readonly byte[] Buffer = new byte[Size];
}