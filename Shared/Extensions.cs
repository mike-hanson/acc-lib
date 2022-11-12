namespace Acc.Lib.Shared;

public static class Extensions
{
    public static string ToFriendlyName(this RaceSessionType sessionType)
    {
        return sessionType switch
               {
                   RaceSessionType.HotlapSuperpole => "Hotlap Superpole"
                 , _ => sessionType.ToString()
               };
    }
}