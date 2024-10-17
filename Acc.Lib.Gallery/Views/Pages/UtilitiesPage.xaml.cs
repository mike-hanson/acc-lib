using System;
using Acc.Lib.Gallery.ViewModels.Pages;
using Wpf.Ui.Controls;

namespace Acc.Lib.Gallery.Views.Pages;

public partial class UtilitiesPage : INavigableView<UtilitiesPageViewModel>
{
    public UtilitiesPage(UtilitiesPageViewModel viewModel)
    {
        this.ViewModel = viewModel;
        this.DataContext = this;
        this.InitializeComponent();
    }

    public UtilitiesPageViewModel ViewModel { get; }
}