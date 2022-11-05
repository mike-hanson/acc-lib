using System.Runtime.InteropServices;

namespace Acc.Lib.SharedMemory;

[StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Unicode), Serializable]
public struct Coordinate3d
{
    public float X;
    public float Y;
    public float Z;

    public override string ToString() => $"X: {this.X}, Y: {this.Y}, Z: {this.Z}";
}