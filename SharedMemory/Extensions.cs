using System.IO.MemoryMappedFiles;
using System.Runtime.InteropServices;

namespace Acc.Lib.SharedMemory;

public static class Extensions
{
    public static string ToFriendlyString(this AccFlagType flagType)
    {
        return flagType switch
               {
                   AccFlagType.NoFlag => "Green"
                 , AccFlagType.BlueFlag => "Blue"
                 , AccFlagType.YellowFlag => "Yellow"
                 , AccFlagType.BlackFlag => "Black"
                 , AccFlagType.WhiteFlag => "White"
                 , AccFlagType.ChequeredFlag => "Chequered"
                 , AccFlagType.PenaltyFlag => "Penalty"
                 , AccFlagType.GreenFlag => "Green"
                 , AccFlagType.BlackFlagWithOrangeCircle => "Orange"
                 , _ => flagType.ToString()
               };
    }

    public static string ToFriendlyString(this AccSessionType sessionType)
    {
        return sessionType switch
               {
                   AccSessionType.HotlapSuperpole => "Hotlap Superpole"
                 , AccSessionType.TimeAttack => "Time Attack", _ => sessionType.ToString()
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