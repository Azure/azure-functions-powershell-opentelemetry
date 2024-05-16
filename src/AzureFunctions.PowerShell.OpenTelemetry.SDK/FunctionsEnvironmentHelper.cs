//
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

namespace AzureFunctions.PowerShell.OpenTelemetry.SDK
{
    public class FunctionsEnvironmentHelper
    {
        private static bool _environmentVariableWarned = false;
        internal static bool IsFunctionsEnvironmentVariableEnabled()
        {
            string? value = System.Environment.GetEnvironmentVariable(SDKConstants.FunctionsOpenTelemetryEnvironmentVariableName);

            if (!string.IsNullOrEmpty(value) && bool.TryParse(value, out bool isEnabled) && isEnabled) 
            {
                return true;
            }
            
            return false;
        }

        internal static bool HasWarnedAboutEnvironmentVariable() 
        {
            return _environmentVariableWarned;
        }

        internal static void DidWarnAboutEnvironmentVariable() 
        {
            _environmentVariableWarned = true;
        }

        internal static string GetEnvironmentVariableMissingWarningMessage() 
        {
            return String.Format("{0}: Environment variable {1} not set, this module may not have the intended behavior. " + 
                "Logs emitted from your function app's default pipeline will not be sent to OpenTelemetry, and traces and spans will not be correlated with telemetry emitted from the functions host.",
                SDKConstants.FunctionsOpenTelemetryModuleName, SDKConstants.FunctionsOpenTelemetryEnvironmentVariableName);
        }
    }
}
