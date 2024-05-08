//
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

namespace OpenTelemetryEngine.Constants
{
    internal class ResourceAttributeConstants
    {
        internal const string AISDKPrefix = "ai.sdk.prefix";
        internal const string ProcessId = "process.pid";

        internal const string ServiceName = "service.name";
        internal const string ServiceVersion = "service.version";

        internal const string CloudProvider = "cloud.provider";
        internal const string CloudPlatform = "cloud.platform";
        internal const string CloudRegion = "cloud.region";
        internal const string CloudResourceId = "cloud.resource.id";
   
        internal const string FaaSVersion = "faas.version";
        
        internal const string AzureCloudProviderValue = "azure";
        internal const string AzurePlatformValue = "azure_functions";
        internal const string SDKPrefix = "azurefunctions";
        internal const string SiteNameEnvVar = "WEBSITE_SITE_NAME";
        internal const string RegionNameEnvVar = "REGION_NAME";
        internal const string ResourceGroupEnvVar = "WEBSITE_RESOURCE_GROUP";
        internal const string OwnerNameEnvVar = "WEBSITE_OWNER_NAME";
    }
}