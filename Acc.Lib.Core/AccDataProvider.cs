using System;
using System.Text;
using Acc.Lib.Core.Exceptions.Exceptions;
using Acc.Lib.Core.Models.Customs;
using Acc.Lib.Core.Models.RaceResult;
using Newtonsoft.Json;

namespace Acc.Lib.Core;

public static class AccDataProvider
{
    /// <summary>The list of valid prefixes for ACC offline result files</summary>
    private static readonly IList<string> FilePrefixes = new List<string>
                                                         {
                                                             "Race",
                                                             "Hotstint",
                                                             "Qualifying",
                                                             "Practice"
                                                         };

    /// <summary>Gets the collection of custom car definitions.</summary>
    /// <returns>A collection of CustomCar objects deserialised from the JSON files maintained by ACC</returns>
    /// <exception cref = "InvalidCustomCarException"></exception>
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

                var customCar = JsonConvert.DeserializeObject<CustomCar>(CleanJson(content));
                customCar.FilePath = filePath;
                result.Add(customCar);
            }
            catch(Exception exception)
            {
                throw new InvalidCustomCarException(filePath, exception);
            }
        }

        return result;
    }

    /// <summary>Gets a collection of information about custom skins/liveries</summary>
    /// <returns>A collection of CustomSkin objects with the name of a skin and the folder path</returns>
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

    /// <summary>Gets the recent session file paths.</summary>
    /// <returns>A collection of file paths for the offline sessions recorded by ACC</returns>
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

    /// <summary>Gets the recent sessions.</summary>
    /// <returns>A collection of RaceSession object deserialised from the JSON files recorded by ACC for offline sessions </returns>
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

    /// <summary>Determines whether a path is a valid offline session file</summary>
    /// <param name="sessionFilePath">The session file path.</param>
    /// <returns>
    ///   <c>true</c> if the specified path is an ofline session file, otherwise, <c>false</c>.</returns>
    public static bool IsLocalSessionFile(string sessionFilePath)
    {
        var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(sessionFilePath);
        return FilePrefixes.Any(filePrefix => fileNameWithoutExtension.StartsWith(filePrefix));
    }

    /// <summary>Loads an offline race session.</summary>
    /// <param name="filePath">The file path.</param>
    /// <returns>A RaceSession object deserialised from the JSON recorded by ACC for an offline session</returns>
    public static RaceSession LoadRaceSession(string filePath)
    {
        try
        {
            var fileContent = File.ReadAllText(filePath);
            var json = CleanJson(fileContent);
            return JsonConvert.DeserializeObject<RaceSession>(json);
        }
        catch(Exception exception)
        {
            Console.WriteLine(exception);
            throw;
        }
    }

    private static string CleanJson(string json)
    {
        return json.Replace("\0", "")
                   .Replace("\n", "");
    }
}