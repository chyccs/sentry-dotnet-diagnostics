using ORMi;

namespace SentryDotnetDiagnostics.Models
{
    [WMIClass("Win32_VideoController")]
    public class VideoController : WMIInstance
    {
        public string Name { get; set; }

        public string AdapterCompatibility { get; set; }

        public string AdapterDACType { get; set; }

        public long AdapterRAM { get; set; }

        public int Availability { get; set; }

        public string Caption { get; set; }

        public int CurrentHorizontalResolution { get; set; }

        public int CurrentVerticalResolution { get; set; }

        public int CurrentRefreshRate { get; set; }

        public int CurrentScanMode { get; set; }

        public string Description { get; set; }

        public string DitherType { get; set; }

        public string DriverVersion { get; set; }

        public long MaxMemorySupported { get; set; }

        public int MaxRefreshRate { get; set; }

        public int MinRefreshRate { get; set; }

        public bool Monochrome { get; set; }

        public int VideoArchitecture { get; set; }

        public int VideoMemoryType { get; set; }

        public string VideoProcessor { get; set; }
    }
}
