using Acc.Lib.Shared;

namespace Acc.Lib.Models.RaceResult;

public class SessionDef
{
    public int DateHour { get; set; }
    public int DateMinute { get; set; }
    public RaceDay RaceDay { get; set; }
    public double TimeMultiplier { get; set; }
    public int PreSessionDuration { get; set; }
    public int SessionDuration { get; set; }
    public int OvertimeDuration { get; set; }
    public int Round { get; set; }
    public RaceSessionType SessionType { get; set; }
    public double DynamicTrackMultiplier { get; set; }
    public TrackStatus TrackStatus { get; set; }

    public string StartTime =>
        TimeSpan.FromMinutes(this.DateHour * 60 + this.DateMinute).ToString(Constants.TimeFormat);
    public string Duration => TimeSpan.FromMinutes(this.SessionDuration).ToString(Constants.TimeFormat);
}