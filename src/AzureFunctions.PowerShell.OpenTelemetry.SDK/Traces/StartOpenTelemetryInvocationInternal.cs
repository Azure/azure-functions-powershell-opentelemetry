//
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

using System.Management.Automation;
using OpenTelemetryEngine.Traces;
using OpenTelemetryEngine.ResponseObjects;
using System.Management.Automation.Runspaces;

namespace AzureFunctions.PowerShell.OpenTelemetry.SDK
{
    /// <summary>
    /// This cmdlet is used to start an OpenTelemetry invocation.
    /// Currently, that just means starting a (hidden) span which is a copy of the host span. 
    /// This allows all new spans created by the user or by dependent modules to link back to the host span using TraceParent. 
    /// </summary>
    [Cmdlet(VerbsLifecycle.Start, "OpenTelemetryInvocationInternal")]
    [OutputType(typeof(FunctionsActivityResponse))]
    public class StartOpenTelemetryInvocationInternal : PSCmdlet
    {
        /// <summary>
        /// InvocationId for the current invocation
        /// </summary>
        [Parameter(Mandatory = true)]
        public string InvocationId { get; set; } = string.Empty;

        /// <summary>
        /// TraceParent from the host span. Sent with TraceContext in InvocationRequest
        /// </summary>
        [AllowEmptyString]
        [Parameter(Mandatory = true)]
        public string TraceParent { get; set; } = string.Empty;
        
        /// <summary>
        /// TraceState from the host span. Sent with TraceContext in InvocationRequest
        /// </summary>
        [AllowEmptyString]
        [Parameter(Mandatory = true)]
        public string TraceState { get; set; } = string.Empty;

        // This method will be called for each input received from the pipeline to this cmdlet; if no input is received, this method is not called
        protected override void ProcessRecord()
        {
            var newActivity = FunctionsActivityBuilder.StartInternalActivity(InvocationId, TraceParent, TraceState);

            try
            {
                string script = "param($id) Set-FunctionInvocationContext -InvocationId $id";
                _ = this.InvokeCommand.InvokeScript(script, false, PipelineResultTypes.Output, null, new object[] { InvocationId });
            }
            catch { }

            WriteObject(newActivity);
        }
    }
}
