using System;
using System.Diagnostics;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace Acc.Lib.Core;

/// <summary>
///     Detects whether ACC is running or not
/// </summary>
public class AccDetector : IDisposable
{
    private readonly Subject<bool> isRunning = new();
    private bool isAccRunning;
    private IDisposable? updateSubscription;

    /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
    public void Dispose()
    {
        this.Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <summary>Starts detecting whether ACC is running.</summary>
    /// <param name = "updateIntervalMs">The update interval in milliseconds.</param>
    /// <returns>
    ///     <see cref="https://learn.microsoft.com/en-us/dotnet/api/system.iobservable-1?view=net-8.0"/>
    /// </returns>
    public IObservable<bool> Start(double updateIntervalMs = 1000)
    {
        this.updateSubscription = Observable.Interval(TimeSpan.FromMilliseconds(updateIntervalMs))
                                            .Subscribe(this.OnNextInterval);

        return this.isRunning.AsObservable();
    }

    /// <summary>Releases unmanaged and - optionally - managed resources.</summary>
    /// <param name = "disposing">
    ///     <c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.
    /// </param>
    protected virtual void Dispose(bool disposing)
    {
        if(disposing)
        {
            this.isRunning?.Dispose();
            this.updateSubscription?.Dispose();
        }
    }

    private void OnNextInterval(long _)
    {
        var processIsRunning = Process.GetProcessesByName("AC2-Win64-Shipping").Any();
        if(processIsRunning == this.isAccRunning)
        {
            return;
        }

        this.isAccRunning = processIsRunning;
        this.isRunning.OnNext(this.isAccRunning);
    }
}