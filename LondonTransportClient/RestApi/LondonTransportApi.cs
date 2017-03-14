using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json;
using RestSharp;
using Tfl.Api.Presentation.Entities;

namespace LondonTransportClient.RestApi
{
    public class LondonTransportApi
    {
        private const string AppId = "dd94f504";
        private const string AppKey = "0655bb9d0faa12ecf660743871882083";
        private const string ApiRootUrl = @"https://api-argon.tfl.gov.uk";
        private const string OldApiRootUrl = @"https://api.tfl.gov.uk";

        public RouteSearchResponse StationSearch(string term)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            var client = new RestClient($"{ApiRootUrl}/Line/Search/{term}?app_id={AppId}&app_key={AppKey}");
            var request = new RestRequest("/", Method.GET);
            request.AddHeader("Content-Type", "application/json");
            var response = (RestResponse)client.Execute(request);
            var content = response.Content;
            var deserialised = JsonConvert.DeserializeObject<RouteSearchResponse>(content);
            return deserialised;
        }

        public List<RoadCorridor> RoadState()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            var client = new RestClient($"{ApiRootUrl}/Road/?app_id={AppId}&app_key={AppKey}");
            var request = new RestRequest("/", Method.GET);
            request.AddHeader("Content-Type", "application/json");
            var response = (RestResponse)client.Execute(request);
            var content = response.Content;
            var deserialised = JsonConvert.DeserializeObject<List<RoadCorridor>>(content);
            return deserialised;
        }

        public LondonAirForecast AirQuality()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            var client = new RestClient($"{ApiRootUrl}/AirQuality?app_id={AppId}&app_key={AppKey}");
            var request = new RestRequest("/", Method.GET);
            request.AddHeader("Content-Type", "application/json");
            var response = (RestResponse)client.Execute(request);
            var content = response.Content;
            var deserialised = JsonConvert.DeserializeObject<LondonAirForecast>(content);
            return deserialised;
        }

        public CarParkOccupancy GetOccupacityCarParkId(string term)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            var client = new RestClient($"{ApiRootUrl}/Occupancy/CarPark/{term}?app_id={AppId}&app_key={AppKey}");
            var request = new RestRequest("/", Method.GET);
            request.AddHeader("Content-Type", "application/json");
            var response = (RestResponse)client.Execute(request);
            var content = response.Content;
            var deserialised = JsonConvert.DeserializeObject<CarParkOccupancy>(content);
            return deserialised;
        }

        public List<StatusSeverity> RoadDisruption()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            var client = new RestClient($"{ApiRootUrl}/Road/Meta/Severities?app_id={AppId}&app_key={AppKey}");
            var request = new RestRequest("/", Method.GET);
            request.AddHeader("Content-Type", "application/json");
            var response = (RestResponse)client.Execute(request);
            var content = response.Content;
            var deserialised = JsonConvert.DeserializeObject<List<StatusSeverity>>(content);
            return deserialised;
        }

        public object ListOfArrivalPrediction(string term)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            var client = new RestClient($"{ApiRootUrl}/StopPoint/{term}/Arrivals?app_id={AppId}&app_key={AppKey}");
            var request = new RestRequest("/", Method.GET);
            request.AddHeader("Content-Type", "application/json");
            var response = (RestResponse)client.Execute(request);
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                var contentError = response.Content;
                var deserialisedError = JsonConvert.DeserializeObject<ApiError>(contentError);
                return deserialisedError;
            }
            var content = response.Content;
            var deserialised = JsonConvert.DeserializeObject<List<Prediction>>(content);
            return deserialised;
        }

        public List<Prediction> GetVehicleById(string term)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            var client = new RestClient($"{OldApiRootUrl}/Vehicle/{term}/Arrivals?app_id={AppId}&app_key={AppKey}");
            var request = new RestRequest("/", Method.GET);
            request.AddHeader("Content-Type", "application/json");
            var response = (RestResponse)client.Execute(request);
            var content = response.Content;
            var deserialised = JsonConvert.DeserializeObject<List<Prediction>>(content);
            return deserialised;
        }

        public List<Place> SearchBikeStantionByNane(string term)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            var client = new RestClient($"{OldApiRootUrl}/BikePoint/Search?query={term}&app_key={AppKey}&app_id={AppId}");
            var request = new RestRequest("/", Method.GET);
            request.AddHeader("Content-Type", "application/json");
            var response = (RestResponse)client.Execute(request);
            var content = response.Content;
            var deserialised = JsonConvert.DeserializeObject<List<Place>>(content);
            return deserialised;
        }
    }
}