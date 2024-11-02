using Acc.Lib.Broadcasting.Demo.ViewModels.Pages;
using Wpf.Ui.Controls;

namespace Acc.Lib.Broadcasting.Demo.Views.Pages
{
    public partial class DashboardPage : INavigableView<DashboardViewModel>
    {
        public DashboardViewModel ViewModel { get; }

        public DashboardPage(DashboardViewModel viewModel)
        {
            ViewModel = viewModel;
            DataContext = this;

            InitializeComponent();
        }
    }
}
