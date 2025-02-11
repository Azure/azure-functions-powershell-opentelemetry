//
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

using OpenTelemetryEngine.Traces;

namespace OpenTelemetryEngineTests.Traces
{
    public class FunctionsActivityBuilderTests
    {

        public FunctionsActivityBuilderTests()
        {
            var _ = FunctionsTracerBuilder.BuildTracer(new List<string>());
        }

        [Fact]
        public void StartInternalActivity_CreatesValidActivity()
        {
            string invocationId = Guid.NewGuid().ToString();

            var startActivityResponse = FunctionsActivityBuilder.StartInternalActivity(invocationId, "", "");

            Assert.NotNull(startActivityResponse);
            Assert.NotNull(startActivityResponse.activity);
            Assert.Equal(invocationId, startActivityResponse.activity.Tags.Where(x => x.Key == "invocationId").First().Value);
        }

        [Fact]
        public void StartInternalActivity_DoesNotAllowDuplicates()
        {
            string invocationId = Guid.NewGuid().ToString();

            var startActivityResponse = FunctionsActivityBuilder.StartInternalActivity(invocationId, "", "");
            var startActivityResponse2 = FunctionsActivityBuilder.StartInternalActivity(invocationId, "", "");

            Assert.NotNull(startActivityResponse2);
            Assert.Null(startActivityResponse2.activity);

        }

        [Fact]
        public void StopInternalActivity_StopsActivity()
        {
            string invocationId = Guid.NewGuid().ToString();

            var startActivityResponse = FunctionsActivityBuilder.StartInternalActivity(invocationId, "", "");

            Assert.NotNull(startActivityResponse);
            Assert.NotNull(startActivityResponse.activity);
            Assert.False(startActivityResponse.activity.IsStopped);

            FunctionsActivityBuilder.StopInternalActivity(invocationId);
            Assert.True(startActivityResponse.activity.IsStopped);
        }

        [Fact]
        public void StartActivity_CreatesValidActivity()
        {
            string activityName = Guid.NewGuid().ToString();

            var startActivityResponse = FunctionsActivityBuilder.StartActivity(activityName);

            Assert.NotNull(startActivityResponse);
            Assert.NotNull(startActivityResponse.activity);
            Assert.Equal(activityName, startActivityResponse.activity.DisplayName);
        }

        [Fact]
        public void StartActivity_DoesNotAllowDuplicates()
        {
            string activityName = Guid.NewGuid().ToString();

            var startActivityResponse = FunctionsActivityBuilder.StartInternalActivity(activityName, "", "");
            var startActivityResponse2 = FunctionsActivityBuilder.StartInternalActivity(activityName, "", "");

            Assert.NotNull(startActivityResponse2);
            Assert.Null(startActivityResponse2.activity);
        }

        [Fact]
        public void StopActivity_StopsActivity()
        {
            string activityName = Guid.NewGuid().ToString();

            var startActivityResponse = FunctionsActivityBuilder.StartActivity(activityName);

            Assert.NotNull(startActivityResponse);
            Assert.NotNull(startActivityResponse.activity);
            Assert.False(startActivityResponse.activity.IsStopped);

            FunctionsActivityBuilder.StopActivity(startActivityResponse);
            Assert.True(startActivityResponse.activity.IsStopped);
        }
    }
}
