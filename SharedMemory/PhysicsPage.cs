using System.Runtime.InteropServices;

namespace Acc.Lib.SharedMemory;

/// <summary>
/// Some code in this namespace is based on code from https://github.com/RiddleTime/Race-Element, which is in turn based on code from other open source repositories
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Unicode), Serializable]
public class PhysicsPage
{
    public int PacketId;
    public float Gas;
    public float Brake;
    public float Fuel;
    public int Gear;
    public int Rpm;
    public float SteerAngle;
    public float SpeedKmh;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
    public float[] Velocity;

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
    public float[] AccG;

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public float[] WheelSlip;

    [Obsolete]
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public float[] WheelLoad;

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public float[] WheelPressure;

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public float[] WheelAngularSpeed;

    [Obsolete]
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public float[] TyreWear;

    [Obsolete]
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public float[] TyreDirtyLevel;

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public float[] TyreCoreTemperature;

    [Obsolete]
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public float[] CamberRad;

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public float[] SuspensionTravel;

    [Obsolete]
    public float Drs;
    public float TC;
    public float Heading;
    public float Pitch;
    public float Roll;

    [Obsolete]
    public float CgHeight;

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
    public float[] CarDamage;

    [Obsolete]
    public int NumberOfTyresOut;

    [MarshalAs(UnmanagedType.Bool)]
    public bool PitLimiterOn;
    public float Abs;

    [Obsolete]
    public float KersCharge;

    [Obsolete]
    public float KersInput;

    [MarshalAs(UnmanagedType.Bool)]
    public bool AutoShifterOn;

    [Obsolete]
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
    public float[] RideHeight;

    public float TurboBoost;

    [Obsolete]
    public float Ballast;
    [Obsolete]
    public float AirDensity;

    public float AirTemp;
    public float RoadTemp;

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
    public float[] LocalAngularVelocity;

    public float finalFF;

    [Obsolete]
    public float PerformanceMeter;
    [Obsolete]
    public int EngineBrake;
    [Obsolete]
    public int ErsRecoveryLevel;
    [Obsolete]
    public int ErsPowerLevel;
    [Obsolete]
    public int ErsHeatCharging;
    [Obsolete]
    public int ErsIsCharging;
    [Obsolete]
    public float KersCurrentKJ;

    [Obsolete]
    [MarshalAs(UnmanagedType.Bool)]
    public bool DrsAvailable;

    [Obsolete]
    [MarshalAs(UnmanagedType.Bool)]
    public bool DrsEnabled;

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public float[] BrakeTemperature;

    public float Clutch;

    [Obsolete]
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public float[] TyreTempI;

    [Obsolete]
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public float[] TyreTempM;

    [Obsolete]
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public float[] TyreTempO;

    [MarshalAs(UnmanagedType.Bool)]
    public bool IsAiControlled;


    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public AccRtVector3d[] TyreContactPoint;

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public AccRtVector3d[] TyreContactNormal;

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public AccRtVector3d[] TyreContactHeading;

    public float BrakeBias;

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
    public float[] LocalVelocity;

    [Obsolete]
    public int P2PActivations;
    [Obsolete]
    public int P2PStatus;

    [Obsolete]
    public int CurrentMaxRpm;

    [Obsolete]
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public float[] mz;

    [Obsolete]
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public float[] fx;

    [Obsolete]
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public float[] fy;

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public float[] SlipRatio;

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public float[] SlipAngle;

    [Obsolete]
    public int TcInAction;
    [Obsolete]
    public int AbsInAction;

    //[Obsolete]
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public float[] SuspensionDamage;

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public float[] TyreTemp;

    public float WaterTemp;

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public float[] brakePressure;
    public int frontBrakeCompound;
    public int rearBrakeCompound;

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public float[] PadLife;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public float[] DiscLife;

    [MarshalAs(UnmanagedType.Bool)]
    public bool IgnitionOn;

    [MarshalAs(UnmanagedType.Bool)]
    public bool StarterEngineOn;

    [MarshalAs(UnmanagedType.Bool)]
    public bool IsEngineRunning;

    public float KerbVibration;
    public float SlipVibrations;
    public float Gvibrations;
    public float AbsVibrations;

    public static readonly int Size = Marshal.SizeOf(typeof(PhysicsPage));
    public static readonly byte[] Buffer = new byte[Size];
}