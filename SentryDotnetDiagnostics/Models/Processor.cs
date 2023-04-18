using ORMi;
using System;

namespace SentryDotnetDiagnostics.Models
{
    [WMIClass("Win32_Processor", Namespace = "root\\CimV2")]
    public class Processor : WMIInstance
    {
        public string Name { get; set; }

        public string AddressWidth { get; set; }

        public string Architecture { get; set; }

        public string AssetTag { get; set; }

        public string Availability { get; set; }

        public string Caption { get; set; }

        public string Characteristics { get; set; }

        public string CpuStatus { get; set; }

        public int CurrentClockSpeed { get; set; }

        public int CurrentVoltage { get; set; }

        public int DataWidth { get; set; }

        public string Description { get; set; }

        public string DeviceID { get; set; }

        public int ExtClock { get; set; }

        public string Family { get; set; }

        public DateTime InstallDate { get; set; }

        public int L2CacheSize { get; set; }

        public int L2CacheSpeed { get; set; }

        public int L3CacheSize { get; set; }

        public int L3CacheSpeed { get; set; }

        public string Manufacturer { get; set; }

        public int MaxClockSpeed { get; set; }

        public int NumberOfCores { get; set; }

        public int NumberOfEnabledCore { get; set; }

        public int NumberOfLogicalProcessors { get; set; }

        public string PartNumber { get; set; }

        public bool PowerManagementSupported { get; set; }

        public string ProcessorId { get; set; }

        public string ProcessorType { get; set; }

        public string Revision { get; set; }

        public string Role { get; set; }

        public bool SecondLevelAddressTranslationExtensions { get; set; }

        public string SerialNumber { get; set; }

        public string SocketDesignation { get; set; }

        public string Status { get; set; }

        public string StatusInfo { get; set; }

        public int ThreadCount { get; set; }

        public int UpgradeMethod { get; set; }

        public string Version { get; set; }

        public bool VirtualizationFirmwareEnabled { get; set; }

        public bool VMMonitorModeExtensions { get; set; }
    }
}
