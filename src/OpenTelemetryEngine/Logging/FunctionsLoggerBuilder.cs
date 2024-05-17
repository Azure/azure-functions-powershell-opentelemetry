//
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

using Microsoft.Extensions.Logging;
using OpenTelemetry.Logs;
using OpenTelemetry.Resources;
using OpenTelemetryEngine.Types;
using OpenTelemetryEngine.Resources;


namespace OpenTelemetryEngine.Logging
{
    public class FunctionsLoggerBuilder 
    {
        private static FunctionsLogger? _logger;

        public static void InitializeLogger()
        {
            if (_logger is null)
            {
                var loggerFactory = LoggerFactory.Create(builder =>
                {
                    builder.AddOpenTelemetry(options =>
                    {
                        options.SetResourceBuilder(ResourceBuilder.CreateDefault().AddDetector(new FunctionsResourceDetector()))
                            .AddOtlpExporter();
                    });
                });

                _logger = new FunctionsLogger(loggerFactory.CreateLogger("Azure Functions PowerShell"));
            }
        }

        public static FunctionsLogger GetLogger() 
        {
            InitializeLogger();

            return _logger;
        }
    }
}