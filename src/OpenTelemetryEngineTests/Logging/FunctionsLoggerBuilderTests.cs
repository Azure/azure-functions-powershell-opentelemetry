//
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

using OpenTelemetryEngine.Logging;

namespace OpenTelemetryEngineTests.Logging
{
    public class FunctionsLoggerBuilderTests
    {
        [Fact]
        public void FunctionsLoggerBuilder_ReturnsValidLogger()
        {
            var logger = FunctionsLoggerBuilder.GetLogger();

            Assert.NotNull(logger);
        }
    }
}
