using Sentry;
using Sentry.Extensibility;

namespace SentryDotnetDiagnostics
{
    public class DiagnosticsEventProcessor : ISentryEventProcessor
    {
        public SentryEvent Process(SentryEvent @event)
        {
            SentryContextsUpdater.Instance.UpdateContext(@event.Contexts, false);
            return @event;
        }
    }

    public class DiagnosticsTransactionProcessor : ISentryTransactionProcessor
    {
        public Transaction Process(Transaction transaction)
        {
            SentryContextsUpdater.Instance.UpdateContext(transaction.Contexts);
            return transaction;
        }
    }
}
