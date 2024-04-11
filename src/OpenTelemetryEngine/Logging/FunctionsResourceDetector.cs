//
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//
using System.Diagnostics;
using OpenTelemetry.Resources;
using OpenTelemetryEngine.Constants;

namespace OpenTelemetryEngine.Logging
{
internal sealed class FunctionsResourceDetector : IResourceDetector
    {
        public Resource Detect()
        {
            List<KeyValuePair<string, object>> attributeList = new(9);
            try
            {
                string? serviceName = Environment.GetEnvironmentVariable(ResourceAttributeConstants.SiteNameEnvVar);
                // Todo: This is probably wrong, but we don't have access to types from the worker in this context
                // We may have to pass the worker's assembly version manually in setup, will figure this out later
                string? version = typeof(FunctionsResourceDetector).Assembly.GetName()?.Version?.ToString();

                if (!string.IsNullOrEmpty(version)) 
                {
                    attributeList.Add(new KeyValuePair<string, object>(ResourceAttributeConstants.ServiceVersion, version));                    
                }

                attributeList.Add(new KeyValuePair<string, object>(ResourceAttributeConstants.AISDKPrefix, $@"{ResourceAttributeConstants.SDKPrefix}:{version}"));
                attributeList.Add(new KeyValuePair<string, object>(ResourceAttributeConstants.ProcessId, Process.GetCurrentProcess().Id));

                // Add these attributes only if running in Azure.
                if (!string.IsNullOrEmpty(serviceName))
                {
                    attributeList.Add(new KeyValuePair<string, object>(ResourceAttributeConstants.ServiceName, serviceName));
                    attributeList.Add(new KeyValuePair<string, object>(ResourceAttributeConstants.CloudProvider, ResourceAttributeConstants.AzureCloudProviderValue));
                    attributeList.Add(new KeyValuePair<string, object>(ResourceAttributeConstants.CloudPlatform, ResourceAttributeConstants.AzurePlatformValue));

                    if (!string.IsNullOrEmpty(version))
                    {
                        attributeList.Add(new KeyValuePair<string, object>(ResourceAttributeConstants.FaaSVersion, version));
                    }

                    string? region = Environment.GetEnvironmentVariable(ResourceAttributeConstants.RegionNameEnvVar);
                    if (!string.IsNullOrEmpty(region))
                    {
                        attributeList.Add(new KeyValuePair<string, object>(ResourceAttributeConstants.CloudRegion, region));
                    }

                    var azureResourceUri = GetAzureResourceURI(serviceName);
                    if (azureResourceUri != null)
                    {
                        attributeList.Add(new KeyValuePair<string, object>(ResourceAttributeConstants.CloudResourceId, azureResourceUri));
                    }
                }
            }
            catch
            {
                // return empty resource.
                return Resource.Empty;
            }

            return new Resource(attributeList);
        }

        private static string? GetAzureResourceURI(string websiteSiteName)
        {
            string? websiteResourceGroup = Environment.GetEnvironmentVariable(ResourceAttributeConstants.ResourceGroupEnvVar);
            string websiteOwnerName = Environment.GetEnvironmentVariable(ResourceAttributeConstants.OwnerNameEnvVar) ?? string.Empty;
            int idx = websiteOwnerName.IndexOf('+', StringComparison.Ordinal);
            string subscriptionId = idx > 0 ? websiteOwnerName.Substring(0, idx) : websiteOwnerName;

            if (string.IsNullOrEmpty(websiteResourceGroup) || string.IsNullOrEmpty(subscriptionId))
            {
                return null;
            }

            return $"/subscriptions/{subscriptionId}/resourceGroups/{websiteResourceGroup}/providers/Microsoft.Web/sites/{websiteSiteName}";
        }
    }
}