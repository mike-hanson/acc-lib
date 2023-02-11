using System;

namespace Acc.Lib.Broadcasting.Messages;

public class CarInfo
{
  public CarInfo(ushort carIndex)
  {
    this.CarIndex = carIndex;
  }

  public ushort CarIndex { get; }
  public IList<DriverInfo> Drivers { get; } = new List<DriverInfo>();
  public byte CarModelType { get; internal set; }
  public byte CupCategory { get; internal set; }
  public int CurrentDriverIndex { get; internal set; }
  public Nationality Nationality { get; internal set; }
  public int RaceNumber { get; internal set; }
  public string TeamName { get; internal set; }

  public string GetCurrentDriverDisplayName()
  {
    return this.CurrentDriverIndex >= this.Drivers.Count
             ? "nobody(?)"
             : this.Drivers[this.CurrentDriverIndex]
                   .InitialAndLastName;
  }

  public override string ToString()
  {
      return !this.Drivers.Any()? $"Race Number: {this.RaceNumber}, Team: {this.TeamName}": $"Race Number: {this.RaceNumber}, Driver: {this.Drivers[this.CurrentDriverIndex].InitialAndLastName}, Team: {this.TeamName}";
  }

  internal void AddDriver(DriverInfo driverInfo)
  {
    this.Drivers.Add(driverInfo);
  }
}