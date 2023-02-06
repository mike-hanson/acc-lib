using Acc.Lib.Shared;

namespace Acc.Lib.Models.RaceResult;

public class SessionDef
{
    public int DateHour { get; set; }
    public int DateMinute { get; set; }
    public RaceDay RaceDay { get; set; }
    public double TimeMultiplier { get; set; }
    public long PreSessionDuration { get; set; }
    public long SessionDuration { get; set; }
    public long OvertimeDuration { get; set; }
    public long Round { get; set; }
    public RaceSessionType SessionType { get; set; }
    public double DynamicTrackMultiplier { get; set; }
    public TrackStatus TrackStatus { get; set; }

    public string StartTime => (this.DateHour * 60 + this.DateMinute).ToTimeString();
    public string Duration => this.SessionDuration.ValidatedValue().ToTimeString();
}