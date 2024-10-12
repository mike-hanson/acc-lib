namespace Acc.Lib.SharedMemory;

/// <summary>
/// Some code in this namespace is based on code from https://github.com/RiddleTime/Race-Element, which is in turn based on code from other open source repositories
/// </summary>
public enum AccRtSessionType
{
    Unknown = -1
  , Practice = 0
  , Qualify = 1
  , Race = 2
  , Hotlap = 3
  , TimeAttack = 4
  , Drift = 5
  , Drag = 6
  , HotStint = 7
  , HotlapSuperpole = 8
}