using System;

namespace Acc.Lib.SharedMemory;

public class AccRealTimeConnection : IDisposable
{
    private readonly List<CancellationTokenSource> runningTasks = new();
    private readonly List<Timer> runningTimers = new();
    private bool isDisposed;

    public AccRealTimeConnection(AccRealTimeConnectionSettings settings = null)
    {
        this.Settings = settings ?? new AccRealTimeConnectionSettings();
    }

    public event EventHandler<GraphicsData> GraphicsDataUpdated;
    public event EventHandler<PhysicsData> PhysicsDataUpdated;
    public event EventHandler<StaticData> StaticDataUpdated;

    public AccRealTimeConnectionSettings Settings { get; }

    public void Dispose()
    {
        this.Dispose(true);
        GC.SuppressFinalize(this);
    }

    public void Start()
    {
        if(this.Settings.ContinuousGraphicsUpdates)
        {
            this.StartContinuousGraphicsUpdates();
        }
        else
        {
            this.StartTimedGraphicsUpdates();
        }

        if(this.Settings.ContinuousPhysicsUpdates)
        {
            this.StartContinuousPhysicsUpdates();
        }
        else
        {
            this.StartTimedPhysicsUpdates();
        }

        if(this.Settings.ContinuousStaticUpdates)
        {
            this.StartContinuousStaticUpdates();
        }
        else
        {
            this.StartTimedStaticUpdates();
        }
    }

    public void Stop()
    {
        foreach(var runningTask in this.runningTasks)
        {
            runningTask.Cancel();
        }

        foreach(var runningTimer in this.runningTimers)
        {
            runningTimer.Dispose();
        }

        this.runningTasks.Clear();
        this.runningTimers.Clear();
    }

    protected virtual void Dispose(bool disposing)
    {
        if(this.isDisposed)
        {
            return;
        }

        if(disposing)
        {
            this.Stop();
        }

        this.isDisposed = true;
    }

    private void NotifyGraphicsUpdated(GraphicsData graphicsData)
    {
        var handler = this.GraphicsDataUpdated;
        handler?.Invoke(this, graphicsData);
    }

    private void NotifyPhysicsUpdated(PhysicsData physicsData)
    {
        var handler = this.PhysicsDataUpdated;
        handler?.Invoke(this, physicsData);
    }

    private void NotifyStaticUpdated(StaticData staticData)
    {
        var handler = this.StaticDataUpdated;
        handler?.Invoke(this, staticData);
    }

    private void SendGraphicsUpdate()
    {
        var graphicsData = AccSharedMemoryProvider.ReadGraphicsData();
        this.NotifyGraphicsUpdated(graphicsData);
    }

    private void SendPhysicsUpdate()
    {
        var physicsData = AccSharedMemoryProvider.ReadPhysicsData();
        this.NotifyPhysicsUpdated(physicsData);
    }

    private void SendStaticDataUpdate()
    {
        var staticData = AccSharedMemoryProvider.ReadStaticData();
        this.NotifyStaticUpdated(staticData);
    }

    private void StartContinuousGraphicsUpdates()
    {
        var cancellationTokenSource = new CancellationTokenSource();
        var cancellationToken = cancellationTokenSource.Token;
        this.runningTasks.Add(cancellationTokenSource);
        Task.Factory.StartNew(() =>
                              {
                                  while(true)
                                  {
                                      this.SendGraphicsUpdate();
                                      if(cancellationToken.IsCancellationRequested)
                                      {
                                          break;
                                      }
                                  }
                              }
                            , cancellationToken);
    }

    private void StartContinuousPhysicsUpdates()
    {
        var cancellationTokenSource = new CancellationTokenSource();
        var cancellationToken = cancellationTokenSource.Token;
        this.runningTasks.Add(cancellationTokenSource);
        Task.Factory.StartNew(() =>
                              {
                                  while(true)
                                  {
                                      this.SendPhysicsUpdate();
                                      if(cancellationToken.IsCancellationRequested)
                                      {
                                          break;
                                      }
                                  }
                              }
                            , cancellationToken);
    }

    private void StartContinuousStaticUpdates()
    {
        var cancellationTokenSource = new CancellationTokenSource();
        var cancellationToken = cancellationTokenSource.Token;
        this.runningTasks.Add(cancellationTokenSource);
        Task.Factory.StartNew(() =>
                              {
                                  while(true)
                                  {
                                      this.SendStaticDataUpdate();
                                      if(cancellationToken.IsCancellationRequested)
                                      {
                                          break;
                                      }
                                  }
                              }
                            , cancellationToken);
    }

    private void StartTimedGraphicsUpdates()
    {
        this.runningTimers.Add(new Timer(_ => this.SendGraphicsUpdate()
                                       , null
                                       , TimeSpan.FromMilliseconds(5)
                                       , TimeSpan.FromMilliseconds(this.Settings
                                             .GraphicsUpdateIntervalMs)));
    }

    private void StartTimedPhysicsUpdates()
    {
        this.runningTimers.Add(new Timer(_ => this.SendPhysicsUpdate()
                                       , null
                                       , TimeSpan.FromMilliseconds(5)
                                       , TimeSpan.FromMilliseconds(this.Settings
                                             .PhysicsUpdateIntervalMs)));
    }

    private void StartTimedStaticUpdates()
    {
        this.runningTimers.Add(new Timer(_ => this.SendStaticDataUpdate()
                                       , null
                                       , TimeSpan.FromMilliseconds(5)
                                       , TimeSpan.FromMilliseconds(this.Settings
                                             .StaticUpdatesIntervalMs)));
    }
}