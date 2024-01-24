using System;

namespace Acc.Lib.SharedMemory;

/// <summary>
/// Some code in this namespace is based on code from https://github.com/RiddleTime/Race-Element, which is in turn based on code from other open source repositories
/// </summary>
public class PhysicsData
{
    public PhysicsData(PhysicsPage physicsPage)
    {
        this.TimeStamp = DateTime.UtcNow;
        this.PacketId = physicsPage.PacketId;
        this.Accelerator = physicsPage.Gas;
        this.Brake = physicsPage.Brake;
        this.Fuel = physicsPage.Fuel;
        this.Gear = physicsPage.Gear;
        this.Rpm = physicsPage.Rpm;
        this.SteerAngle = physicsPage.SteerAngle;
        this.SpeedKmh = physicsPage.SpeedKmh;
        this.Velocity = physicsPage.Velocity;
        this.AccG = physicsPage.AccG;
        this.WheelSlip = physicsPage.WheelSlip;
        this.WheelPressure = physicsPage.WheelPressure;
        this.WheelAngularSpeed = physicsPage.WheelAngularSpeed;
        this.TyreCoreTemperature = physicsPage.TyreCoreTemperature;
        this.SuspensionTravel = physicsPage.SuspensionTravel;
        this.TractionControl = physicsPage.TC;
        this.Heading = physicsPage.Heading;
        this.Pitch = physicsPage.Pitch;
        this.Roll = physicsPage.Roll;
        this.CarDamage = physicsPage.CarDamage;
        this.PitLimiterOn = physicsPage.PitLimiterOn;
        this.Abs = physicsPage.Abs;
        this.AutoShiftOn = physicsPage.AutoShifterOn;
        this.TurboBoost = physicsPage.TurboBoost;
        this.AirTemp = physicsPage.AirTemp;
        this.TrackTemp = physicsPage.RoadTemp;
        this.LocalAngularVelocity = physicsPage.LocalAngularVelocity;
        this.FinalFf = physicsPage.finalFF;
        this.BrakeTemperature = physicsPage.BrakeTemperature;
        this.Clutch = physicsPage.Clutch;
        this.IsAiControlled = physicsPage.IsAiControlled;
        this.TyreContactPoint = physicsPage.TyreContactPoint;
        this.TyreContactNormal = physicsPage.TyreContactNormal;
        this.TyreContactHeading = physicsPage.TyreContactHeading;
        this.BrakeBias = physicsPage.BrakeBias;
        this.LocalVelocity = physicsPage.LocalVelocity;
        this.SlipRatio = physicsPage.SlipRatio;
        this.SlipAngle = physicsPage.SlipAngle;
        this.SuspensionDamage = physicsPage.SuspensionDamage;
        this.TyreTemp = physicsPage.TyreTemp;
        this.WaterTemp = physicsPage.WaterTemp;
        this.BrakePressure = physicsPage.brakePressure;
        this.FrontBrakeCompound = physicsPage.frontBrakeCompound;
        this.RearBrakeCompound = physicsPage.rearBrakeCompound;
        this.PadLife = physicsPage.PadLife;
        this.DiscLife = physicsPage.DiscLife;
        this.IgnitionOn = physicsPage.IgnitionOn;
        this.StarterEngineOn = physicsPage.StarterEngineOn;
        this.IsEngineRunning = physicsPage.IsEngineRunning;
        this.KerbVibration = physicsPage.KerbVibration;
        this.SlipVibrations = physicsPage.SlipVibrations;
        this.GearVibrations = physicsPage.Gvibrations;
        this.AbsVibrations = physicsPage.AbsVibrations;
    }

    public DateTime TimeStamp { get; }

    public float Abs { get; set; }

    public float AbsVibrations { get; set; }

    public float Accelerator { get; set; }

    public float[] AccG { get; set; }

    public float AirTemp { get; set; }

    public bool AutoShiftOn { get; set; }

    public float Brake { get; set; }

    public float BrakeBias { get; set; }

    public float[] BrakePressure { get; set; }

    public float[] BrakeTemperature { get; set; }

    public float[] CarDamage { get; set; }

    public float Clutch { get; set; }

    public float[] DiscLife { get; set; }

    public float FinalFf { get; set; }

    public int FrontBrakeCompound { get; set; }

    public float Fuel { get; set; }

    public int Gear { get; set; }

    public float GearVibrations { get; set; }

    public float Heading { get; set; }

    public bool IgnitionOn { get; set; }

    public bool IsAiControlled { get; set; }

    public bool IsEngineRunning { get; set; }

    public float KerbVibration { get; set; }

    public float[] LocalAngularVelocity { get; set; }

    public float[] LocalVelocity { get; set; }

    public int PacketId { get; set; }

    public float[] PadLife { get; set; }

    public float Pitch { get; set; }

    public bool PitLimiterOn { get; set; }

    public int RearBrakeCompound { get; set; }

    public float Roll { get; set; }

    public int Rpm { get; set; }

    public float[] SlipAngle { get; set; }

    public float[] SlipRatio { get; set; }

    public float SlipVibrations { get; set; }

    public float SpeedKmh { get; set; }

    public bool StarterEngineOn { get; set; }

    public float SteerAngle { get; set; }

    public float[] SuspensionDamage { get; set; }

    public float[] SuspensionTravel { get; set; }

    public float TrackTemp { get; set; }

    public float TractionControl { get; set; }

    public float TurboBoost { get; set; }

    public AccRtVector3d[] TyreContactHeading { get; set; }

    public AccRtVector3d[] TyreContactNormal { get; set; }

    public AccRtVector3d[] TyreContactPoint { get; set; }

    public float[] TyreCoreTemperature { get; set; }

    public float[] TyreTemp { get; set; }

    public float[] Velocity { get; set; }

    public float WaterTemp { get; set; }

    public float[] WheelAngularSpeed { get; set; }

    public float[] WheelPressure { get; set; }

    public float[] WheelSlip { get; set; }

    public override string ToString()
    {
        return
            $"Physics Data Update: Time Stamp: {this.TimeStamp:hh:mm:ss.ffff}, Accelerator: {this.Accelerator}, Brake: {this.Brake}, Fuel: {this.Fuel}, Gear: {this.Gear}, RPM: {this.Rpm}, Steering Angle: {this.SteerAngle}, Speed KMH: {this.SpeedKmh}, X: {this.TyreContactPoint[0].X}, Y: {this.TyreContactPoint[0].Z}";
    }
}