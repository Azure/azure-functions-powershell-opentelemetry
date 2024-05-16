//
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

using System.Management.Automation;
using OpenTelemetryEngine.Logging;
using OpenTelemetryEngine.ResponseObjects;

namespace AzureFunctions.PowerShell.OpenTelemetry.SDK
{
    /// <summary>
    /// Cmdlet that sets up the OpenTelemetry Logger
    /// This Logger is configured with an OTLP exporter that will send logs to an OTLP endpoint defined by the user
    /// See https://github.com/open-telemetry/opentelemetry-dotnet/blob/main/src/OpenTelemetry.Exporter.OpenTelemetryProtocol/README.md
    /// </summary>
    [Cmdlet(VerbsCommon.New, "FunctionsOpenTelemetryLogger")]
    [OutputType(typeof(FunctionsLoggerResponse))]
    public class NewFunctionsOpenTelemetryLogger : PSCmdlet
    {
        protected override void ProcessRecord()
        {
            if (!FunctionsEnvironmentHelper.IsFunctionsEnvironmentVariableEnabled() && !FunctionsEnvironmentHelper.HasWarnedAboutEnvironmentVariable()) 
            {
                WriteWarning(FunctionsEnvironmentHelper.GetEnvironmentVariableMissingWarningMessage());
                FunctionsEnvironmentHelper.DidWarnAboutEnvironmentVariable();
            }

            var response = FunctionsLoggerBuilder.GetLogger().BuildResponse();

            WriteObject(response);
        }
    }
}
