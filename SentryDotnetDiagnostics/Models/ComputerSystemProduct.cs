using ORMi;

namespace SentryDotnetDiagnostics.Models
{
    [WMIClass(Name = "Win32_ComputerSystemProduct", Namespace = @"root\CIMV2")]
    public class ComputerSystemProduct
    {
        [WMIProperty("Name")]
        public string Name { get; set; }

        [WMIProperty("UUID")]
        public string UUID { get; set; }

        [WMIProperty("Vendor")]
        public string Vendor { get; set; }

        [WMIProperty("Version")]
        public string Version { get; set; }

        [WMIProperty("IdentifyingNumber")]
        public string IdentifyingNumber { get; set; }

        [WMIProperty("Description")]
        public string Description { get; set; }

        [WMIProperty("Caption")]
        public string Caption { get; set; }
    }
}
