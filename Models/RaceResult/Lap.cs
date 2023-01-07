using Acc.Lib.Shared;

namespace Acc.Lib.Models.RaceResult;

public class Lap
{
    public string Sector1Time =>
        TimeSpan.FromMilliseconds(this.Splits[0])
                .ToString(Constants.TimingFormat);
    public string Sector2Time =>
        TimeSpan.FromMilliseconds(this.Splits[1])
                .ToString(Constants.TimingFormat);
    public string Sector3Time =>
        TimeSpan.FromMilliseconds(this.Splits[2])
                .ToString(Constants.TimingFormat);

    public string Timestamp =>
        TimeSpan.FromMilliseconds(this.TimestampMS)
                .ToString(Constants.TimingFormat);
    public int CarId { get; set; }
    public int DriverId { get; set; }
    public int Flags { get; set; }
    public double Fuel { get; set; }
    public int LapTime { get; set; }
    public List<int> Splits { get; set; }
    public double TimestampMS { get; set; }

    public string GetLapTime()
    {
        return TimeSpan.FromMilliseconds(this.LapTime)
                       .ToString(Constants.TimingFormat);
    }
}