using System.Text;
using Acc.Lib.Models.Config;
using Acc.Lib.Models.Config.SeasonEntity;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Acc.Lib;

public class AccLocalConfigProvider
{
    private static readonly JsonSerializerSettings jsonSerializerSettings = new()
        {
            ContractResolver = new DefaultContractResolver
                               {
                                   NamingStrategy = new CamelCaseNamingStrategy()
                               },
            Formatting = Formatting.Indented
        };

    public static Account GetAccount()
    {
        return DeserialiseConfigEntity<Account>(AccPathProvider.AccountFilePath);
    }

    public static BroadcastingSettings GetBroadcastingSettings()
    {
        return DeserialiseConfigEntity<BroadcastingSettings>(AccPathProvider
                                                                 .BroadcastingSettingsFilePath);
    }

    public static SeasonSettings GetSeasonSettings()
    {
        return DeserialiseConfigEntity<SeasonSettings>(AccPathProvider.SeasonSettingsFilePath);
    }

    public static void SaveBroadcastingSettings(BroadcastingSettings settings)
    {
        var json =
            JsonConvert.SerializeObject(settings, Formatting.Indented, jsonSerializerSettings);
        File.WriteAllText(AccPathProvider.BroadcastingSettingsFilePath, json);
    }

    private static T DeserialiseConfigEntity<T>(string filePath)
        where T: class
    {
        if(!File.Exists(filePath))
        {
            return null;
        }

        var content = NormalisedContent(filePath);
        return JsonConvert.DeserializeObject<T>(content);
    }

    private static string NormalisedContent(string filePath)
    {
        var content = File.ReadAllText(filePath, Encoding.UTF8);

        return content.Replace(Environment.NewLine, "")
                      .Replace("\0", "")
                      .Replace("\n", "");
    }
}