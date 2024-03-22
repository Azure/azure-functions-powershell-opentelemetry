//
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

using Microsoft.Extensions.Logging;
using OpenTelemetry.Logs;
using OpenTelemetryEngine.Types;


namespace OpenTelemetryEngine.Logging
{
    public class FunctionsLoggerBuilder 
    {
        internal static LoggerWrapper? _logger;

        public static void SetLogger()
        {
            if (_logger is null)
            {
                var loggerFactory = LoggerFactory.Create(builder =>
                {
                    builder.AddOpenTelemetry(options =>
                    {
                        options.AddOtlpExporter();
                    });
                });

                _logger = new LoggerWrapper(loggerFactory.CreateLogger("Azure Functions PowerShell"));
            }
        }
        
        public static LoggerWrapper GetLogger() 
        {
            SetLogger();

            if (_logger is null)
            {
                throw new InvalidOperationException("Logger was null after SetLogger() call");
            }

            return _logger;
        }

        public static void Log(object? logItem, string? level) 
        {
            var logger = GetLogger();
            if (Enum.TryParse(typeof(LogLevel), level, out var _logLevel)) {
                if (logItem is not null && _logLevel is not null) {
                    logger.logger.Log((LogLevel)_logLevel, logItem.ToString());
                }   
                else 
                {
                    // TODO: Discuss behavior in this case
                    throw new ArgumentException("Message and/or level was null when attempting to log");
                }
            }
        }
    }
}