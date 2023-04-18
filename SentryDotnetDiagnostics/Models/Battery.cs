using ORMi;


namespace SentryDotnetDiagnostics.Models
{
    public enum BatteryStatus : ushort
    {
        OTHER = 1,
        UNKNOWN = 2,
        FULLY_CHARGED = 3,
        LOW = 4,
        CRITICAL = 5,
        CHARGING = 6,
        CHARGING_AND_HIGH = 7,
        CHARGING_AND_LOW = 8,
        CHARGING_AND_CRITICAL = 9,
        UNDEFINED = 10,
        PARTIALLY_CHARGED = 11,
    }

    [WMIClass(Name = "Win32_Battery", Namespace = @"root\CIMV2")]
    public class Battery
    {
        public short EstimatedChargeRemaining { get; set; }

        public short BatteryStatus { get; set; }

        [WMIIgnore]
        public BatteryStatus Status { get { return (BatteryStatus)BatteryStatus; } }

        public int TimeOnBattery { get; set; }
    }
}
