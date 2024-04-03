//
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

using OpenTelemetry;
using OpenTelemetry.Trace;
using OpenTelemetryEngine.Logging;

namespace OpenTelemetryEngine.Traces
{
    public static class FunctionsTracerBuilder
    {
        public static void BuildTracer(List<string> AdditionalSources, string b)
        {
            var tracerBuilder = Sdk.CreateTracerProviderBuilder()
                .AddSource("AzureFunctions")
                .AddSource("AzureFunctionsInternal");

            foreach (string source in AdditionalSources)
            {
                tracerBuilder.AddSource(source);
            }

            tracerBuilder
                .ConfigureResource(x => x.AddDetector(new FunctionsResourceDetector()))
                .AddProcessor(TraceFilterProcessor.Instance)
                .AddOtlpExporter()
                .Build();
        }
    }
}
