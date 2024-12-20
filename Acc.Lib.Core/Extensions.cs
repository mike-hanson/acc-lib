﻿using System;
using Acc.Lib.Core.Enums;

namespace Acc.Lib.Core;

public static class Extensions
{
    public static string ToFriendlyName(this RaceSessionType sessionType)
    {
        return sessionType switch
               {
                   RaceSessionType.HotlapSuperpole => "Hotlap Superpole",
                   _ => sessionType.ToString()
               };
    }

    public static string ToFriendlyName(this SessionPhase sessionType)
    {
        return sessionType switch
               {
                   SessionPhase.FormationLap => "Formation Lap",
                   SessionPhase.PostSession => "Post Session",
                   SessionPhase.PreFormation => "Pre Formation",
                   SessionPhase.PreSession => "Pre Session",
                   SessionPhase.SessionOver => "Session Over",
                   _ => sessionType.ToString()
               };
    }

    public static string ToGapString(this int timingMs)
    {
        var timeSpan = TimeSpan.FromMilliseconds(timingMs);

        return timingMs switch
               {
                   >= Constants.MillisecondsPerHour => timeSpan.ToString(Constants.HoursGapFormat),
                   >= Constants.MilliSecondsPerMinute => timeSpan.ToString(Constants
                       .MinutesGapFormat),
                   _ => timeSpan.ToString(Constants.SecondsGapFormat)
               };
    }

    public static string ToGapString(this long timingMs)
    {
        var timeSpan = TimeSpan.FromMilliseconds(timingMs);

        return timingMs switch
               {
                   >= Constants.MillisecondsPerHour => timeSpan.ToString(Constants.HoursGapFormat),
                   >= Constants.MilliSecondsPerMinute => timeSpan.ToString(Constants
                       .MinutesGapFormat),
                   _ => timeSpan.ToString(Constants.SecondsGapFormat)
               };
    }

    public static string ToGapString(this float timingMs)
    {
        var timeSpan = TimeSpan.FromMilliseconds(timingMs);

        return timingMs switch
               {
                   >= Constants.MillisecondsPerHour => timeSpan.ToString(Constants.HoursGapFormat),
                   >= Constants.MilliSecondsPerMinute => timeSpan.ToString(Constants
                       .MinutesGapFormat),
                   _ => timeSpan.ToString(Constants.SecondsGapFormat)
               };
    }

    public static string ToTimeString(this double timeMs)
    {
        return TimeSpan.FromMilliseconds(timeMs)
                       .ToString(Constants.TimeFormat);
    }

    public static string ToTimeString(this int timeMs)
    {
        return TimeSpan.FromMilliseconds(timeMs)
                       .ToString(Constants.TimeFormat);
    }

    public static string ToTimeString(this long timeMs)
    {
        return TimeSpan.FromMilliseconds(timeMs)
                       .ToString(Constants.TimeFormat);
    }

    public static string ToTimingString(this double timingMs)
    {
        return TimeSpan.FromMilliseconds(timingMs)
                       .ToString(Constants.TimingFormat);
    }

    public static string ToTimingString(this float timingMs)
    {
        return TimeSpan.FromMilliseconds(timingMs)
                       .ToString(Constants.TimingFormat);
    }

    public static string ToTimingString(this int timingMs)
    {
        return TimeSpan.FromMilliseconds(timingMs)
                       .ToString(Constants.TimingFormat);
    }

    public static string ToTimingString(this long timingMs)
    {
        return TimeSpan.FromMilliseconds(timingMs)
                       .ToString(Constants.TimingFormat);
    }

    public static long ValidatedValue(this long longValue)
    {
        return longValue >= int.MaxValue? 0: longValue;
    }

    public static int ValidatedValue(this int intValue)
    {
        return intValue >= int.MaxValue? 0: intValue;
    }
}