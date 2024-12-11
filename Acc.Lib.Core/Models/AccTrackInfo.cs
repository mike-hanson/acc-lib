using System;

namespace Acc.Lib.Core.Models;

public record AccTrackInfo
{
    public int Corners { get; set; }
    public string CountryTag { get; set; } = string.Empty;
    public double Latitude { get; set; }
    public int Length { get; set; }
    public double Longitude { get; set; }
    public string Name { get; set; } = string.Empty;
    public string TrackId { get; set; } = string.Empty;
}