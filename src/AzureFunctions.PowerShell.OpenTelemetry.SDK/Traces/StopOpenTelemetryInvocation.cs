//
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

using System.Management.Automation;
using OpenTelemetryEngine.Traces;

namespace AzureFunctions.PowerShell.OpenTelemetry.SDK
{
    [Cmdlet(VerbsLifecycle.Stop, "OpenTelemetryInvocation")]
    public class StopOpenTelemetryInvocation : PSCmdlet
    {
        [Parameter(Mandatory = true, Position = 0)]
        public string InvocationId { get; set; } = string.Empty;

        // This method will be called for each input received from the pipeline to this cmdlet; if no input is received, this method is not called
        protected override void ProcessRecord()
        {
            FunctionsActivityBuilder.StopInternalActivity(InvocationId);
        }
    }
}
