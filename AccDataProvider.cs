using System.Text;
using Acc.Lib.Exceptions;
using Acc.Lib.Models.Customs;
using Acc.Lib.Models.RaceResult;
using Newtonsoft.Json;

namespace Acc.Lib;

public class AccDataProvider
{
    private static readonly IList<string> validCarCodes = new List<string>
                                                          {
                                                              "AMRV8",
                                                              "AR8EVO",
                                                              "AR8EVOII",
                                                              "BENTC",
                                                              "BMWM4",
                                                              "BMWM6",
                                                              "FER488",
                                                              "FER488EVO",
                                                              "HONNSX",
                                                              "LAMHUR",
                                                              "LAMHUREVO",
                                                              "LEXUSRC",
                                                              "MC720S",
                                                              "MERCAMG",
                                                              "MERCAMGEVO",
                                                              "NISGTR",
                                                              "PO991II"
                                                          };
    private static readonly IList<string> FilePrefixes = new List<string>
                                                         {
                                                             "Race",
                                                             "Hotstint",
                                                             "Qualifying",
                                                             "Practice"
                                                         };

    public static IEnumerable<CustomCar> GetCustomCars()
    {
        if(!Directory.Exists(AccPathProvider.CustomCarsFolderPath))
        {
            return Enumerable.Empty<CustomCar>();
        }

        var filePaths = Directory.GetFiles(AccPathProvider.CustomCarsFolderPath, "*.json");
        var result = new List<CustomCar>();
        foreach(var filePath in filePaths)
        {
            try
            {
                var content = File.ReadAllText(filePath, Encoding.Unicode);
                if(!content.Contains("displayName") && !content.Contains("carModelType"))
                {
                    continue;
                }

                var customCar =
                    JsonConvert.DeserializeObject<CustomCar>(CleanJson(content));
                customCar.FilePath = filePath;
                result.Add(customCar);
            }
            catch (Exception exception)
            {
               throw new InvalidCustomCarException(filePath, exception);
            }
        }

        return result;
    }

    public static IEnumerable<CustomSkin> GetCustomSkins()
    {
        if(!Directory.Exists(AccPathProvider.CustomLiveriesFolderPath))
        {
            return Enumerable.Empty<CustomSkin>(); 
        }

        var folderPaths = Directory.GetDirectories(AccPathProvider.CustomLiveriesFolderPath);

        var result = new List<CustomSkin>();

        foreach(var folderPath in folderPaths)
        {
            var folderName =
                Path.GetRelativePath(AccPathProvider.CustomLiveriesFolderPath, folderPath);
            result.Add(new CustomSkin
                       {
                           Name = folderName,
                           FolderPath = folderPath
                       });
        }

        return result;
    }

    public static IEnumerable<string> GetRecentSessionFilePaths()
    {
        if(!Directory.Exists(AccPathProvider.ResultFolderPath))
        {
            return Enumerable.Empty<string>();
        }

        return Directory.GetFiles(AccPathProvider.ResultFolderPath, "*.json")
                        .Where(IsLocalSessionFile)
                        .ToList();
    }

    public static IEnumerable<RaceSession> GetRecentSessions()
    {
        var result = new List<RaceSession>();

        var sessionFilePaths = GetRecentSessionFilePaths();
        foreach(var sessionFilePath in sessionFilePaths)
        {
            var raceSession = LoadRaceSession(sessionFilePath);
            result.Add(raceSession);
        }

        return result;
    }

    public static bool IsLocalSessionFile(string sessionFilePath)
    {
        var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(sessionFilePath);
        return FilePrefixes.Any(filePrefix => fileNameWithoutExtension.StartsWith(filePrefix));
    }

    public static RaceSession LoadRaceSession(string filePath)
    {
        Thread.Sleep(1000);
        try
        {
            var fileContent = File.ReadAllText(filePath);
            var json = CleanJson(fileContent);
            return JsonConvert.DeserializeObject<RaceSession>(json);
        }
        catch(Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private static string CleanJson(string json)
    {
        return json.Replace("\0", "")
                   .Replace("\n", "");
    }
}