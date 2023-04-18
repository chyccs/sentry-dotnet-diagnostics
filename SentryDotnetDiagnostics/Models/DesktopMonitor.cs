using ORMi;

namespace SentryDotnetDiagnostics.Models
{
    [WMIClass("Win32_DesktopMonitor")]
    public class DesktopMonitor
    {
        public int PixelsPerXLogicalInch { get; set; }

        public int PixelsPerYLogicalInch { get; set; }
    }
}
