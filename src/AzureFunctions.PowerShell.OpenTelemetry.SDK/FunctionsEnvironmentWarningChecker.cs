//
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

using System.Security.Policy;

namespace AzureFunctions.PowerShell.OpenTelemetry.SDK
{
    public class FunctionsEnvironmentWarningChecker
    {
        private static bool _environmentVariableWarned = false;

        private static string _warningMessage = String.Format("{0}: Environment variable {1} not set, this module may not have the intended behavior. " + 
                                                             "Logs emitted from your function app's default pipeline will not be sent to OpenTelemetry, " +
                                                             "and traces and spans will not be correlated with telemetry emitted from the functions host.",
            SDKConstants.FunctionsOpenTelemetryModuleName, SDKConstants.FunctionsOpenTelemetryEnvironmentVariableName);

        private static bool IsFunctionsEnvironmentVariableEnabled()
        {
            string? value = System.Environment.GetEnvironmentVariable(SDKConstants.FunctionsOpenTelemetryEnvironmentVariableName);

            if (!string.IsNullOrEmpty(value) && bool.TryParse(value, out bool isEnabled) && isEnabled) 
            {
                return true;
            }

            return false;
        }

        internal static bool ShouldWarnEnvironmentVariableMissing(out string? warningMessage) 
        {
            warningMessage = null;

            if (!_environmentVariableWarned && !IsFunctionsEnvironmentVariableEnabled())
            {
                warningMessage = _warningMessage;

                _environmentVariableWarned = true;
                return true;
            }

            // Even if we didn't warn, we still set this to true to prevent expensive env checks for future calls
            _environmentVariableWarned = true;
            return false;
        }
    }
}
