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
        internal static FunctionsLogger? _logger;

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
            }
        }
        
        public static FunctionsLogger GetLogger() 
        {
            SetLogger();

            if (_logger is null)
            {
                throw new InvalidOperationException("Logger was null after SetLogger() call");
            }

            return _logger;
        }
    }
}