using System;

namespace Acc.Lib.Core;

public static class AccCompatibilityChecker
{
    public static bool HasCustomCars()
    {
        return IsAccInstalled() && Directory
                                   .EnumerateDirectories(AccPathProvider.CustomCarsFolderPath)
                                   .ToList()
                                   .Any();
    }

    public static bool HasCustomLiveries()
    {
        return IsAccInstalled() && Directory
                                   .EnumerateDirectories(AccPathProvider.CustomLiveriesFolderPath)
                                   .ToList()
                                   .Any();
    }

    public static bool HasDrivenAtLeastOneSession()
    {
        return IsAccInstalled() && Directory.GetFiles(AccPathProvider.ResultFolderPath)
                                            ?.Length > 0;
    }

    public static bool HasSavedSetup()
    {
        return IsAccInstalled() && Directory.Exists(AccPathProvider.SetupsFolderPath);
    }

    public static bool HasValidBroadcastingSettings()
    {
        var broadcastSettings = AccLocalConfigProvider.GetBroadcastingSettings();
        return broadcastSettings is
               {
                   UpdListenerPort: > 1023
               };
    }

    public static bool IsAccInstalled()
    {
        return Directory.Exists(AccPathProvider.DocumentsFolderPath);
    }

    public static bool IsAccountAvailable()
    {
        return IsAccInstalled() && File.Exists(AccPathProvider.AccountFilePath);
    }
}