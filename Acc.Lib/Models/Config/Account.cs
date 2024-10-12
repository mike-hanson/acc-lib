using System;

namespace Acc.Lib.Models.Config;

public class Account
{
    public string FullName => $"{this.FirstName} {this.LastName}";
    public string DriverDisplayName => $"{this.FirstName[..1]}. {this.LastName}";
    public string Country { get; set; }
    public string DiscordUserName { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string GamePlatformUserId { get; set; }
    public string LastName { get; set; }
    public string LocalMachineId { get; set; }
    public string NickName { get; set; }

}