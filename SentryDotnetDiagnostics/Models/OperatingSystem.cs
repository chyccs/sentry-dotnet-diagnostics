using ORMi;
using System;

namespace SentryDotnetDiagnostics.Models
{
    [WMIClass(Name = "Win32_OperatingSystem", Namespace = "root\\CimV2")]
    public class OperatingSystem
    {
        [WMIProperty("Caption")]
        public string Name { get; set; }

        public string Manufacturer { get; set; }

        public string Version { get; set; }

        public int BuildNumber { get; set; }

        public short CountryCode { get; set; }

        public long FreePhysicalMemory { get; set; }

        public long FreeSpaceInPagingFiles { get; set; }

        public long FreeVirtualMemory { get; set; }

        public long MaxNumberOfProcesses { get; set; }

        public long MaxProcessMemorySize { get; set; }

        public DateTime InstallDate { get; set; }

        public DateTime LastBootUpTime { get; private set; }

        public string[] MUILanguages { get; set; }

        public short NumberOfProcesses { get; set; }

        public short NumberOfUsers { get; set; }

        public short OperatingSystemSKU { get; set; }

        public string OSArchitecture { get; set; }

        public string SerialNumber { get; set; }

        public long SizeStoredInPagingFiles { get; set; }

        public string SystemDrive { get; set; }

        public string SystemDirectory { get; set; }

        public string WindowsDirectory { get; set; }

        public long TotalVirtualMemorySize { get; set; }

        public long TotalVisibleMemorySize { get; set; }
    }
}
