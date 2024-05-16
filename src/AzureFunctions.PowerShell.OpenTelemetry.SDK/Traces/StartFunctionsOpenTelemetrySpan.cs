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
    /// Manually starts a span (Activity) at the request of the user. 
    /// It is required to capture the returned Activity and pass it to Stop-Span to end the span.
    /// </summary>
    [Cmdlet(VerbsLifecycle.Start, "FunctionsOpenTelemetrySpan")]
    [OutputType(typeof(FunctionsActivityResponse))]
    public class StartFunctionsOpenTelemetrySpan : PSCmdlet
    {
        /// <summary>
        /// The name of the span (optional)
        /// </summary>
        [Parameter(Mandatory = false)]
        public string? ActivityName { get; set; }
        // This method will be called for each input received from the pipeline to this cmdlet; if no input is received, this method is not called
        protected override void ProcessRecord()
        {
            if (!FunctionsEnvironmentHelper.IsFunctionsEnvironmentVariableEnabled() && !FunctionsEnvironmentHelper.HasWarnedAboutEnvironmentVariable()) 
            {
                WriteWarning(FunctionsEnvironmentHelper.GetEnvironmentVariableMissingWarningMessage());
                FunctionsEnvironmentHelper.DidWarnAboutEnvironmentVariable();
            }

            var response = FunctionsActivityBuilder.StartActivity(ActivityName);

            WriteObject(response);
        }
    }
}
