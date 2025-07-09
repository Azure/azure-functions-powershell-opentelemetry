//
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

using System.Management.Automation;
using OpenTelemetryEngine.Traces;
using OpenTelemetryEngine.ResponseObjects;

namespace AzureFunctions.PowerShell.OpenTelemetry.SDK
{
    /// <summary>
    /// This cmdlet is used to start an OpenTelemetry invocation.
    /// Currently, that just means starting a (hidden) span which is a copy of the host span. 
    /// This allows all new spans created by the user or by dependent modules to link back to the host span using TraceParent. 
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "CurrentActivityForInvocation")]
    [OutputType(typeof(GetActivityResponse))]
    public class GetCurrentActivityForInvocation : PSCmdlet
    {
        /// <summary>
        /// InvocationId for the current invocation
        /// </summary>
        [Parameter(Mandatory = true)]
        public string InvocationId { get; set; } = string.Empty;

        // This method will be called for each input received from the pipeline to this cmdlet; if no input is received, this method is not called
        protected override void ProcessRecord()
        {
            WriteObject(FunctionsActivityBuilder.GetActivityForInvocation(InvocationId));
        }
    }
}
