namespace Acc.Lib.SharedMemory;

public class StaticData
{
    public StaticData(StaticDataPage staticDataPage)
    {
        this.SharedMemoryVersion = staticDataPage.SharedMemoryVersion;
        this.AccVersion = staticDataPage.AccVersion;
        this.NumberOfSessions = staticDataPage.NumberOfSessions;
        this.NumberOfCars = staticDataPage.NumberOfCars;
        this.CarModel = staticDataPage.CarModel;
        this.Track = staticDataPage.TrackName;
        this.PlayerName = staticDataPage.PlayerFirstName;
        this.PlayerSurname = staticDataPage.PlayerSurname;
        this.PlayerNickname = staticDataPage.PlayerNickname;
        this.SectorCount = staticDataPage.SectorCount;
        this.MaxRpm = staticDataPage.MaxRpm;
        this.MaxFuel = staticDataPage.MaxFuel;
        this.PenaltiesEnabled = staticDataPage.PenaltiesEnabled;
        this.AidFuelRate = staticDataPage.AidFuelRate;
        this.AidTyreRate = staticDataPage.AidTireRate;
        this.AidMechanicalDamage = staticDataPage.AidMechanicalDamage;
        this.AidAllowTyreBlankets = staticDataPage.AidAllowTyreBlankets;
        this.AidStability = staticDataPage.AidStability;
        this.AidAutoClutch = staticDataPage.AidAutoClutch;
        this.AidAutoBlip = staticDataPage.AidAutoBlip;
        this.PitWindowStart = staticDataPage.PitWindowStart;
        this.PitWindowEnd = staticDataPage.PitWindowEnd;
        this.IsOnline = staticDataPage.isOnline;
        this.DryTyresName = staticDataPage.DryTyresName;
        this.WetTyresName = staticDataPage.WetTyresName;
    }

    public string AccVersion { get; }

    public bool AidAllowTyreBlankets { get; }

    public bool AidAutoBlip { get; }

    public bool AidAutoClutch { get; }

    public float AidFuelRate { get; }

    public float AidMechanicalDamage { get; }

    public float AidStability { get; }

    public float AidTyreRate { get; }

    public string CarModel { get; }

    public string DryTyresName { get; }

    public bool IsOnline { get; }

    public float MaxFuel { get; }

    public int MaxRpm { get; }

    public int NumberOfCars { get; }

    public int NumberOfSessions { get; }

    public bool PenaltiesEnabled { get; }

    public int PitWindowEnd { get; }

    public int PitWindowStart { get; }

    public string PlayerDisplayName() {
        
        if(!string.IsNullOrEmpty(this.PlayerName)) {
            return $"{this.PlayerName[..1]}. {this.PlayerSurname}";
        }

        if(!string.IsNullOrEmpty(this.PlayerNickname))
        {
            return this.PlayerNickname;
        }

        return "Not Available";
    }

    public string PlayerName { get; }

    public string PlayerNickname { get; }

    public string PlayerSurname { get; }

    public int SectorCount { get; }

    public string SharedMemoryVersion { get; }

    public string Track { get; }

    public string WetTyresName { get; }

    public override string ToString()
    {
        return
            $"Static Data Update: ACC Version: {this.AccVersion}, Shared Memory Version: {this.SharedMemoryVersion}, Track: {this.Track}, Car Model: {this.CarModel}, Driver: {this.PlayerDisplayName()}";
    }
}