# Project

 This project contains the source code for the AzureFunctions.PowerShell.OpenTelemetry.SDK module which will be hosted in the PowerShell Gallery. The purpose of this module is to provide cmdlets designed for use in Azure Functions which will allow customers to implement their PowerShell Functions with OpenTelemetry via the OTLP Exporter. The cmdlets within this module will assume they are running in the context of the [Azure Functions PowerShell Worker](https://github.com/Azure/azure-functions-powershell-worker) in an Azure Functions environment. 

## Contributing

This project welcomes contributions and suggestions.  Most contributions require you to agree to a
Contributor License Agreement (CLA) declaring that you have the right to, and actually do, grant us
the rights to use your contribution. For details, visit https://cla.opensource.microsoft.com.

When you submit a pull request, a CLA bot will automatically determine whether you need to provide
a CLA and decorate the PR appropriately (e.g., status check, comment). Simply follow the instructions
provided by the bot. You will only need to do this once across all repos using our CLA.

This project has adopted the [Microsoft Open Source Code of Conduct](https://opensource.microsoft.com/codeofconduct/).
For more information see the [Code of Conduct FAQ](https://opensource.microsoft.com/codeofconduct/faq/) or
contact [opencode@microsoft.com](mailto:opencode@microsoft.com) with any additional questions or comments.

## Building from source

### Prereqs

* [.NET 6.0 SDK](https://dotnet.microsoft.com/en-us/download/visual-studio-sdks)

### To get started 

Navigate to the root directory of the repo in a PowerShell terminal and run .\build.ps1. The module will be built in the .\out\ directory. 

Visual Studio Code tasks have been built for F5 build and debug. The module will be built and run in a PowerShell window, but outside the context of the Azure Functions PowerShell worker, so cmdlets may not function as expected. 

## Trademarks

This project may contain trademarks or logos for projects, products, or services. Authorized use of Microsoft 
trademarks or logos is subject to and must follow 
[Microsoft's Trademark & Brand Guidelines](https://www.microsoft.com/en-us/legal/intellectualproperty/trademarks/usage/general).
Use of Microsoft trademarks or logos in modified versions of this project must not cause confusion or imply Microsoft sponsorship.
Any use of third-party trademarks or logos are subject to those third-party's policies.
