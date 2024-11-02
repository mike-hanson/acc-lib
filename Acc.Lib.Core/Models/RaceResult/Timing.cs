using System;

namespace Acc.Lib.Core.Models.RaceResult;

public class Timing
{
	public string AverageLapTime =>
		(this.LapCount > 0? ((double)this.TotalTime.ValidatedValue()) / this.LapCount: 0)
		.ToTimingString();

	public string BestLapTime =>
		this.BestLap.ValidatedValue()
		    .ToTimingString();
	public string BestSector1 => (this.LapCount > 0? this.BestSplits[0]: 0).ToTimingString();
	public string BestSector2 => (this.LapCount > 0? this.BestSplits[1]: 0).ToTimingString();
	public string BestSector3 => (this.LapCount > 0? this.BestSplits[2]: 0).ToTimingString();
	public long BestLap { get; set; }
	public List<int> BestSplits { get; set; } = null!;
	public int LapCount { get; set; }
	public long LastLap { get; set; }
	public long LastSplitId { get; set; }
	public List<long> LastSplits { get; set; } = null!;
	public long TotalTime { get; set; }
}