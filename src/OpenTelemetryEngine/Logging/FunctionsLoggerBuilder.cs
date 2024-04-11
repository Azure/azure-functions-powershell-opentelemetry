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
        private static FunctionsLogger? _logger;

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

                _logger = new FunctionsLogger(loggerFactory.CreateLogger("Azure Functions PowerShell"));

                _logger.logger.Log(LogLevel.Information, "Logger initialized");
            }
        }

        public static FunctionsLogger GetLogger() 
        {
            SetLogger();

            return _logger;
        }
    }
}