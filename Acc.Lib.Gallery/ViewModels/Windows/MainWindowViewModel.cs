using System.Collections.ObjectModel;
using Wpf.Ui.Controls;

namespace Acc.Lib.Gallery.ViewModels.Windows
{
    public partial class MainWindowViewModel : ObservableObject
    {
        [ObservableProperty]
        private string applicationTitle = "ACC Lib Gallery";

        [ObservableProperty]
        private ObservableCollection<object> menuItems =
        [
            new NavigationViewItem()
            {
                Content = "Home",
                Icon = new SymbolIcon
                       {
                           Symbol = SymbolRegular.Home24
                       },
                TargetPageType = typeof(Views.Pages.HomePage)
            },

            new NavigationViewItem()
            {
                Content = "ACC Detection",
                Icon = new SymbolIcon
                       {
                           Symbol = SymbolRegular.Video24
                       },
                TargetPageType = typeof(Views.Pages.AccDetectionPage)
            }
        ];

        [ObservableProperty]
        private ObservableCollection<object> footerMenuItems =
        [
            new NavigationViewItem()
            {
                Content = "Settings",
                Icon = new SymbolIcon
                       {
                           Symbol = SymbolRegular.Settings24
                       },
                TargetPageType = typeof(Views.Pages.SettingsPage)
            }
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
}
