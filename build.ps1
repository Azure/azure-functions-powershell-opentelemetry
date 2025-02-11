#
# Copyright (c) Microsoft. All rights reserved.
# Licensed under the MIT license. See LICENSE file in the project root for full license information.
#

# This build script is designed to work with the ALC-based PowerShell guidance for dependency isolation.
# https://docs.microsoft.com/en-us/powershell/scripting/dev-cross-plat/resolving-dependency-conflicts?view=powershell-7.2#loading-through-net-core-assembly-load-contexts
param(
    [ValidateSet('Debug', 'Release')]
    [string]
    $Configuration = 'Debug',
    [switch]
    $NoBuild,
    [switch]
    $Test
)

Import-Module "$PSScriptRoot\pipelineUtilities.psm1" -Force

$SrcDirectory = "$PSScriptRoot\src"

if (!$NoBuild.IsPresent) {

    $packageName = "AzureFunctions.PowerShell.OpenTelemetry.SDK"
    $shimPath = "$PSScriptRoot/src/AzureFunctions.PowerShell.OpenTelemetry.SDK"
    $otelEnginePath = "$PSScriptRoot/src/OpenTelemetryEngine"
    $otelAppPath = "$PSScriptRoot/test/E2E/app/Modules/$packageName"
    $powerShellModulePath = "$PSScriptRoot/src/$packageName.psm1"
    $manifestPath = "$PSScriptRoot/src/$packageName.psd1"

    # Publish to /out/ folder
    # When test app added, publish there instead
    $outputPath = $otelAppPath

    $sharedDependenciesPath = "$outputPath/Dependencies/"

    $netCoreTFM = 'net6.0'
    $publishPathSuffix = "bin/$Configuration/$netCoreTFM/publish"

    #region BUILD ARTIFACTS ===========================================================================
    Write-Log "Build started..."
    Write-Log "Configuration: '$Configuration'`nOutput folder '$outputPath'`nShared dependencies folder: '$sharedDependenciesPath'" "Gray"

    # Map from project names to the folder containing the corresponding .csproj
    $projects = @{
        'OpenTelemetry SDK' = $shimPath
        'OpenTelemetry Engine' = $otelEnginePath
    }

    # Remove previous build if it exists
    Write-Log "Removing previous build from $outputPath if it exists..." "Cyan"
    if (Test-Path $outputPath)
    {
        Remove-Item -Path $outputPath -Recurse -Force -ErrorAction Ignore
    }
    # Create output folder and its inner dependencies directory
    Write-Log "Creating a new output and shared dependencies folder at $outputPath and $sharedDependenciesPath..." "Cyan"
    [void](New-Item -Path $sharedDependenciesPath -ItemType Directory)

    # Build the OTel SDK project
    foreach ($project in $projects.GetEnumerator()) {
        Write-Log "Building $($project.Name) project with target framework $netCoreTFM...."
        Push-Location $project.Value
        try
        {
            dotnet publish -f $netCoreTFM -c $Configuration
        }
        finally
        {
            Pop-Location
        }
    }

    $commonFiles = [System.Collections.Generic.HashSet[string]]::new()

    Write-Log "Copying assemblies from the OpenTelemetry Engine project into $sharedDependenciesPath" "Gray"
    Get-ChildItem -Path (Join-Path "$otelEnginePath" "$publishPathSuffix") |
        Where-Object { $_.Extension -in '.dll','.pdb' } |
        ForEach-Object { [void]$commonFiles.Add($_.Name); Copy-Item -LiteralPath $_.FullName -Destination $sharedDependenciesPath }

    # Copy all *unique* assemblies from OTel SDK into output directory
    Write-Log "Copying unique assemblies from the OTel SDK project into $outputPath" "Gray"
    Get-ChildItem -Path (Join-Path "$shimPath" "$publishPathSuffix") |
        Where-Object { $_.Extension -in '.dll','.pdb' -and -not $commonFiles.Contains($_.Name) } |
        ForEach-Object { Copy-Item -LiteralPath $_.FullName -Destination $outputPath }

    # Move OTel SDK manifest into the output directory
    Write-Log "Copying PowerShell module and manifest from the OTel SDK source code into $outputPath" "Gray"
    Copy-Item -Path $powerShellModulePath -Destination $outputPath
    Copy-Item -Path $manifestPath -Destination $outputPath
    Write-Log "Build succeeded!"
    #endregion
}
#region Test ==================================================================================
if ($Test.IsPresent) {
    Set-Location $SrcDirectory
    dotnet test
    if ($LASTEXITCODE -ne 0) { throw "xunit tests failed." }
}
#endregion