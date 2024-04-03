//
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

using System.Diagnostics;
using OpenTelemetryEngine.Types;

namespace OpenTelemetryEngine.Traces
{ 
    public class FunctionsActivityBuilder
    {
        public static ActivitySource sourceInternal = new ActivitySource("AzureFunctionsInternal");
        public static ActivitySource source = new ActivitySource("AzureFunctions");

        private static Dictionary<string, Activity> internalActivitiesByInvocationId = new Dictionary<string, Activity>();

        public static FunctionsActivity StartInternalActivity(string invocationId, string traceParent, string traceState) 
        {
            Activity? activity = sourceInternal.StartActivity("InternalActivity");

            if (activity == null) 
            {
                Console.WriteLine("WARNING: The InternalActivity was null, logs and spans generated in user code may not link properly to host telemetry");
                return new FunctionsActivity(null);
            }

            activity.AddTag("invocationId", invocationId);

            if (ActivityContext.TryParse(traceParent, traceState, true, out ActivityContext activityContext))
            {
                activity.SetId(traceParent);
                activity.SetSpanId(activityContext.SpanId.ToString());
                activity.SetTraceId(activityContext.TraceId.ToString());
                activity.SetRootId(activityContext.TraceId.ToString());
            }

            internalActivitiesByInvocationId.Add(invocationId, activity);

            return new FunctionsActivity(activity);
        }

        public static void StopInternalActivity(string invocationId)
        {
            if (internalActivitiesByInvocationId.Keys.Contains(invocationId)) 
            {
                internalActivitiesByInvocationId[invocationId].Stop();
            }
        }

        public static FunctionsActivity StartActivity(string? activityName)
        {
            Activity? activity;
            if (!string.IsNullOrEmpty(activityName))
            {
                activity = source.StartActivity(activityName);
            }
            else
            {
                activity = source.StartActivity();
            }

            if (activity is not null && activity.Parent is not null) 
            {
                // This is so that the invocation ID and other tags manually added to the InternalActivity will propogate to user activities. 
                // TODO: Evaluate whether we should attach all tags from all parent spans to all child spans, or limit scope a bit more
                foreach (var tag in activity.Parent.Tags) 
                {
                    activity.AddTag(tag.Key, tag.Value);
                }
            }

            return new FunctionsActivity(activity);
        }

        public static void StopActivity(FunctionsActivity? activity) 
        {
            activity?.activity?.Stop();
        }
    }
}

