namespace Acc.Lib.SharedMemory;

/// <summary>
/// Some code in this namespace is based on code from https://github.com/RiddleTime/Race-Element, which is in turn based on code from other open source repositories
/// </summary>
public enum AccRtPenaltyShortcut : int
{
    None
  , DriveThroughCutting
  , StopAndGo10Cutting
  , StopAndGo20Cutting
  , StopAndGo30Cutting
  , DisqualifiedCutting
  , RemoveBestLapTimeCutting
  , DriveThroughPitSpeeding
  , StopAndGo10PitSpeeding
  , StopAndGo20PitSpeeding
  , StopAndGo30PitSpeeding
  , DisqualifiedPitSpeeding
  , RemoveBestLapTimePitSpeeding
  , DisqualifiedIgnoredMandatoryPit
  , PostRaceTime
  , DisqualifiedTrolling
  , DisqualifiedPitEntry
  , DisqualifiedPitExit
  , DisqualifiedWrongWay
  , DriveThroughIgnoredDriverStint
  , DisqualifiedIgnoredDriverStint
  , DisqualifiedExceededDriverStintLimit
};