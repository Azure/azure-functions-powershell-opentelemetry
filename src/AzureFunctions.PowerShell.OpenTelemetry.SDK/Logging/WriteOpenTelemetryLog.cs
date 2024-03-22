//
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

using System.Management.Automation;
using OpenTelemetryEngine.Logging;

namespace AzureFunctions.PowerShell.OpenTelemetry.SDK
{
    [Cmdlet(VerbsCommunications.Write, "OpenTelemetryLog")]
    public class WriteOpenTelemetryLog : PSCmdlet
    {
        [Parameter(Mandatory = true, Position = 0)]
        public object? LogItem { get; set; }

        [Parameter(Mandatory = true, Position = 1)]
        public object? Level { get; set; }

        // This method will be called for each input received from the pipeline to this cmdlet; if no input is received, this method is not called
        protected override void ProcessRecord()
        {
            FunctionsLoggerBuilder.Log(LogItem, Level?.ToString());
        }
    }
}
