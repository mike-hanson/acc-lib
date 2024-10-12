namespace Acc.Lib.Models.RaceResult;

public class Driver
{
    public string FullName => $"{this.FirstName} {this.LastName}";
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PlayerId { get; set; }
    public string ShortName { get; set; }
}