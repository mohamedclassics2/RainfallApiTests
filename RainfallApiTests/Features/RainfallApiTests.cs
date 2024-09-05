using System;
using TechTalk.SpecFlow;
using RestSharp;

namespace RainfallApiTests.Features
{
    [Binding]
    public class RainfallApiTests : BaseSpecFlowTest
    {
        private readonly string ApiKey = Environment.GetEnvironmentVariable("RAINFALL_API_KEY");
        private readonly RestClient Client = new RestClient("http://environment.data.gov.uk/flood-monitoring");

        protected RestResponse ExecuteGetRequest(string endpoint, int? limit = null, DateTime? date = null)
        {
            var request = new RestRequest(endpoint);
            request.AddHeader("Authorization", $"Bearer {ApiKey}");

            if (limit.HasValue)
                request.AddParameter("limit", limit.Value);

            if (date.HasValue)
                request.AddParameter("date", date.Value.ToString("yyyy-MM-dd"));

            return Client.Execute(request);
        }
    }

    public abstract class BaseSpecFlowTest
    {
        [Binding]
        public class BaseSpecFlowBinding : BaseSpecFlowBinding
        {
            private BaseSpecFlowBinding()
            {
            }

            private static BaseSpecFlowBinding Instance { get; } = new();

            public static BaseSpecFlowBinding CreateInstance()
            {
                return Instance;
            }

            protected void OnBeforeScenarioExecuted(ScenarioExecutionContext executionContext, ScenarioStepExecutionContext stepContext)
            {
                base.OnBeforeScenarioExecuted(executionContext, stepContext);
                // Add any setup logic here
            }

            protected void OnAfterScenarioExecuted(ScenarioExecutionContext executionContext, ScenarioStepExecutionContext stepContext)
            {
                base.OnAfterScenarioExecuted(executionContext, stepContext);
                // Add any teardown logic here
            }
        }
    }

    public class ScenarioStepExecutionContext
    {
    }

    public class ScenarioExecutionContext
    {
    }
}