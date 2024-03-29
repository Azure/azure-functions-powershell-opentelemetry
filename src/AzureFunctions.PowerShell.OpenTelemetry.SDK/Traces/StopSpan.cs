//
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

using System.Management.Automation;
using OpenTelemetryEngine.Traces;
using OpenTelemetryEngine.Types;

namespace AzureFunctions.PowerShell.OpenTelemetry.SDK
{
    /// <summary>
    /// Cmdlet to stop the span for the given activity.
    /// Required for each call to Start-Span
    /// </summary>
    [Cmdlet(VerbsLifecycle.Stop, "Span")]
    public class StopSpan : PSCmdlet
    {
        /// <summary>
        ///  The activity to stop.
        /// </summary>
        [Parameter(Mandatory = true, Position = 0)]
        public FunctionsActivity? Activity { get; set; }
        // This method will be called for each input received from the pipeline to this cmdlet; if no input is received, this method is not called
        protected override void ProcessRecord()
        {
            FunctionsActivityBuilder.StopActivity(Activity);
        }
    }
}
