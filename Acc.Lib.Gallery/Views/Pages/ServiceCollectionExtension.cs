using System;
using Microsoft.Extensions.DependencyInjection;

namespace Acc.Lib.Gallery.Views.Pages;

internal static class ServiceCollectionExtension
{
    internal static IServiceCollection UsePages(this IServiceCollection services)
    {
        services.AddSingleton<AccDataProviderPage>();
        services.AddSingleton<AccDetectionPage>();
        services.AddSingleton<AccLocalConfigProviderPage>();
        services.AddSingleton<AccPathProviderPage>();
        services.AddSingleton<HomePage>();
        services.AddSingleton<SettingsPage>();
        services.AddSingleton<UtilitiesPage>();
        return services;
    }
}