//
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

using Microsoft.Extensions.Logging;

namespace OpenTelemetryEngine.ResponseObjects
{
    public class FunctionsLoggerResponse
    {
        public ILogger? logger;

        public FunctionsLoggerResponse(ILogger? logger) {  this.logger = logger; }
    }
}
