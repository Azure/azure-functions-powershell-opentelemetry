//
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

using Microsoft.Extensions.Logging;

namespace OpenTelemetryEngine.Types
{
    public class FunctionsLogger
    {
        public ILogger logger;

        public FunctionsLogger(ILogger logger) {  this.logger = logger; }

        public void Log(object? logItem, string? level, Exception? exception = null) 
        {
            if (Enum.TryParse(typeof(LogLevel), level, out var _logLevel)) {
                if (logItem is not null && _logLevel is not null) {
                    logger.Log((LogLevel)_logLevel, logItem.ToString(), exception);
                }   
                else 
                {
                    throw new ArgumentException("Message and/or level was null when attempting to log");
                }
            }
        }

        public void WorkerLogHandler(string level, string message, Exception exception) 
        {
            Log(message, level, exception);
        }
    }
}
