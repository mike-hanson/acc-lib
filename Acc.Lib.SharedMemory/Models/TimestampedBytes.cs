using System;

namespace Acc.Lib.SharedMemory.Models;

/// <summary>
/// Some code in this namespace is based on code from https://github.com/RiddleTime/Race-Element, which is in turn based on code from other open source repositories
/// </summary>
public class TimestampedBytes(byte[] rawData, DateTime incomingDate)
{
    public byte[] RawData = rawData;
    public DateTime IncomingDate = incomingDate;

    public TimestampedBytes(byte[] rawData)
        : this(rawData, DateTime.Now) { }
}