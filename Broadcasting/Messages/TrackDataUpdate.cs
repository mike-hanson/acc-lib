using System;

namespace Acc.Lib.Broadcasting.Messages;

public class TrackDataUpdate
{
    public TrackDataUpdate()
    {
        this.CameraSets = new Dictionary<string, List<string>>();
        this.HudPages = new List<string>();
    }
    public Dictionary<string, List<string>> CameraSets { get; internal set; }
    public IEnumerable<string> HudPages { get; internal set; }
    public string ConnectionIdentifier { get; internal set; }
    public int TrackId { get; internal set; }
    public float TrackMeters { get; internal set; }
    public string TrackName { get; internal set; }
    public override string ToString()
    {
        return $"Track Data Update: Connection: {this.ConnectionIdentifier}, Track ID {this.TrackId}, Track: {this.TrackName}, Track Meters: {this.TrackMeters}";
    }
}