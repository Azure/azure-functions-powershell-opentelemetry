using System.Collections.Immutable;
using System.Diagnostics;
using OpenTelemetry;

namespace OpenTelemetryEngine.Traces
{
    internal class TraceFilterProcessor : BaseProcessor<Activity>
    {
        private TraceFilterProcessor() { }

        public static TraceFilterProcessor Instance { get; } = new TraceFilterProcessor();

        public override void OnEnd(Activity data)
        {
            DropInternalActivities(data);

            if (data.ActivityTraceFlags != ActivityTraceFlags.None)
            {
                // Console.WriteLine("Didn't drop Activity: {0}", data.DisplayName);
            }

            base.OnEnd(data);
        }

        private void DropInternalActivities(Activity data)
        {
            if (data.ActivityTraceFlags is ActivityTraceFlags.Recorded)
            {
                if (data.DisplayName == "InternalActivity" && data.Source.Name == "AzureFunctionsInternal")  
                {
                    data.ActivityTraceFlags = ActivityTraceFlags.None;
                    // Console.WriteLine("Dropped Activity: {0}", data.DisplayName);
                }
            }
        }
    }
}