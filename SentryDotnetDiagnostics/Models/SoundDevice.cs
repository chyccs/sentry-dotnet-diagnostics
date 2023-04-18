using ORMi;

namespace SentryDotnetDiagnostics.Models
{
    [WMIClass(Name = "Win32_SoundDevice", Namespace = "root\\CimV2")]
    public class SoundDevice
    {
        public string DeviceID { get; set; }

        public string Name { get; set; }

        public string Caption { get; set; }

        public string Description { get; set; }

        public string Manufacturer { get; set; }

        public string Status { get; set; }

        public int StatusInfo { get; set; }

        public void RenamePrinter(string newName)
        {
            WMIMethod.ExecuteMethod(this, new { NewPrinterName = newName });
        }

    }
}
