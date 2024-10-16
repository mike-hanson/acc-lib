using System.Reflection;
using Wpf.Ui.Appearance;
using Wpf.Ui.Controls;

namespace Acc.Lib.Gallery.ViewModels.Pages;

public partial class SettingsViewModel : ObservableObject, INavigationAware
{
    [ObservableProperty]
    private string appVersion = string.Empty;

    [ObservableProperty]
    private ApplicationTheme currentTheme = ApplicationTheme.Unknown;

    private bool isInitialized = false;

    public void OnNavigatedFrom() { }

    public void OnNavigatedTo()
    {
        if(!this.isInitialized)
        {
            this.InitializeViewModel();
        }
    }

    private string GetAssemblyVersion()
    {
        return Assembly.GetExecutingAssembly()
                       .GetName()
                       .Version?.ToString() ?? string.Empty;
    }

    private void InitializeViewModel()
    {
        this.CurrentTheme = ApplicationThemeManager.GetAppTheme();
        this.AppVersion = $"ACC Lib Gallery - {this.GetAssemblyVersion()}";

        this.isInitialized = true;
    }

    [RelayCommand]
    private void OnChangeTheme(string parameter)
    {
        switch(parameter)
        {
            case "theme_light":
                if(this.CurrentTheme == ApplicationTheme.Light)
                {
                    break;
                }

                ApplicationThemeManager.Apply(ApplicationTheme.Light);
                this.CurrentTheme = ApplicationTheme.Light;

                break;

            default:
                if(this.CurrentTheme == ApplicationTheme.Dark)
                {
                    break;
                }

                ApplicationThemeManager.Apply(ApplicationTheme.Dark);
                this.CurrentTheme = ApplicationTheme.Dark;

                break;
        }
    }
}