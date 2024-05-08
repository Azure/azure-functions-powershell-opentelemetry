#	
# Copyright (c) Microsoft. All rights reserved.	
# Licensed under the MIT license. See LICENSE file in the project root for full license information.	
#
function Initialize-FunctionsOpenTelemetry {
    [CmdletBinding()]
    param()
    $ErrorActionPreference = 'Stop'
    
    New-FunctionsOpenTelemetryLogger | Out-Null
    New-FunctionsOpenTelemetryTracerBuilder | Out-Null
}