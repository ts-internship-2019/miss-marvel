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
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

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
        private readonly IHostingEnvironment hostingEnvironment;

        public HomeController(DictionaryService LandmarkService,
            IHostingEnvironment hostingEnvironment)
        {
            _LandmarkService = LandmarkService;
            this.hostingEnvironment = hostingEnvironment;
        }
        public IActionResult LandmarkList(string landmarkName, string City)
        {
             LandmarkModelList landmarkModelList = new LandmarkModelList();

              landmarkModelList.LandmarkList = _LandmarkService.GetLandmarkListFiltered(landmarkName,City);
               for (int i = 0; i < landmarkModelList.LandmarkList.Count; i++)
               {
                   landmarkModelList.LandmarkList[i].Pictures = _LandmarkService.GetLandmarkPictures(landmarkModelList.LandmarkList[i].LandmarkId);
                }

           
            return View(landmarkModelList);
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

        public IActionResult AddLandmark([Bind("LandmarkId, LandmarkName, LandmarkDescription, LandmarkCode, Photos, Latitude, Longitude")]LandmarkModel landmark, String LandmarkTypeId,
          String LandmarkTypeId_input, String LandmarkPeriodId, String LandmarkPeriodId_input, String CityId, String CityId_input,
          String StudentPrice, String AdultPrice, String RetiredPrice, String CurrencyId, String CurrencyId_input, int id)
        {
            if (id != 0)
            {
                List<TicketXlandmark> pricesList = new List<TicketXlandmark>();
                DictionaryCurrencyType currency = new DictionaryCurrencyType();
                Landmark loadedLandmark = _LandmarkService.GetLandmark(id, out pricesList, out currency);
                LandmarkModel modelToLoad = new LandmarkModel
                {
                    LandmarkId = loadedLandmark.LandmarkId,
                    LandmarkCode = loadedLandmark.LandmarkCode,
                    LandmarkDescription = loadedLandmark.LandmarkDescription,
                    LandmarkName = loadedLandmark.LandmarkName,
                    CityId = Convert.ToInt32(loadedLandmark.City.CityId),
                    CityName = loadedLandmark.City.CityName,
                    LandmarkTypeId = Convert.ToInt32(loadedLandmark.LandmarkTypeId),
                    LandmarkTypeName = loadedLandmark.LandmarkType.DictionaryItemName,
                    LandmarkPeriodId = Convert.ToInt32(loadedLandmark.LandmarkPeriodId),
                    LandmarkPeriodName = loadedLandmark.LandmarkPeriod.LandmarkPeriodName,




                };
                if (pricesList.Count > 0 && currency != null)
                {
                    modelToLoad.CurrencyId = currency.CurrencyTypeId;
                    modelToLoad.CurrencyName = currency.CurrencyName;
                    modelToLoad.StudentPrice = Convert.ToDecimal(pricesList[0].TicketValue);
                    modelToLoad.AdultPrice = Convert.ToDecimal(pricesList[1].TicketValue);
                    modelToLoad.RetiredPrice = Convert.ToDecimal(pricesList[2].TicketValue);
                }

                return View(modelToLoad);

            }

            if (ModelState.IsValid && landmark != null && LandmarkTypeId != null)
            {
                Landmark l = new Landmark();
                List<LandmarkPicture> pictures = new List<LandmarkPicture>();
                String err;
                String uniqueFileName = null;
                if (landmark.Photos != null && landmark.Photos.Count > 0)
                {
                    foreach (IFormFile photo in landmark.Photos)
                    {
                        String uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                        uniqueFileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
                        String filePath = Path.Combine(uploadsFolder, uniqueFileName);
                        photo.CopyTo(new FileStream(filePath, FileMode.Create));
                        pictures.Add(new LandmarkPicture(0, uniqueFileName));
                    }
                }
                l.LandmarkTypeId = Convert.ToInt32(LandmarkTypeId);
                l.CityId = Convert.ToInt32(CityId);
                l.LandmarkPeriodId = Convert.ToInt32(LandmarkPeriodId);
                l.LandmarkName = landmark.LandmarkName;
                l.LandmarkId = landmark.LandmarkId;
                l.LandmarkDescription = landmark.LandmarkDescription;
                l.LandmarkCode = landmark.LandmarkCode;
                l.Latitude = landmark.Latitude;
                l.Longitude = landmark.Longitude;
                List<TicketXlandmark> pricesList = new List<TicketXlandmark>();
                if (StudentPrice != null)
                {

                    pricesList.Add(new TicketXlandmark(0, l.LandmarkId, 1, Convert.ToInt32(CurrencyId), Convert.ToDecimal(StudentPrice)));
                    pricesList.Add(new TicketXlandmark(0, l.LandmarkId, 137, Convert.ToInt32(CurrencyId), Convert.ToDecimal(AdultPrice)));
                    pricesList.Add(new TicketXlandmark(0, l.LandmarkId, 3, Convert.ToInt32(CurrencyId), Convert.ToDecimal(RetiredPrice)));
                }
                _LandmarkService.AddEditLandmark(l, pricesList, out err, pictures);
                if (!String.IsNullOrWhiteSpace(err))
                {
                    ModelState.AddModelError(string.Empty, err);
                }
            }

            return View(new LandmarkModel());
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

        public JsonResult GetCurrencyTypeCombo(string text)
        {
            int rowsNo = 0;
            if (text == null)
                text = "";
            var x = _LandmarkService.GetCurrencyTypeCombo(text);

            return Json(x);

        }

        public IActionResult LandmarkView(int landmarkId =47)
        {
            var landmarkDetails = _LandmarkService.GetLandmark(landmarkId, out List<TicketXlandmark> priceList, out DictionaryCurrencyType dictionaryCurrencyType);
            LandmarkModel landmarkModel = new LandmarkModel();
            landmarkModel.LandmarkId = landmarkDetails.LandmarkId;
            landmarkModel.Pictures = _LandmarkService.GetLandmarkPictures(landmarkId);
            landmarkModel.LandmarkName = landmarkDetails.LandmarkName;
            landmarkModel.LandmarkDescription = landmarkDetails.LandmarkDescription;
            landmarkModel.LandmarkTicket = landmarkDetails.LandmarkTicket;
            landmarkModel.LandmarkCode = landmarkDetails.LandmarkCode;
            landmarkModel.LandmarkPeriodName = landmarkDetails.LandmarkPeriod.LandmarkPeriodName;
            landmarkModel.LandmarkType = new DictionaryLandmarkTypeModel();
            landmarkModel.City = new DictionaryCityModel();
            landmarkModel.City.CityName = landmarkDetails.City.CityName;
            landmarkModel.Latitude = landmarkDetails.Latitude;
            landmarkModel.Longitude = landmarkDetails.Longitude;
         
            
            for (int i = 0; i < priceList.Count; i++)
            {
                if (priceList[i].TicketTypeId == 1)
                    landmarkModel.StudentPrice = Convert.ToDecimal(priceList[i].TicketValue);
                if (priceList[i].TicketTypeId == 3)
                    landmarkModel.RetiredPrice = Convert.ToDecimal(priceList[i].TicketValue);
                if (priceList[i].TicketTypeId == 137)
                    landmarkModel.AdultPrice = Convert.ToDecimal(priceList[i].TicketValue);
            }
            landmarkModel.LandmarkType.DictionaryItemName = landmarkDetails.LandmarkType.DictionaryItemName;



            return View(landmarkModel);
        }
    }
}
