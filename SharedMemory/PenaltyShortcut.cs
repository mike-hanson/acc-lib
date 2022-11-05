namespace Acc.Lib.SharedMemory;

public enum PenaltyShortcut : int
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