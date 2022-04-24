using System.Text;
using Acc.Lib.Models.Config;
using Acc.Lib.Models.Config.SeasonEntity;
using Newtonsoft.Json;

namespace Acc.Lib;

public class AccLocalConfigProvider
{
  public static BroadcastingSettings? GetBroadcastingSettings()
  {
    if(!File.Exists(AccPathProvider.BroadcastingSettingsFilePath))
    {
      return null;
    }

    var content = File.ReadAllText(AccPathProvider.BroadcastingSettingsFilePath, Encoding.UTF8);
    content = content.Replace(Environment.NewLine, "")
                     .Replace("\0", "")
                     .Replace("\n", "");
    return JsonConvert.DeserializeObject<BroadcastingSettings>(content);
  }

  public static SeasonSettings? GetSeasonSettings()
  {
    if(!File.Exists(AccPathProvider.SeasonSettingsFilePath))
    {
      return null;
    }

    var content = File.ReadAllText(AccPathProvider.SeasonSettingsFilePath, Encoding.UTF8);
    content = content.Replace(Environment.NewLine, "")
                     .Replace("\0", "")
                     .Replace("\n", "");
    return JsonConvert.DeserializeObject<SeasonSettings>(content);
  }

  public static void SaveBroadcastingSettings(BroadcastingSettings settings)
  {
    var json = JsonConvert.SerializeObject(settings);
    File.WriteAllText(AccPathProvider.BroadcastingSettingsFilePath, json);
  }
}