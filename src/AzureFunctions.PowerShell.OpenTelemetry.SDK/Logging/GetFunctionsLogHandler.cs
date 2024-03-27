//
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

using System.Management.Automation;
using OpenTelemetryEngine.Logging;

namespace AzureFunctions.PowerShell.OpenTelemetry.SDK
{
    [Cmdlet(VerbsCommon.Get, "FunctionsLogHandler")]
    [OutputType(typeof(Action<string, string, Exception>))]
    public class GetFunctionsLogHandlerCmdlet : Cmdlet
    {
        protected override void ProcessRecord()
        {
            WriteObject(FunctionsLoggerBuilder.GetLogger().handleEventLogs);
        }
    }
}