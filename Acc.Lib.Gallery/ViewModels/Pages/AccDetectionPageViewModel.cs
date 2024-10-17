using System;

namespace Acc.Lib.Gallery.ViewModels.Pages;

public partial class AccDetectionPageViewModel : ObservableObject
{
    public AccDetectionPageViewModel()
    {
        this.XamlCode = @"<Grid Grid.Column=""0""
            Grid.Row=""1""
            VerticalAlignment=""Top""
            HorizontalAlignment=""Left"">
        <Grid.ColumnDefinitions>
            <ColumnDefinition Width=""Auto"" />
            <ColumnDefinition />
        </Grid.ColumnDefinitions>
        <Label Grid.Column=""0""
                Content=""ACC:""
                VerticalAlignment=""Center""
                FontWeight=""Bold"" />
        <Image Grid.Column=""1""
                ToolTip=""Indicates whether ACC is running on this computer""
                Height=""24""
                Width=""24""
                Margin=""0 0 5 0"">
            <Image.Style>
                <Style TargetType=""{x:Type Image}"">
                    <Style.Triggers>
                        <DataTrigger Binding=""{Binding ViewModel.IsAccRunning}""
                                    Value=""True"">
                            <Setter Property=""Source""
                                    Value=""/Assets/button_green.png"" />
                        </DataTrigger >
                        <DataTrigger Binding=""{Binding ViewModel.IsAccRunning}""
                                    Value=""False"">
                            <Setter Property=""Source""
                                    Value=""/Assets/button_blue.png"" />
                        </DataTrigger>
                    </Style.Triggers>
                </Style>
            </Image.Style>
        </Image>
    </Grid >";

        this.CSharpCode = @"public partial class AccDetectionPageViewModel : ObservableObject
{
    [ObservableProperty]
    private string cSharpCode;

    private AccDetector detector;
    private IDisposable detectorSubscription;

    [ObservableProperty]
    private bool isAccRunning;

    [ObservableProperty]
    private string xamlCode;

    [RelayCommand]
    public void Start()
    {
        this.detector = new AccDetector();
        this.detectorSubscription = this.detector.Start().Subscribe(this.OnNextIsRunningUpdate);
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
}";
    }

    [ObservableProperty]
    private string cSharpCode;

    private AccDetector detector;
    private IDisposable detectorSubscription;

    [ObservableProperty]
    private bool isAccRunning;

    [ObservableProperty]
    private string xamlCode;

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