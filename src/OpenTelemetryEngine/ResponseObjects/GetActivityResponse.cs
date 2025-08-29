using System.Diagnostics;

namespace OpenTelemetryEngine.ResponseObjects
{
    public class GetActivityResponse
    {
        public Activity? activity;

        public GetActivityResponse(Activity? activity)
        {
            this.activity = activity;
        }
    }
}
