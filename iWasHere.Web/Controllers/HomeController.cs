using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using iWasHere.Web.Models;
using Kendo.Mvc.UI;
using iWasHere.Domain.Service;


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

        public IActionResult AddLandmark()
        {
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
