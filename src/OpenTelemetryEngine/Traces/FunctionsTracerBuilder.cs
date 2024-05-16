//
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

using OpenTelemetry;
using OpenTelemetry.Trace;
using OpenTelemetryEngine.Constants;
using OpenTelemetryEngine.Resources;
using OpenTelemetryEngine.ResponseObjects;

namespace OpenTelemetryEngine.Traces
{
    public static class FunctionsTracerBuilder
    {
        public static FunctionsTracerBuilderResponse BuildTracer(List<string> AdditionalSources)
        {
            var tracerBuilder = Sdk.CreateTracerProviderBuilder()
                .AddSource(OpenTelemetryModuleConstants.ActivitySourceName)
                .AddSource(OpenTelemetryModuleConstants.InternalActivitySourceName);

            foreach (string source in AdditionalSources)
            {
                tracerBuilder.AddSource(source);
            }

            tracerBuilder
                .ConfigureResource(x => x.AddDetector(new FunctionsResourceDetector()))
                .AddProcessor(TraceFilterProcessor.Instance)
                .AddOtlpExporter()
                .Build();

            return new FunctionsTracerBuilderResponse();
        }
    }
}
