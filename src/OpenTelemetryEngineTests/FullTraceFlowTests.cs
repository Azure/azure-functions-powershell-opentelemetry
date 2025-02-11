//
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

using OpenTelemetryEngine.Logging;
using OpenTelemetryEngine.Traces;

namespace OpenTelemetryEngineTests
{
    public class FullTraceFlowTests
    {
        [Fact]
        public void FullyTracedCode_ExecutesSuccessfully() 
        {
            var _ = FunctionsTracerBuilder.BuildTracer(new List<string>());
            var loggerBuilderResponse = FunctionsLoggerBuilder.GetLogger();

            string internalActivityId = Guid.NewGuid().ToString();

            var internalActivity = FunctionsActivityBuilder.StartInternalActivity(internalActivityId, "", "");

            var userActivity = FunctionsActivityBuilder.StartActivity("MyActivity");

            loggerBuilderResponse.Log("A log item", "Information");

            FunctionsActivityBuilder.StopActivity(userActivity);

            FunctionsActivityBuilder.StopInternalActivity(internalActivityId);
        }
    }
}
