namespace Acc.Lib.Models.RaceResult;

public class SnapShot
{
    public long Bestlap { get; set; }
    public List<long> BestSplits { get; set; } = null!;
    public int IsWetSession { get; set; }
    public List<LeaderBoardLine> LeaderBoardLines { get; set; } = null!;
    public int Type { get; set; }

    internal LeaderBoardLine GetLeaderBoardLineByCarId(int carId)
    {
        return this.LeaderBoardLines.FirstOrDefault(l => l.Car.CarId == carId);
    }

    internal LeaderBoardLine GetPlayerLeaderBoardLine()
    {
        return this.LeaderBoardLines.First(e => e.CurrentDriver.PlayerId != "0");
    }
}