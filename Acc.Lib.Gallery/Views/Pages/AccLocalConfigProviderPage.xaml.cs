using System;
using Acc.Lib.Gallery.ViewModels.Pages;
using Wpf.Ui.Controls;

namespace Acc.Lib.Gallery.Views.Pages;

public partial class
    AccLocalConfigProviderPage : INavigableView<AccLocalConfigProviderPageViewModel>
{
    public AccLocalConfigProviderPage(AccLocalConfigProviderPageViewModel viewModel)
    {
        this.ViewModel = viewModel;
        this.DataContext = this;
        this.InitializeComponent();
    }

    public AccLocalConfigProviderPageViewModel ViewModel { get; }
}