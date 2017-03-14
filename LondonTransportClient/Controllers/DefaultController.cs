using System.Web.Mvc;
using LondonTransportClient.RestApi;
using Tfl.Api.Presentation.Entities;

namespace LondonTransportClient.Controllers
{
    public class DefaultController : Controller
    {
        private readonly LondonTransportApi _londonTransportApi = new LondonTransportApi();

        public ActionResult RoadDisruption()
        {
            var response = _londonTransportApi.RoadDisruption();
            return View(response);
        }

        public ActionResult ListOfArrivalPredictionId(string searchPredicationId)
        {
            if (string.IsNullOrEmpty(searchPredicationId) || string.IsNullOrWhiteSpace(searchPredicationId))
            {
                return View("ListOfArrivalPredictionId");
            }
            var response = _londonTransportApi.ListOfArrivalPrediction(searchPredicationId);
            if (response is ApiError)
            {
                return View("ListOfArrivalPredictionError");
            }
            return View(response);
        }

        public ActionResult GetVehicleById(string vehicleId)
        {
            if (string.IsNullOrEmpty(vehicleId) || string.IsNullOrWhiteSpace(vehicleId))
            {
                return View("GetVehicleById");
            }
            var response = _londonTransportApi.GetVehicleById(vehicleId);
            if (response.Count == 0)
            {
                return View("GetVehicleByIdError");
            }
            return View(response);
        }

        public ActionResult SearchBikeStantionByNane(string searchName)
        {
            if (string.IsNullOrEmpty(searchName) || string.IsNullOrWhiteSpace(searchName))
            {
                return View("SearchBikeStantionByNane");
            }
            var response = _londonTransportApi.SearchBikeStantionByNane(searchName);
            if (response.Count == 0)
            {
                return View("SearchBikeStantionByNaneError");
            }
            return View(response);
        }

        public ActionResult StationSearch(string term)
        {
            var response = _londonTransportApi.StationSearch(term);
            return View(response);
        }

        public ActionResult RoadState()
        {
            var response = _londonTransportApi.RoadState();
            return View(response);
        }

        public ActionResult AirQuality()
        {
            var response = _londonTransportApi.AirQuality();
            return View(response);
        }

        public ActionResult GetOccupacityCarParkId(string searchId)
        {
            if (searchId == null)
            {
                return View("GetOccupacityCarParkId");
            }
            var response = _londonTransportApi.GetOccupacityCarParkId(searchId);
            return View(response);
        }
    }
}