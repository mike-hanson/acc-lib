using Acc.Lib.Broadcasting.Demo.ViewModels.Pages;
using Wpf.Ui.Controls;

namespace Acc.Lib.Broadcasting.Demo.Views.Pages
{
    public partial class SettingsPage : INavigableView<SettingsViewModel>
    {
        public SettingsViewModel ViewModel { get; }

        public SettingsPage(SettingsViewModel viewModel)
        {
            ViewModel = viewModel;
            DataContext = this;

            InitializeComponent();
        }
    }
}
