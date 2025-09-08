# Contributor Onboarding
This contributor guide explains how to make and test changes to the Azure Functions PowerShell OpenTelemetry SDK.
Thank you for taking the time to contribute to the Azure Functions PowerShell OpenTelemetry SDK!

## Table of Contents

- [Relevant Docs](#relevant-docs)
- [Prerequisites](#prerequisites)
- [Pull Request Change Flow](#pull-request-change-flow)
- [Testing with a Durable Functions app](#testing-with-a-durable-functions-app)

## Relevant Docs
- [Durable Functions Overview](https://docs.microsoft.com/en-us/azure/azure-functions/durable/durable-functions-overview)
- [Durable Functions Application Patterns](https://learn.microsoft.com/en-us/azure/azure-functions/durable/durable-functions-overview?tabs=in-process%2Cnodejs-v3%2Cv1-model&pivots=java#application-patterns)
- [Azure Functions PowerShell Quickstart](https://learn.microsoft.com/en-us/azure/azure-functions/durable/quickstart-powershell-vscode)

## Prerequisites
- Visual Studio Code
- [Azure Functions Core Tools](https://learn.microsoft.com/en-us/azure/azure-functions/functions-run-local?tabs=windows%2Cisolated-process%2Cnode-v4%2Cpython-v2%2Chttp-trigger%2Ccontainer-apps&pivots=programming-language-java)

## Pull Request Change Flow

The general flow for making a change to the library is:

1. 🍴 Fork the repo (add the fork via `git remote add me <clone url here>`)
2. 🌳 Create a branch for your change (generally branch from dev) (`git checkout -b my-change`)
3. 🛠 Make your change
4. ✔️ Test your change
5. ⬆️ Push your changes to your fork (`git push me my-change`)
6. 💌 Open a PR to the main branch
7. 📢 Address feedback and make sure tests pass (yes even if it's an "unrelated" test failure)
8. 📦 [Rebase](https://git-scm.com/docs/git-rebase) your changes into meaningful commits (`git rebase -i HEAD~N` where `N` is commits you want to squash)
9. :shipit: Rebase and merge (This will be done for you if you don't have contributor access)
10. ✂️ Delete your branch (optional)

## Testing with a Durable Functions app

The following instructions explain how to test changes in a Durable Functions PowerShell app.

1. Run `./build.ps1`.
2. Copy the `/test/e2e/app/modules/AzureFunctions.PowerShell.OpenTelemetry.SDK` folder into your test app's `app/Modules` folder.
3. Remove AzureFunctions.PowerShell.OpenTelemetry.SDK from requirements.psd1 in the test app.
4. Add the following line to profile.ps1: Write-Information $env:PSModulePath and execute any function in the app. Go to every location in the outputted PSModulePath and remove AzureFunctions.PowerShell.OpenTelemetry.SDK folders anywhere they might exist except the one copied in step #2.
