//
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

using System.Collections.Concurrent;
using System.Diagnostics;
using OpenTelemetryEngine.Constants;
using OpenTelemetryEngine.ResponseObjects;

namespace OpenTelemetryEngine.Traces
{ 
    public class FunctionsActivityBuilder
    {
        public static ActivitySource sourceInternal = new ActivitySource("AzureFunctionsInternal");
        public static ActivitySource source = new ActivitySource(OpenTelemetryModuleConstants.ActivitySourceName);

        private static ConcurrentDictionary<string, Activity> internalActivitiesByInvocationId = new ConcurrentDictionary<string, Activity>();

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

            if(!internalActivitiesByInvocationId.TryAdd(invocationId, activity)) 
            {
                Console.WriteLine("WARNING: An activity for this InvocationId already exists, stopping this one");
                activity.Stop();
                return new FunctionsActivityResponse(null);
            }

            return new FunctionsActivityResponse(activity);
        }

        public static void StopInternalActivity(string invocationId)
        {
            if (internalActivitiesByInvocationId.Keys.Contains(invocationId)) 
            {
                if (internalActivitiesByInvocationId.TryRemove(invocationId, out Activity? activityToStop))
                {
                    activityToStop.Stop();
                }
                else 
                {
                    // There are two reasons why the activity may not be removed from the dictionary:
                    // 1. The activity was added successfully to the dictionary but was already removed from the dictionary by another thread
                    //    Due to the architecture of Azure Functions, this is not likely unless the user calls these cmdlets manually
                    //    If this happens, the activity was already stopped and the user should not be concerned
                    // 2. The activity was not added successfully to the dictionary
                    //    This is also not a concern - this only happens if the key already exists and we guard this case in StartInternalActivity
                }
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

        public static void StopActivity(FunctionsActivityResponse? response) 
        {
            response?.activity?.Stop();
        }

        public static GetActivityResponse GetActivityForInvocation(string invocationId)
        {
            if (internalActivitiesByInvocationId.ContainsKey(invocationId))
            {
                return new GetActivityResponse(internalActivitiesByInvocationId[invocationId]);
            }
            return new GetActivityResponse(Activity.Current);
        }
    }
}

