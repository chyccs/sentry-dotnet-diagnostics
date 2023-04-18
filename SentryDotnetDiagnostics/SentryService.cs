using Sentry;
using System;
using System.Collections.Generic;

namespace SentryDotnetDiagnostics
{
    public class SentryService : IApplicationTrackingServiceProvider<SentryOptions>
    {
        private static volatile SentryService instance;
        private static readonly object syncRoot = new object();

        private SentryService()
        {
        }

        public static SentryService Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new SentryService();
                        }
                    }
                }
                return instance;
            }
        }

        public void SetUserContext(string id, string email, string name)
        {
            SentrySdk.ConfigureScope(scope =>
            {
                scope.User = new User
                {
                    Id = id,
                    Username = name,
                    Email = email,
                    IpAddress = "{{auto}}"
                };
            });
        }

        public void Init(Action<SentryOptions> configure, bool setDiagnosisContext = true)
        {
            SentrySdk.Init(configure);
            if (setDiagnosisContext)
                SentrySdk.ConfigureScope(scope => SentryContextsUpdater.Instance.UpdateContext(scope.Contexts));
        }

        public void RaiseException()
        {
            int i2 = 0;
            int i = 10 / i2;
        }

        public void Capture(Exception e, Dictionary<string, string> extra = null)
        {
            SentrySdk.ConfigureScope(scope => {
                scope.Contexts["extra"] = extra;
            });
            SentrySdk.CaptureException(e);
        }

        public void Capture(string message, SentryLevel level = SentryLevel.Warning, Dictionary<string, string> extra = null)
        {
            SentrySdk.ConfigureScope(scope => {
                scope.Contexts["extra"] = extra;
            });
            SentrySdk.CaptureMessage(message, level: level);
        }

        public void AddBreadcrumb(string message, string category = null, BreadcrumbTypes type = BreadcrumbTypes.DEFAULT, BreadcrumbLevel level = BreadcrumbLevel.Info, Dictionary<string, string> extra = null)
        {
            SentrySdk.AddBreadcrumb(message, category, type.ToString().ToLower(), extra, level);
        }

        public void AddNaviBreadcrumb(string message, string from, string to)
        {
            SentrySdk.AddBreadcrumb(message, BreadcrumbTypes.NAVIGATION.ToString().ToLower(), BreadcrumbTypes.NAVIGATION.ToString().ToLower(), new Dictionary<string, string>
            {
                ["from"] = from,
                ["to"] = to
            }, BreadcrumbLevel.Info);
        }

        public void AddClickBreadcrumb(string message, Dictionary<string, string> extra = null)
        {
            SentrySdk.AddBreadcrumb(message, "click", BreadcrumbTypes.USER.ToString().ToLower(), data: extra);
        }

        public void AddHttpBreadcrumb(string message, string url = null, string method = null, string status_code = null, string reason = null)
        {
            SentrySdk.AddBreadcrumb(message, null, BreadcrumbTypes.HTTP.ToString().ToLower(), new Dictionary<string, string>
            {
                ["url"] = url,
                ["method"] = method,
                ["status_code"] = status_code,
                ["reason"] = reason,
            }, BreadcrumbLevel.Info);
        }

        public ITransaction StartTransaction(string name, string operation = null)
        {
            var transaction = SentrySdk.StartTransaction(name, operation ?? name);
            SentrySdk.ConfigureScope(scope => scope.Transaction = transaction);
            return transaction;
        }

        public ISpan StartChild(ITransaction transaction, string operation, string description = null)
        {
            return transaction.StartChild(operation, description);
        }

        public void SetContext(string key, Contexts context)
        {
            SentrySdk.ConfigureScope(scope =>
            {
                scope.Contexts[key] = context;
            });
        }

        public void SetTag(string key, string value)
        {
            SentrySdk.ConfigureScope(scope =>
            {
                scope.SetTag(key, value);
            });
        }
    }

    public enum BreadcrumbTypes
    {
        DEFAULT,
        DEBUG,
        ERROR,
        NAVIGATION,
        HTTP,
        INFO,
        QUERY,
        USER,
    }
}
