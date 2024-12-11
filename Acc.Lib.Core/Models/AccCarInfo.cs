using System;

namespace Acc.Lib.Core.Models;

public record AccCarInfo
{
    public string AccName { get; init; } = string.Empty;
    public string Class { get; init; } = string.Empty;
    public string DisplayName { get; init; } = string.Empty;
    public string ManufacturerTag { get; init; } = string.Empty;
    public int MaxFuel { get; init; }
    public int MaxRpm { get; init; }
    public byte ModelId { get; init; }
    public int Year { get; init; }
}