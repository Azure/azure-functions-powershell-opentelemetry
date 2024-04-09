//
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

using System.Management.Automation;
using OpenTelemetryEngine.Logging;

namespace AzureFunctions.PowerShell.OpenTelemetry.SDK
{
    public class SDKConstants 
    {
        internal const string FunctionsOpenTelemetryEnvironmentVariableName = "OTEL_FUNCTIONS_WORKER_ENABLED";
        internal const string EnvironmentVariableMissingErrorCategory = "OpenTelemetryEnvironmentVariableNotSet";

    }
}