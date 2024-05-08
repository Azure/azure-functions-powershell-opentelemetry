//
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

using System.Management.Automation;
using OpenTelemetryEngine.Traces;

namespace AzureFunctions.PowerShell.OpenTelemetry.SDK
{
    /// <summary>
    /// Cmdlet used by the PowerShell functions worker at the end of an invocation, to stop the worker's internal activity.
    /// Ensures that the activity length is correct and resources are disposed properly. 
    /// </summary>
    [Cmdlet(VerbsLifecycle.Stop, "OpenTelemetryInvocationInternal")]
    public class StopOpenTelemetryInvocationInternal : PSCmdlet
    {
        /// <summary>
        /// ID of the invocation that just ended. 
        /// </summary>
        [Parameter(Mandatory = true)]
        public string InvocationId { get; set; } = string.Empty;

        // This method will be called for each input received from the pipeline to this cmdlet; if no input is received, this method is not called
        protected override void ProcessRecord()
        {
            FunctionsActivityBuilder.StopInternalActivity(InvocationId);
        }
    }
}
