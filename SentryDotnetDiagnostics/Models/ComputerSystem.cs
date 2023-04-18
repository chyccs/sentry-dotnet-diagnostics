using ORMi;

namespace SentryDotnetDiagnostics.Models
{
    public enum PCSystemType : ushort
    {
        UNSPECIFIED = 0,
        DESKTOP = 1,
        MOBILE = 2,
        WORKSTATION = 3,
        ENTERPRISE_SERVER = 4,
        SOHO_SERVER = 5,
        APPLIANCE_PC = 6,
        PERFORMANCE_SERVER = 7,
        MAXIMUM = 8,
    }

    [WMIClass(Name = "Win32_ComputerSystem", Namespace = @"root\CIMV2")]
    public class ComputerSystem
    {
        public string Model { get; set; }

        public string Manufacturer { get; set; }

        public string SystemFamily { get; set; }

        public string SystemType { get; set; }

        public long TotalPhysicalMemory { get; set; }

        public int NumberOfLogicalProcessors { get; set; }

        public int NumberOfProcessors { get; set; }

        public short PCSystemType { get; set; }

        [WMIIgnore]
        public PCSystemType Type { get { return (PCSystemType)PCSystemType; } }
    }
}
