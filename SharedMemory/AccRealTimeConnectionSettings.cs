using System;

namespace Acc.Lib.SharedMemory;

public class AccRealTimeConnectionSettings
{
    public bool ContinuousGraphicsUpdates { get; set; }
    public bool ContinuousPhysicsUpdates { get; set; }
    public bool ContinuousStaticUpdates { get; set; }
    public int GraphicsUpdateIntervalMs { get; set; } = 1000;
    public int PhysicsUpdateIntervalMs { get; set; } = 100;
    public int StaticUpdatesIntervalMs { get; set; } = 5000;
}