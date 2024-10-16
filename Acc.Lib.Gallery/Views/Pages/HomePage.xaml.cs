using Acc.Lib.Gallery.ViewModels.Pages;
using Wpf.Ui.Controls;

namespace Acc.Lib.Gallery.Views.Pages;

public partial class HomePage : INavigableView<HomePageViewModel>
{
    public HomePage(HomePageViewModel viewModel)
    {
        this.ViewModel = viewModel;
        this.DataContext = this;

        this.InitializeComponent();
    }

    public HomePageViewModel ViewModel { get; }
}