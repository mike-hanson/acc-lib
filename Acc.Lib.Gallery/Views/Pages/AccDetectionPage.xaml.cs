using System;
using Acc.Lib.Gallery.ViewModels.Pages;
using Wpf.Ui.Controls;

namespace Acc.Lib.Gallery.Views.Pages;

public partial class AccDetectionPage : INavigableView<AccDetectionPageViewModel>
{
    public AccDetectionPage(AccDetectionPageViewModel viewModel)
    {
        this.ViewModel = viewModel;
        this.DataContext = this;
        this.InitializeComponent();
    }

    public AccDetectionPageViewModel ViewModel { get; }
}