//
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

using System.Management.Automation;
using OpenTelemetryEngine.Logging;
using OpenTelemetryEngine.Types;

namespace AzureFunctions.PowerShell.OpenTelemetry.SDK
{
    [Cmdlet(VerbsCommon.New, "OpenTelemetryLogger")]
    [OutputType(typeof(FunctionsLogger))]
    public class NewOpenTelemetryLogger : PSCmdlet
    {
        // This method will be called for each input received from the pipeline to this cmdlet; if no input is received, this method is not called
        protected override void ProcessRecord()
        {
            WriteObject(FunctionsLoggerBuilder.GetLogger());
        }
    }
}
