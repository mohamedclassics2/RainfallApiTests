using System;
using TechTalk.SpecFlow;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Newtonsoft.Json;
using RestSharp;


namespace RainfallApiTests.Features
{
    [Binding]
    public class RainfallApiSteps : BaseSpecFlowTest
    {
        private Response _lastResponse;

        [Given(@"I have a valid API key")]
        public void GivenIHaveAValidApiKey()
        {
            // This step doesn't need implementation as it's just a prerequisite
        }

        [When(@"I call the API to get rainfall measurements for a station with a limit of (.*)")]
        public void WhenICallTheAPItoGetRainfallMeasurementsForAStationWithALimitOf(int limit)
        {
            var response = ExecuteGetRequest("/stations/{stationId}/measurements", limit);
            _lastResponse = response;
        }

        private Response ExecuteGetRequest(string stationsStationidMeasurements, int limit)
        {
            throw new System.NotImplementedException();
        }

        [Then(@"I should receive exactly (.*) rainfall measurements")]
        public void ThenIShouldReceiveExactlyRainfallMeasurements(int count)
        {
            Assert.AreEqual(count, _lastResponse.Content.Count);
        }

        [Then(@"each measurement should be within the last (.*) hours")]
        public void ThenEachMeasurementShouldBeWithinTheLastHours(int hours)
        {
            var measurements = JsonConvert.DeserializeObject<List<Measurement>>(_lastResponse.Content);
            var latestTime = DateTime.Now - TimeSpan.FromHours(hours);
            Assert.All(measurements, m => Assert.IsTrue(latestTime <= m.Timestamp));
        }

        [When(@"I call the API to get rainfall measurements for a station on (.*)")]
        public void WhenICallTheAPItoGetRainfallMeasurementsForAStationOn(string date)
        {
            var response = ExecuteGetRequest($"/stations/{stationId}/measurements", null, DateTime.Parse(date));
            _lastResponse = response;
        }

        [Then(@"I should receive rainfall measurements for that date")]
        public void ThenIShouldReceiveRainfallMeasurementsForThatDate()
        {
            var measurements = JsonConvert.DeserializeObject<List<Measurement>>(_lastResponse.Content);
            Assert.IsTrue(measurements.Any(m => m.Timestamp.Date == DateTime.Parse((string)_lastResponse.Request.Parameters["date"].ToString())));
        }

        [Then(@"the measurements should be sorted chronologically")]
        public void ThenTheMeasurementsShouldBeSortedChronologically()
        {
            var measurements = JsonConvert.DeserializeObject<List<Measurement>>(_lastResponse.Content);
            Assert.IsTrue(measurements.OrderBy(m => m.Timestamp).SequenceEqual(measurements));
        }
    }

    public abstract class Assert
    {
        public static void IsTrue(bool sequenceEqual)
        {
            throw new NotImplementedException();
        }

        public static void All(List<Measurement> measurements, Action<object> action)
        {
            throw new NotImplementedException();
        }

        public static void AreEqual(int count, object o)
        {
            throw new NotImplementedException();
        }
    }

    public abstract class Measurement
    {
        public decimal Value { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
