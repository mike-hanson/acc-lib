using Acc.Lib.Gallery.Views.Pages;
using Acc.Lib.Gallery.Views.Windows;
using Microsoft.Extensions.Hosting;
using Wpf.Ui;

namespace Acc.Lib.Gallery.Services;

public class ApplicationHostService(IServiceProvider serviceProvider) : IHostedService
{
    private INavigationWindow _navigationWindow;

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await this.HandleActivationAsync();
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
    }

    private async Task HandleActivationAsync()
    {
        if(!Application.Current.Windows.OfType<MainWindow>()
                       .Any())
        {
            this._navigationWindow =
                (serviceProvider.GetService(typeof(INavigationWindow)) as INavigationWindow)!;
            this._navigationWindow!.ShowWindow();

            this._navigationWindow.Navigate(typeof(HomePage));
        }

        await Task.CompletedTask;
    }
}