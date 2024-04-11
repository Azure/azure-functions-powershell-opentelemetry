//
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

using System.Diagnostics;
using OpenTelemetryEngine.Constants;
using OpenTelemetryEngine.ResponseObjects;

namespace OpenTelemetryEngine.Traces
{ 
    public class FunctionsActivityBuilder
    {
        public static ActivitySource sourceInternal = new ActivitySource("AzureFunctionsInternal");
        public static ActivitySource source = new ActivitySource(OpenTelemetryModuleConstants.ActivitySourceName);

        private static Dictionary<string, Activity> internalActivitiesByInvocationId = new Dictionary<string, Activity>();

        public static FunctionsActivityResponse StartInternalActivity(string invocationId, string traceParent, string traceState) 
        {
            Activity? activity = sourceInternal.StartActivity("InternalActivity");

            if (activity == null) 
            {
                Console.WriteLine("WARNING: The InternalActivity was null, logs and spans generated in user code may not link properly to host telemetry");
                return new FunctionsActivityResponse(null);
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

            return new FunctionsActivityResponse(activity);
        }

        public static void StopInternalActivity(string invocationId)
        {
            if (internalActivitiesByInvocationId.Keys.Contains(invocationId)) 
            {
                internalActivitiesByInvocationId[invocationId].Stop();
            }
        }

        public static FunctionsActivityResponse StartActivity(string? activityName)
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
                foreach (var tag in activity.Parent.Tags) 
                {
                    activity.AddTag(tag.Key, tag.Value);
                }
            }

            return new FunctionsActivityResponse(activity);
        }

        public static void StopActivity(FunctionsActivityResponse? activity) 
        {
            activity?.activity?.Stop();
        }
    }
}

