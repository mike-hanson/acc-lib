using System;

namespace Acc.Lib.Gallery.ViewModels.Pages;

public partial class AccDetectionPageViewModel : ObservableObject
{
    private AccDetector detector;
    private IDisposable detectorSubscription;

    [ObservableProperty]
    private bool isAccRunning;

    [RelayCommand]
    public void Start()
    {
        this.detector = new AccDetector();
        this.detectorSubscription = this.detector.Start()
                                        .Subscribe(this.OnNextIsRunningUpdate);
    }

    [RelayCommand]
    public void Stop()
    {
        this.detectorSubscription.Dispose();
        this.detector.Dispose();
        this.detectorSubscription = null;
        this.detector = null;
    }

    private void OnNextIsRunningUpdate(bool isRunning)
    {
        this.IsAccRunning = isRunning;
    }
}