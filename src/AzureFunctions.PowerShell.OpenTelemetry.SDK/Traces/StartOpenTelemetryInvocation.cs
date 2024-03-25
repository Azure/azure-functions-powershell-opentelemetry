//
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

using System.Management.Automation;
using OpenTelemetryEngine.Traces;
using OpenTelemetryEngine.Types;

namespace AzureFunctions.PowerShell.OpenTelemetry.SDK
{
    [Cmdlet(VerbsLifecycle.Start, "OpenTelemetryInvocation")]
    [OutputType(typeof(ActivityWrapper))]
    public class StartOpenTelemetryInvocation : PSCmdlet
    {
        [Parameter(Mandatory = true, Position = 0)]
        public string InvocationId { get; set; } = string.Empty;

        //TODO: Test if AllowEmptyString is correct here. I suspect that it is, but the cmdlet, at that point, serves no purpose
        [AllowEmptyString]
        [Parameter(Mandatory = true, Position = 1)]
        public string TraceParent { get; set; } = string.Empty;
        
        [AllowEmptyString]
        [Parameter(Mandatory = true, Position = 2)]
        public string TraceState { get; set; } = string.Empty;

        // This method will be called for each input received from the pipeline to this cmdlet; if no input is received, this method is not called
        protected override void ProcessRecord()
        {
            var newActivity = FunctionsActivityBuilder.StartInternalActivity(InvocationId, TraceParent, TraceState);
            WriteObject(newActivity);
        }
    }
}
