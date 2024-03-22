//
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

using System.Diagnostics;

namespace OpenTelemetryEngine.Types
{
    public class ActivityWrapper
    {
        public Activity? activity;

        public ActivityWrapper(Activity? activity) {  this.activity = activity; }
    }
}
