using System;
using System.Runtime.InteropServices;

namespace Acc.Lib.SharedMemory.Models;

/// <summary>
/// Some code in this namespace is based on code from https://github.com/RiddleTime/Race-Element, which is in turn based on code from other open source repositories
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Unicode), Serializable]
public struct AccRtVector3d
{
    public float X;
    public float Y;
    public float Z;

    public override string ToString() => $"X: {this.X}, Y: {this.Y}, Z: {this.Z}";
}