using Acc.Lib.Broadcasting.Demo.Views.Pages;
using Acc.Lib.Broadcasting.Demo.Views.Windows;
using Microsoft.Extensions.Hosting;
using Wpf.Ui;

namespace Acc.Lib.Broadcasting.Demo.Services;

public class ApplicationHostService(IServiceProvider serviceProvider) : IHostedService
{
    private INavigationWindow navigationWindow;

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
            this.navigationWindow =
                (serviceProvider.GetService(typeof(INavigationWindow)) as INavigationWindow)!;
            this.navigationWindow!.ShowWindow();

            this.navigationWindow.Navigate(typeof(DashboardPage));
        }

        await Task.CompletedTask;
    }
}