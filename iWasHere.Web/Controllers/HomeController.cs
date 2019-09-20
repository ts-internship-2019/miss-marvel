using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using iWasHere.Web.Models;
using Kendo.Mvc.UI;
using iWasHere.Domain.Service;
using iWasHere.Domain.DTOs;
using iWasHere.Domain.Models;

namespace iWasHere.Web.Controllers
{
    public class HomeController : Controller
    {
        //private readonly LandmarkService _LandmarkService;

        //public HomeController(LandmarkService LandmarkService)
        //{
        //    _LandmarkService = LandmarkService;
        //}
        private readonly DictionaryService _LandmarkService;

        public HomeController(DictionaryService LandmarkService)
        {
            _LandmarkService = LandmarkService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult AddLandmark([Bind("LandmarkId, LandmarkName, LandmarkDescription, LandmarkCode")]LandmarkModel landmark, String LandmarkType,
            String LandmarkType_input, String LandmarkPeriod, String LandmarkPeriod_input, String City, String City_input, 
            String StudentPrice, String AdultPrice, String RetiredPrice, String Currency, int id)
        {
            if(id != 0)
            {
                List<TicketXlandmark> pricesList = new List<TicketXlandmark>();
                Landmark loadedLandmark = _LandmarkService.GetLandmark(id, out pricesList);
                LandmarkModel modelToLoad = new LandmarkModel
                {
                    LandmarkId = loadedLandmark.LandmarkId,
                    LandmarkCode = loadedLandmark.LandmarkCode,
                    LandmarkDescription = loadedLandmark.LandmarkDescription,
                    LandmarkName = loadedLandmark.LandmarkName,
                    CityId = Convert.ToInt32(loadedLandmark.CityId),
                    LandmarkTypeId = Convert.ToInt32(loadedLandmark.LandmarkTypeId),
                    LandmarkPeriodId = Convert.ToInt32(loadedLandmark.LandmarkPeriodId),
                    LandmarkPeriodName = loadedLandmark.LandmarkPeriod.LandmarkPeriodName
                    //StudentPrice = Convert.ToDecimal(pricesList[0].TicketValue),
                    //AdultPrice = Convert.ToDecimal(pricesList[1].TicketValue),
                    //RetiredPrice = Convert.ToDecimal(pricesList[2].TicketValue)


                };
                int a = 5;
                return View(modelToLoad);

            }

            if (ModelState.IsValid && landmark != null && LandmarkType != null)
            {
                Landmark l = new Landmark();
                String err;
                l.LandmarkTypeId = Convert.ToInt32(LandmarkType);
                l.CityId = Convert.ToInt32(City);
                l.LandmarkPeriodId = Convert.ToInt32(LandmarkPeriod);
                l.LandmarkName = landmark.LandmarkName;
                l.LandmarkId = landmark.LandmarkId;
                l.LandmarkDescription = landmark.LandmarkDescription;
                l.LandmarkCode = landmark.LandmarkCode;

                List<TicketXlandmark> pricesList = new List<TicketXlandmark>();
                pricesList.Add(new TicketXlandmark(0, 0, 1, Convert.ToInt32(Currency), Convert.ToDecimal(StudentPrice)));
                pricesList.Add(new TicketXlandmark(0, 0, 4, Convert.ToInt32(Currency), Convert.ToDecimal(AdultPrice)));
                pricesList.Add(new TicketXlandmark(0, 0, 3, Convert.ToInt32(Currency), Convert.ToDecimal(RetiredPrice)));
                _LandmarkService.AddEditLandmark(l, pricesList, out err);                
            }

             return View();
        }

        public JsonResult GetLandmarkType(string text)
        {
            int rowsNo = 0;
            if (text == null)
                text = "";
            var x = _LandmarkService.GetLandmarkType(text);
            
            return Json(x);

        }
        public JsonResult GetLandmarkPeriod(string text)
        {
            int rowsNo = 0;
            if (text == null)
                text = "";
            var x = _LandmarkService.GetLandmarkPeriod(text);

            return Json(x);

        }

        public JsonResult GetCities(string text)
        {
            int rowsNo = 0;
            if (text == null)
                text = "";
            var x = _LandmarkService.GetCities(text);

            return Json(x);

        }

        public JsonResult GetCurrencyType(string text)
        {
            int rowsNo = 0;
            if (text == null)
                text = "";
            var x = _LandmarkService.GetCurrencyType(text);

            return Json(x);

        }
    }
}
