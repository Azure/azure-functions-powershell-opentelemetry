//
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

using Microsoft.Extensions.Logging;

namespace OpenTelemetryEngine.Types
{
    public class LoggerWrapper
    {
        public ILogger logger;

        public LoggerWrapper(ILogger logger) {  this.logger = logger; }
    }
}
