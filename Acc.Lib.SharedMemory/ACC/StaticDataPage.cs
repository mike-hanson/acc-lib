﻿using System;
using System.Runtime.InteropServices;

namespace Acc.Lib.SharedMemory.ACC;

/// <summary>
/// Some code in this namespace is based on code from https://github.com/RiddleTime/Race-Element, which is in turn based on code from other open source repositories
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Unicode), Serializable]
public class StaticDataPage
{
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 15)]
    public string SharedMemoryVersion;

    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 15)]
    public string AccVersion;

    public int NumberOfSessions;
    public int NumberOfCars;

    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 33)]
    public string CarModel;

    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 33)]
    public string TrackName;

    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 33)]
    public string PlayerFirstName;

    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 33)]
    public string PlayerSurname;

    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 33)]
    public string PlayerNickname;

    public int SectorCount;

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

    [Obsolete]
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