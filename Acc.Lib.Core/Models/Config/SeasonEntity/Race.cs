using System;

namespace Acc.Lib.Core.Models.Config.SeasonEntity;

public class Race
{
    public int EventType { get; set; }
    public EventRules EventRules { get; set; }
    public List<Session> Sessions { get; set; }
}