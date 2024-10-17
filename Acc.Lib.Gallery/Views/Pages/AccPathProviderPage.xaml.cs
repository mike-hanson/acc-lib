using System;
using Acc.Lib.Gallery.ViewModels.Pages;
using Wpf.Ui.Controls;

namespace Acc.Lib.Gallery.Views.Pages;

public partial class AccPathProviderPage : INavigableView<AccPathProviderPageViewModel>
{
    public AccPathProviderPage(AccPathProviderPageViewModel viewModel)
    {
        this.ViewModel = viewModel;
        this.DataContext = this;
        this.InitializeComponent();
    }

    public AccPathProviderPageViewModel ViewModel { get; }
}