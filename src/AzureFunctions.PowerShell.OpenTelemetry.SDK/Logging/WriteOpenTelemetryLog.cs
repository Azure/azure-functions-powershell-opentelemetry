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
    [Cmdlet(VerbsCommunications.Write, "OpenTelemetryLog")]
    public class WriteOpenTelemetryLog : PSCmdlet
    {
        [Parameter(Mandatory = true, Position = 0)]
        public object? LogItem { get; set; }

        [Parameter(Mandatory = true, Position = 1)]
        public object? Level { get; set; }

        protected override void ProcessRecord()
        {
            FunctionsLoggerBuilder.GetLogger().Log(LogItem, Level?.ToString());
        }
    }
}
