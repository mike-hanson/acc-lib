using Acc.Lib.Shared;

namespace Acc.Lib.Models.RaceResult;

public class Timing
{
    
    public int LastLap { get; set; }
    public List<int> LastSplits { get; set; } = null!;
    public int BestLap { get; set; }
    public List<int> BestSplits { get; set; } = null!;
    public int TotalTime { get; set; }
    public int LapCount { get; set; }
    public int LastSplitId { get; set; }

    public string BestLapTime =>
        TimeSpan.FromMilliseconds(this.BestLap)
                .ToString(Constants.TimingFormat);
    public string BestSector1 =>
        TimeSpan.FromMilliseconds(this.BestSplits[0])
                .ToString(Constants.TimingFormat);
    public string BestSector2 =>
        TimeSpan.FromMilliseconds(this.BestSplits[1])
                .ToString(Constants.TimingFormat);
    public string BestSector3 =>
        TimeSpan.FromMilliseconds(this.BestSplits[2])
                .ToString(Constants.TimingFormat);
    public string AverageLapTime =>
        TimeSpan.FromMilliseconds((double) this.TotalTime / this.LapCount)
                .ToString(Constants.TimingFormat);
}