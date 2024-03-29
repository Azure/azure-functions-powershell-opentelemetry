//
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

using System.Management.Automation;
using OpenTelemetryEngine.Logging;

namespace AzureFunctions.PowerShell.OpenTelemetry.SDK
{
    /// <summary>
    /// Cmdlet to get the log handler for the functions logger.
    /// This handler allows the worker to send logs back to the logger we set up in the FunctionsLoggerBuilder.
    /// </summary>
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