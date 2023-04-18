using ORMi;
using System;

namespace SentryDotnetDiagnostics.Models
{
    [WMIClass("Win32_PhysicalMemory")]
    public class Memory
    {
        [WMIProperty("Name")]
        public string Name { get; set; }

        [WMIProperty("Description")]
        public string Description { get; set; }

        [WMIProperty("EstimatedChargeRemaining")]
        public short BatteryLevel { get; set; }

        [WMIProperty("BatteryStatus")]
        public short Status { get; set; }

        public string BatteryStatus => this.Status + "";

        public bool IsCharging { get { return true; } }

        public void Update(Sentry.Protocol.Device device)
        {
            device.BatteryLevel = this.BatteryLevel;
            device.IsCharging = this.IsCharging;
            device.BatteryStatus = this.BatteryStatus;
        }
    }
}
