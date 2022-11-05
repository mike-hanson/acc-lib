namespace Acc.Lib.SharedMemory;

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