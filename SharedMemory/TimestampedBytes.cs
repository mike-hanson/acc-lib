namespace Acc.Lib.SharedMemory;

/// <summary>
/// Some code in this namespace is based on code from https://github.com/RiddleTime/Race-Element, which is in turn based on code from other open source repositories
/// </summary>
public class TimestampedBytes
{
    public byte[] RawData;
    public DateTime IncomingDate;

    public TimestampedBytes(byte[] rawData)
    {
        this.RawData = rawData;
        this.IncomingDate = DateTime.Now;
    }

    public TimestampedBytes(byte[] rawData, DateTime incomingDate)
    {
        this.RawData = rawData;
        this.IncomingDate = incomingDate;
    }
}