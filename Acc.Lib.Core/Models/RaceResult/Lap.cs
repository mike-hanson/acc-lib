﻿using System;

namespace Acc.Lib.Core.Models.RaceResult;

public class Lap
{
	public string Sector1Time =>
		this.Splits[0]
		    .ToTimingString();
	public string Sector2Time =>
		this.Splits[1]
		    .ToTimingString();
	public string Sector3Time =>
		this.Splits[2]
		    .ToTimingString();
	public string Timestamp => this.TimestampMS.ToTimingString();
	public int CarId { get; set; }
	public int DriverId { get; set; }
	public int Flags { get; set; }
	public double Fuel { get; set; }
	public long LapTime { get; set; }
	public List<long> Splits { get; set; }
	public double TimestampMS { get; set; }

	public string GetLapTime()
	{
		return this.LapTime.ValidatedValue()
		           .ToTimingString();
	}
}