using System;
using System.Collections.Generic;

using Sentry;

namespace SentryDotnetDiagnostics
{
    public interface IApplicationTrackingServiceProvider<T>
    {
        void Init(Action<T> configure, bool setDiagnosisContext = true);

        void SetUserContext(string id, string email, string name);

        void Capture(Exception exception, Dictionary<string, string> extra = null);

        void Capture(string message, SentryLevel level = SentryLevel.Error, Dictionary<string, string> extra = null);
    }
}
