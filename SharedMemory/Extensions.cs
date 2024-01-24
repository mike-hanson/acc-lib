using System.IO.MemoryMappedFiles;
using System.Runtime.InteropServices;

namespace Acc.Lib.SharedMemory;

/// <summary>
/// Some code in this namespace is based on code from https://github.com/RiddleTime/Race-Element, which is in turn based on code from other open source repositories
/// </summary>
public static class Extensions
{
    public static string ToFriendlyName(this AccRtFlagType flagType)
    {
        return flagType switch
               {
                   AccRtFlagType.NoFlag => "Green"
                 , AccRtFlagType.BlueFlag => "Blue"
                 , AccRtFlagType.YellowFlag => "Yellow"
                 , AccRtFlagType.BlackFlag => "Black"
                 , AccRtFlagType.WhiteFlag => "White"
                 , AccRtFlagType.ChequeredFlag => "Chequered"
                 , AccRtFlagType.PenaltyFlag => "Penalty"
                 , AccRtFlagType.GreenFlag => "Green"
                 , AccRtFlagType.BlackFlagWithOrangeCircle => "Orange"
                 , _ => flagType.ToString()
               };
    }

    public static string ToFriendlyName(this AccRtSessionType sessionType)
    {
        return sessionType switch
               {
                   AccRtSessionType.HotlapSuperpole => "Hotlap Superpole"
                 , AccRtSessionType.TimeAttack => "Time Attack", _ => sessionType.ToString()
               };
    }

    public static T ToStruct<T>(this MemoryMappedFile file, byte[] buffer)
    {
        using var stream = file.CreateViewStream();
        stream.Read(buffer, 0, buffer.Length);
        var handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
        var data = (T)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(T));
        handle.Free();
        return data;
    }

    public static T ToStruct<T>(this TimestampedBytes timestampedBytes)
    {
        var handle = GCHandle.Alloc(timestampedBytes.RawData, GCHandleType.Pinned);
        var data = (T)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(T));
        handle.Free();
        return data;
    }

    public static TimestampedBytes ToTimestampedBytes<T>(this T s, byte[] buffer)
        where T: struct
    {
        var ptr = Marshal.AllocHGlobal(buffer.Length);
        Marshal.StructureToPtr(s, ptr, false);
        Marshal.Copy(ptr, buffer, 0, buffer.Length);
        Marshal.FreeHGlobal(ptr);
        return new TimestampedBytes(buffer, DateTime.Now);
    }
}