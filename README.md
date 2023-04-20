[![NuGet](https://img.shields.io/nuget/v/DiagnosticsExtensions.Sentry)](https://www.nuget.org/packages/DiagnosticsExtensions.Sentry/)
[![NuGet](https://img.shields.io/nuget/dt/DiagnosticsExtensions.Sentry)](https://www.nuget.org/packages/DiagnosticsExtensions.Sentry/)

[DiagnosticsExtensions.Sentry](https://github.com/chyccs/sentry-dotnet-diagnostics/)
================

It collects and updates the information that is not automatically collected among the various fields of sentry contexts using `WMI`

## Usage ##

### Using SDK library ###

Just install package in the Nuget

    Install-Package DiagnosticsExtensions.Sentry
    
Let's getting started using the library

### Configurations ###

```c#
        SentryService.Instance.Init(option => {
            option.AddEventProcessor(new DiagnosticsEventProcessor());
            option.AddTransactionProcessor(new DiagnosticsTransactionProcessor());
        });

        Application.Run(new Form1());
```

### Add Breadcrumbs ###

```c#
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SentryService.Instance.AddBreadcrumb("websocket-starting-failed", "middleware", BreadcrumbTypes.ERROR, Sentry.BreadcrumbLevel.Error);
        }
```
