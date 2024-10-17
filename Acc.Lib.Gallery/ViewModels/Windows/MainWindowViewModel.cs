using System.Collections.ObjectModel;
using Acc.Lib.Gallery.Views.Pages;
using Wpf.Ui.Controls;

namespace Acc.Lib.Gallery.ViewModels.Windows;

public partial class MainWindowViewModel : ObservableObject
{
    [ObservableProperty]
    private string applicationTitle = "ACC Lib Gallery";

    [ObservableProperty]
    private ObservableCollection<object> footerMenuItems =
    [
        new NavigationViewItem("Settings",SymbolRegular.Settings24,typeof(SettingsPage))
    ];

    [ObservableProperty]
    private ObservableCollection<object> menuItems =
    [
        new NavigationViewItem("Home",SymbolRegular.Home24,typeof(HomePage)),

        new NavigationViewItem("Utilities", SymbolRegular.Toolbox24, typeof(UtilitiesPage))
        {
            MenuItemsSource = new object[]
                              {
                                  new NavigationViewItem("ACC Data Provider",
                                      SymbolRegular.Class24,
                                      typeof(AccDataProviderPage)),
                                  new NavigationViewItem("ACC Local Config Provider",
                                      SymbolRegular.Class24,
                                      typeof(AccLocalConfigProviderPage)),
                                  new NavigationViewItem("ACC Path Provider",
                                      SymbolRegular.Class24,
                                      typeof(AccPathProviderPage))
                              }
        },

        new NavigationViewItem("ACC Detection",SymbolRegular.Video24, typeof(AccDetectionPage))
    ];

    [ObservableProperty]
    private ObservableCollection<MenuItem> trayMenuItems =
    [
        new()
        {
            Header = "Home",
            Tag = "tray_home"
        }
    ];
}