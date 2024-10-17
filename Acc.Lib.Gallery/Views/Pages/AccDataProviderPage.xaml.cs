using System;
using Acc.Lib.Gallery.ViewModels.Pages;
using Wpf.Ui.Controls;

namespace Acc.Lib.Gallery.Views.Pages;

public partial class AccDataProviderPage : INavigableView<AccDataProviderPageViewModel>
{
    public AccDataProviderPage(AccDataProviderPageViewModel viewModel)
    {
        this.ViewModel = viewModel;
        this.DataContext = this;
        this.InitializeComponent();
    }

    public AccDataProviderPageViewModel ViewModel { get; }
}