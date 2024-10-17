using System;
using Microsoft.Extensions.DependencyInjection;

namespace Acc.Lib.Gallery.ViewModels.Pages;

internal static class ServiceCollectionExtension
{
    internal static IServiceCollection UsePageViewModels(this IServiceCollection services)
    {
        services.AddSingleton<AccDataProviderPageViewModel>();
        services.AddSingleton<AccDetectionPageViewModel>();
        services.AddSingleton<AccLocalConfigProviderPageViewModel>();
        services.AddSingleton<AccPathProviderPageViewModel>();
        services.AddSingleton<HomePageViewModel>();
        services.AddSingleton<SettingsViewModel>();
        services.AddSingleton<UtilitiesPageViewModel>();
        return services;
    }
}