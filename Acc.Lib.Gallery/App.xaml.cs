using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;
using System.Windows.Threading;
using Acc.Lib.Gallery.Services;
using Acc.Lib.Gallery.ViewModels.Pages;
using Acc.Lib.Gallery.ViewModels.Windows;
using Acc.Lib.Gallery.Views.Pages;
using Acc.Lib.Gallery.Views.Windows;
using Wpf.Ui;

namespace Acc.Lib.Gallery;

public partial class App
{
    private static readonly IHost host = Host.CreateDefaultBuilder()
                                             .ConfigureAppConfiguration(c =>
                                             {
                                                 c.SetBasePath(
                                                     Path.GetDirectoryName(
                                                         Assembly.GetEntryAssembly()!.Location)
                                                     ?? string.Empty);
                                             })
                                             .ConfigureServices((ctx, sc) =>
                                                                {
                                                                    sc.AddHostedService<
                                                                        ApplicationHostService>();

                                                                    sc.AddSingleton<IPageService,
                                                                        PageService>();

                                                                    sc.AddSingleton<IThemeService,
                                                                        ThemeService>();

                                                                    sc.AddSingleton<ITaskBarService,
                                                                        TaskBarService>();

                                                                    sc.AddSingleton<
                                                                        INavigationService,
                                                                        NavigationService>();

                                                                    sc.AddSingleton<
                                                                        INavigationWindow,
                                                                        MainWindow>();
                                                                    sc.AddSingleton<
                                                                        MainWindowViewModel>();

                                                                    sc.UsePageViewModels();
                                                                    sc.UsePages();
                                                                })
                                             .Build();

    public static T GetRequiredService<T>()
        where T: class
    {
        return host.Services.GetRequiredService<T>();
    }

    public static T GetService<T>()
        where T: class
    {
        return host.Services.GetService<T>();
    }

    private void OnDispatcherUnhandledException(object sender,
        DispatcherUnhandledExceptionEventArgs e)
    {
        // For more info see https://docs.microsoft.com/en-us/dotnet/api/system.windows.application.dispatcherunhandledexception?view=windowsdesktop-6.0
    }

    private async void OnExit(object sender, ExitEventArgs e)
    {
        await host.StopAsync();

        host.Dispose();
    }

    private void OnStartup(object sender, StartupEventArgs e)
    {
        host.Start();
    }
}