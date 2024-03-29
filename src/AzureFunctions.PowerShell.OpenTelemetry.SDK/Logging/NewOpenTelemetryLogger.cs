//
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

using System.Management.Automation;
using OpenTelemetryEngine.Logging;
using OpenTelemetryEngine.Types;

namespace AzureFunctions.PowerShell.OpenTelemetry.SDK
{
    /// <summary>
    /// Cmdlet that sets up the OpenTelemetry Logger
    /// This Logger is configured with an OTLP exporter that will send logs to an OTLP endpoint defined by the user
    /// See https://github.com/open-telemetry/opentelemetry-dotnet/blob/main/src/OpenTelemetry.Exporter.OpenTelemetryProtocol/README.md
    /// </summary>
    [Cmdlet(VerbsCommon.New, "OpenTelemetryLogger")]
    [OutputType(typeof(FunctionsLogger))]
    public class NewOpenTelemetryLogger : PSCmdlet
    {
        protected override void ProcessRecord()
        {
            WriteObject(FunctionsLoggerBuilder.GetLogger());
        }
    }
}
