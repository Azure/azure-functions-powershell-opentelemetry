//
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

using OpenTelemetryEngine.Logging;

namespace OpenTelemetryEngineTests.Logging
{
    public class FunctionsLoggerTests
    {
        [Fact]
        public void FunctionsLogger_CanLog()
        {
            var logger = FunctionsLoggerBuilder.GetLogger();

            logger.Log("This is a debug log", "Debug");
            logger.Log("This is an information log", "Information");
            logger.Log("This is a warning log", "Warning");
            logger.Log("This is an error log", "Error");
        }

        [Fact]
        public void FunctionsLogger_CanLogWithWorkerHandler()
        {
            var logger = FunctionsLoggerBuilder.GetLogger();

            logger.WorkerLogHandler("Debug", "This is a debug log", null);
            logger.WorkerLogHandler("Information", "This is an information log", null);
            logger.WorkerLogHandler("Warning", "This is a warning log", null);
            logger.WorkerLogHandler("Error", "This is an error log", null);
        }

        [Fact]
        public void FunctionsLogger_InvalidLogTypesThrow()
        {
            var logger = FunctionsLoggerBuilder.GetLogger();

            Assert.Throws<System.ArgumentException>(() => logger.WorkerLogHandler("NotALogCategory", "This is an invalid log", null));
            Assert.Throws<System.ArgumentException>(() => logger.Log("This is an invalid log", "NotALogCategory"));
        }

        [Fact]
        public void FunctionsLogger_NullLogMessageThrows()
        {
            var logger = FunctionsLoggerBuilder.GetLogger();

            Assert.Throws<System.ArgumentException>(() => logger.WorkerLogHandler("Warning", null, null));
            Assert.Throws<System.ArgumentException>(() => logger.Log(null, "Warning"));
        }
    }
}