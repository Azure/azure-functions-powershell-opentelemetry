//
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

using OpenTelemetryEngine.Traces;

namespace OpenTelemetryEngineTests.Traces
{
    public class FunctionsTracerBuilderTests
    {
        [Fact]
        public void TestFunctionsTracerBuilder()
        {
            var tracerBuilderResponse = FunctionsTracerBuilder.BuildTracer(new List<string>() { "AnActivitySource" });

            Assert.NotNull(tracerBuilderResponse);
        }
    }
}
