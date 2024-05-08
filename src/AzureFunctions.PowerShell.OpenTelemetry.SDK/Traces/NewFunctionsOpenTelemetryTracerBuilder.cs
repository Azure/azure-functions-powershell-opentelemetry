//
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

using System.Management.Automation;
using OpenTelemetryEngine.ResponseObjects;
using OpenTelemetryEngine.Traces;

namespace AzureFunctions.PowerShell.OpenTelemetry.SDK
{
    /// <summary>
    /// Cmdlet that sets up the OpenTelemetry TracerBuilder
    /// This Logger is configured with an OTLP exporter that will send traces to an OTLP endpoint defined by the user
    /// See https://github.com/open-telemetry/opentelemetry-dotnet/blob/main/src/OpenTelemetry.Exporter.OpenTelemetryProtocol/README.md
    /// </summary>
    [Cmdlet(VerbsCommon.New, "FunctionsOpenTelemetryTracerBuilder")]
    [OutputType(typeof(FunctionsTracerBuilderResponse))]
    public class NewFunctionsOpenTelemetryTracerBuilder : PSCmdlet
    {

        /// <summary>
        /// Allows the user to specify additional trace sources to listen for and stream to the OTLP endpoint
        /// If the user is using packages or modules that utilize .NET Activities, subscribing to those ActivitySources will link them to the parent trace
        /// </summary>
        [Parameter(Mandatory = false, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
        public List<string> AdditionalSources { get; set; } = new List<string>();
        // This method will be called for each input received from the pipeline to this cmdlet; if no input is received, this method is not called
        protected override void ProcessRecord()
        {
            if (string.IsNullOrEmpty(Environment.GetEnvironmentVariable(SDKConstants.FunctionsOpenTelemetryEnvironmentVariableName))) 
            {
                WriteWarning("OpenTelemetry environment variable not set, user generated traces will not be linked to parent trace from functions host");
            }

            var response = FunctionsTracerBuilder.BuildTracer(AdditionalSources);

            WriteObject(response);
        }
    }
}
