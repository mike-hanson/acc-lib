using System;

namespace Acc.Lib.Broadcasting.Messages;

public class LapInfo
{
    internal void UpdateFromReader(BinaryReader binaryReader)
    {
        this.LapTimeMs = binaryReader.ReadInt32();
        this.CarIndex = binaryReader.ReadUInt16();
        this.DriverIndex = binaryReader.ReadUInt16();
        this.SplitCount = binaryReader.ReadByte();
        for(var i = 0; i < this.SplitCount; i++)
        {
            this.Splits[i] = binaryReader.ReadInt32();
        }

        this.IsInvalid = binaryReader.ReadByte() > 0;
        this.IsValidForBest = binaryReader.ReadByte() > 0;

        var isOutLap = binaryReader.ReadByte() > 0;
        var isInLap = binaryReader.ReadByte() > 0;

        if(isOutLap)
        {
            this.LapType = LapType.Outlap;
        }
        else if(isInLap)
        {
            this.LapType = LapType.Inlap;
        }
        else
        {
            this.LapType = LapType.Regular;
        }

        
        // "null" entries are Int32.Max, in the C# world we can replace this to null
        for(var i = 0; i < this.Splits.Count; i++)
        {
            if(this.Splits[i] == int.MaxValue)
            {
                this.Splits[i] = 0;
            }
        }

        if(this.LapTimeMs == int.MaxValue)
        {
            this.LapTimeMs = 0;
        }
    }

    public byte SplitCount { get; set; }
    public int LapTimeMs { get; set; }
    public List<int> Splits { get; } = new() {0, 0, 0};
    public ushort CarIndex { get; internal set; }
    public ushort DriverIndex { get; internal set; }
    public bool IsInvalid { get; internal set; }
    public bool IsValidForBest { get; internal set; }
    public LapType LapType { get; internal set; }

    public override string ToString()
    {
        return $"{this.LapTimeMs,5}|{string.Join("|", this.Splits)} Split Count: {this.SplitCount} Invalid: {this.IsInvalid}";
    }
}