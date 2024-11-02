using System;

namespace Acc.Lib.Core;

public static class AccPathProvider
{
    private const string AccountFileName = "account.json";
    private const string BroadcastingSettingsFileName = "broadcasting.json";
    private const string ConfigFolderName = "Config";
    private const string CustomCarsFolderName = "Cars";
    private const string CustomDriversFolderName = "Drivers";
    private const string CustomLiveriesFolderName = "Liveries";
    private const string CustomsFolderName = "Customs";
    private const string DocumentsFolderName = "Assetto Corsa Competizione";
    private const string ReplaysFolderName = "Replay";
    private const string ReplaySavedFolderName = "Saved";
    private const string ResultsFolderName = "Results";
    private const string SeasonSettingsFileName = "seasonEntity.json";
    private const string SetupsFolderName = "Setups";

    static AccPathProvider()
    {
        var myDocumentsFolderPath =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                         DocumentsFolderName);
        AccountFilePath = Path.Combine(myDocumentsFolderPath, ConfigFolderName, AccountFileName);

        BroadcastingSettingsFilePath = Path.Combine(myDocumentsFolderPath,
                                                    ConfigFolderName,
                                                    BroadcastingSettingsFileName);
        ConfigFolderPath = Path.Combine(myDocumentsFolderPath, ConfigFolderName);
        CustomCarsFolderPath =
            Path.Combine(myDocumentsFolderPath, CustomsFolderName, CustomCarsFolderName);
        CustomDriversFolderPath =
            Path.Combine(myDocumentsFolderPath, CustomsFolderName, CustomDriversFolderName);

        CustomLiveriesFolderPath =
            Path.Combine(myDocumentsFolderPath, CustomsFolderName, CustomLiveriesFolderName);
        ResultFolderPath = Path.Combine(myDocumentsFolderPath, ResultsFolderName);
        SeasonSettingsFilePath =
            Path.Combine(myDocumentsFolderPath, ConfigFolderPath, SeasonSettingsFileName);
        SavedReplaysFolderPath = Path.Combine(myDocumentsFolderPath,
                                              ReplaysFolderName,
                                              ReplaySavedFolderName);
        SetupsFolderPath = Path.Combine(myDocumentsFolderPath, SetupsFolderName);
        DocumentsFolderPath = myDocumentsFolderPath;
    }

    public static string AccountFilePath { get; }
    public static string BroadcastingSettingsFilePath { get; }
    public static string ConfigFolderPath { get; }
    public static string CustomCarsFolderPath { get; }
    public static string CustomDriversFolderPath { get; }
    public static string CustomLiveriesFolderPath { get; }
    public static string DocumentsFolderPath { get; }
    public static string ResultFolderPath { get; }
    public static string SavedReplaysFolderPath { get; }
    public static string SeasonSettingsFilePath { get; }
    public static string SetupsFolderPath { get; }
}