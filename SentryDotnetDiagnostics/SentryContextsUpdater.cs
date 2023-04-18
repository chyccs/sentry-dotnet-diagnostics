using ORMi;
using ORMi.Interfaces;
using SentryDotnetDiagnostics.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace SentryDotnetDiagnostics
{
    public class SentryContextsUpdater
    {
        private readonly List<object> models;

        private IWMIHelper helper;

        public IWMIHelper Helper
        {
            get
            {
                if (helper == null)
                {
                    helper = new WMIHelper(@"root\CIMV2");
                }
                return helper;
            }
        }

        private static volatile SentryContextsUpdater instance;
        private static readonly object syncRoot = new object();

        public static SentryContextsUpdater Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new SentryContextsUpdater();
                        }
                    }
                }
                return instance;
            }
        }

        private SentryContextsUpdater()
        {
            models = new List<object>
            {
                Helper.QueryFirstOrDefault<Battery>(),
                Helper.QueryFirstOrDefault<Models.OperatingSystem>(),
                Helper.QueryFirstOrDefault<ComputerSystem>(),
                Helper.QueryFirstOrDefault<ComputerSystemProduct>(),
                Helper.QueryFirstOrDefault<Processor>(),
                Helper.QueryFirstOrDefault<VideoController>(),
                Helper.QueryFirstOrDefault<DesktopMonitor>(),
                Helper.QueryFirstOrDefault<SoundDevice>()
            };
        }

        public T GetModel<T>(bool cached = true)
        {
            if (cached)
            {
                var provider = models.Where(p => p is T).First();
                if (provider != null && provider is T)
                    return (T)provider;
                else
                    return default;
            }
            else
            {
                try
                {
                    return Helper.QueryFirstOrDefault<T>();
                }
                catch
                {
                    return default;
                }
            }
        }

        public void UpdateContext(Sentry.Contexts context, bool cached = true)
        {
            Update(context.App);
            Update(context.Device, cached);
            Update(context.OperatingSystem);
            Update(context.Gpu);
        }

        public void Update(Sentry.Protocol.App app)
        {
            var assembly = Assembly.GetExecutingAssembly().GetName();
            app.Name = assembly.FullName;
            app.Version = $"{assembly.Version}";
        }

        public void Update(Sentry.Protocol.OperatingSystem os)
        {
            var fetched = GetModel<Models.OperatingSystem>();
            os.Build = $"{fetched?.BuildNumber}";
            os.Name = fetched?.Name;
            os.Version = fetched?.Version;
        }

        public void Update(Sentry.Protocol.Device device, bool cached = true)
        {
            var battery = GetModel<Battery>(cached);
            var system = GetModel<ComputerSystem>();
            var product = GetModel<ComputerSystemProduct>();
            var os = GetModel<Models.OperatingSystem>(cached);
            var processor = GetModel<Processor>();
            var video = GetModel<VideoController>();
            var monitor = GetModel<DesktopMonitor>();
            var sound = GetModel<SoundDevice>();

            device.Name = product?.Name;
            device.Manufacturer = system?.Manufacturer;
            device.Brand = product?.Vendor;
            device.Model = system?.Model;
            device.Family = system?.SystemFamily;
            device.DeviceUniqueIdentifier = product?.IdentifyingNumber;
            device.DeviceType = system?.Type.ToString();
            device.Timezone = TimeZoneInfo.Local;

            device.BatteryLevel = battery?.EstimatedChargeRemaining;
            device.IsCharging = battery?.TimeOnBattery == 0;
            device.BatteryStatus = battery?.Status.ToString();

            device.Architecture = os?.OSArchitecture;
            device.IsOnline = true;
            device.BootTime = os?.LastBootUpTime;

            device.ScreenResolution = $"{video?.CurrentHorizontalResolution}*{video?.CurrentVerticalResolution}";
            device.ScreenDpi = monitor != null ? Math.Min(monitor.PixelsPerXLogicalInch, monitor.PixelsPerYLogicalInch) : 0;
            var ori = SystemInformation.ScreenOrientation;
            device.Orientation = ori == ScreenOrientation.Angle270
                || ori == ScreenOrientation.Angle90 ? Sentry.Protocol.DeviceOrientation.Portrait : Sentry.Protocol.DeviceOrientation.Landscape;

            device.FreeMemory = os?.FreePhysicalMemory * 1024;
            device.MemorySize = system?.TotalPhysicalMemory;

            device.ProcessorCount = system?.NumberOfLogicalProcessors;
            device.CpuDescription = processor?.Name;

            device.SupportsAudio = sound?.StatusInfo == 3;
        }

        public void Update(Sentry.Protocol.Gpu gpu)
        {
            var fetched = GetModel<VideoController>();
            if (fetched != null)
            {
                gpu.Name = fetched?.Name;
                gpu.MemorySize = (int)(fetched?.AdapterRAM / 1024 / 1024);
                gpu.Version = fetched?.DriverVersion;
                gpu.VendorName = fetched?.AdapterCompatibility;
            }
        }
    }
}
