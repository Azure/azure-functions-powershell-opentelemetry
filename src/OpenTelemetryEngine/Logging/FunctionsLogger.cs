//
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

using Microsoft.Extensions.Logging;
using OpenTelemetryEngine.ResponseObjects;

namespace OpenTelemetryEngine.Types
{
    public class FunctionsLogger
    {
        public ILogger logger;

        public FunctionsLogger(ILogger logger) {  this.logger = logger; }

        public void Log(object? logItem, string? level, Exception? exception = null) 
        {
            object? logLevelParsed;
            if (!Enum.TryParse(typeof(LogLevel), level, out logLevelParsed)) 
            {
                throw new ArgumentException("Log level was not a valid log level");
            }

            if (logItem is null) 
            {
                throw new ArgumentException("Message was null when attempting to log");
            }
            
            if (logLevelParsed is null) 
            {
                throw new ArgumentException("Log level was null when attempting to log");
            }
            
            logger.Log((LogLevel)logLevelParsed, logItem.ToString(), exception);
        }

        public void WorkerLogHandler(string level, string message, Exception exception) 
        {
            Log(message, level, exception);
        }

        public FunctionsLoggerResponse BuildResponse() 
        {
            return new FunctionsLoggerResponse(logger);
        }
    }
}
