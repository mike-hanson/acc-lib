using System;
using System.Text;
using Acc.Lib.Core.Models.Config;
using Acc.Lib.Core.Models.Config.SeasonEntity;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Acc.Lib.Core;

public static class AccLocalConfigProvider
{
    private static readonly JsonSerializerSettings jsonSerializerSettings = new()
        {
            ContractResolver = new DefaultContractResolver
                               {
                                   NamingStrategy = new CamelCaseNamingStrategy()
                               },
            Formatting = Formatting.Indented,
            
        };

    public static Account? GetAccount()
    {
        return DeserialiseConfigEntity<Account>(AccPathProvider.AccountFilePath);
    }

    public static BroadcastingSettings? GetBroadcastingSettings()
    {
        return DeserialiseConfigEntity<BroadcastingSettings>(AccPathProvider
                                                                 .BroadcastingSettingsFilePath);
    }

    // public static SeasonSettings? GetSeasonSettings()
    // {
    //     return DeserialiseConfigEntity<SeasonSettings>(AccPathProvider.SeasonSettingsFilePath);
    // }

    public static void SaveBroadcastingSettings(BroadcastingSettings settings)
    {
        var json =
            JsonConvert.SerializeObject(settings, Formatting.Indented, jsonSerializerSettings);
        File.WriteAllText(AccPathProvider.BroadcastingSettingsFilePath, json, Encoding.UTF8);
    }

    private static T? DeserialiseConfigEntity<T>(string filePath)
        where T: class
    {
        if(!File.Exists(filePath))
        {
            return null;
        }


        try
        {
            var content = GetContent(filePath, Encoding.UTF8);
            return JsonConvert.DeserializeObject<T>(content, jsonSerializerSettings);
        }
        catch (Exception)
        {
            var content = GetContent(filePath, Encoding.Unicode);
            return JsonConvert.DeserializeObject<T>(content, jsonSerializerSettings);
        }
    }

    private static string GetContent(string filePath, Encoding encoding)
    {
        return File.ReadAllText(filePath, encoding);
    }
}