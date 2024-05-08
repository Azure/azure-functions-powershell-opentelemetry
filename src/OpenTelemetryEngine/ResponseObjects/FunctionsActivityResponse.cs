//
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

using System.Diagnostics;

namespace OpenTelemetryEngine.ResponseObjects
{
    public class FunctionsActivityResponse
    {
        public Activity? activity;

        public FunctionsActivityResponse(Activity? activity) {  this.activity = activity; }
    }
}
