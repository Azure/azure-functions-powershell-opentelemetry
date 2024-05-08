//
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

using System.Management.Automation;
using OpenTelemetryEngine.Logging;

namespace AzureFunctions.PowerShell.OpenTelemetry.SDK
{
    /// <summary>
    /// A cmdlet that can be used to manually log information to the OTLP endpoint. 
    /// Now that the worker supports log forwarding with Get-FunctionsLogHandler, this cmdlet is likely not needed for most customers. 
    /// It may be handy for testing or very specific scenarios where the customer does not want logs flowing through the worker. 
    /// </summary>
    [Cmdlet(VerbsCommunications.Write, "FunctionsOpenTelemetryLog")]
    public class WriteFunctionsOpenTelemetryLog : PSCmdlet
    {
        [Parameter(Mandatory = true)]
        public object? LogItem { get; set; }

        [Parameter(Mandatory = true)]
        public object? Level { get; set; }

        protected override void ProcessRecord()
        {
            if (string.IsNullOrEmpty(Environment.GetEnvironmentVariable(SDKConstants.FunctionsOpenTelemetryEnvironmentVariableName))) 
            {
                WriteWarning("OpenTelemetry environment variable not set, logs emitted from this worker instance will not be correlated with the invocation");
            }
            
            FunctionsLoggerBuilder.GetLogger().Log(LogItem, Level?.ToString());
        }
    }
}
