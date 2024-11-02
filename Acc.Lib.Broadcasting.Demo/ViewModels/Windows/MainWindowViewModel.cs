using System.Collections.ObjectModel;
using Acc.Lib.Broadcasting.Demo.Views.Pages;
using Wpf.Ui.Controls;

namespace Acc.Lib.Broadcasting.Demo.ViewModels.Windows;

public partial class MainWindowViewModel : ObservableObject
{
    [ObservableProperty]
    private string applicationTitle = "ACC Lib Broadcasting Demo";

    [ObservableProperty]
    private ObservableCollection<object> footerMenuItems =
    [
        new NavigationViewItem("Settings",SymbolRegular.Settings24, typeof(SettingsPage))
    ];

    [ObservableProperty]
    private ObservableCollection<object> menuItems =
    [
        new NavigationViewItem("Home",SymbolRegular.Home24,typeof(DashboardPage)),

        new NavigationViewItem("Data",SymbolRegular.DataHistogram24, typeof(DataPage))
    ];

    [ObservableProperty]
    private ObservableCollection<MenuItem> trayMenuItems =
    [
        new MenuItem
        {
            Header = "Home",
            Tag = "tray_home"
        }
    ];
}