using System;
using System.Collections.Generic;
using System.Linq;
using Kendo.Mvc.UI;
using System.Threading.Tasks;
using iWasHere.Domain.DTOs;
using iWasHere.Domain.Models;
using iWasHere.Domain.Service;
using Kendo.Mvc.Extensions;
using Microsoft.AspNetCore.Mvc;
using iWasHere.Web;
using iWasHere.Domain;


namespace iWasHere.Web.Controllers
{
    public class DictionaryController : Controller
    {
        private readonly DictionaryService _dictionaryService;

        public DictionaryController(DictionaryService dictionaryService)
        {
            _dictionaryService = dictionaryService;
        }

        public IActionResult Index()
        {
            List<DictionaryLandmarkTypeModel> dictionaryLandmarkTypeModels = _dictionaryService.GetDictionaryLandmarkTypeModels();

            return View(dictionaryLandmarkTypeModels);
        }

      
        #region Cata
        public IActionResult Cities()
        {


            return View();
        }
        public IActionResult AddDictionaryCity()
        {
            return View();
        }
        public IActionResult EditDictionaryCity([Bind("CityId, CityCode, CityName, CuntyId")]DictionaryCity dt,int id)
        {
            String exMessage;
            if (ModelState.IsValid && dt.CityName != null)
            {
                var result = _dictionaryService.EditDictionaryCity(dt, out exMessage);
                if (result == null)
                {
                    ModelState.AddModelError(string.Empty, exMessage);
                    return View();
                }
            }
            if (id != 0)
            {
                return View(_dictionaryService.GetDictionaryCity(id));
            }
            else
                return View();
        }
        public ActionResult GetCities([DataSourceRequest]DataSourceRequest request, String lFilter, String text)
        {
            int rowsNo = 0;
            var x = _dictionaryService.GetCities(request.Page, request.PageSize, out rowsNo, lFilter, Convert.ToInt32(text));
            DataSourceResult dataSource = new DataSourceResult();
            dataSource.Data = x;
            dataSource.Total = rowsNo;
            return Json(dataSource);

        }
        public ActionResult DeleteCity([DataSourceRequest] DataSourceRequest request, int id)
        {
            if (id != -1)
            {
                _dictionaryService.DeleteCity(id);
            }

            return Json(ModelState.ToDataSourceResult());
        }



        public JsonResult GetComboCounty(string text)
        {
            if (String.IsNullOrEmpty(text))
            {
                text = "";
            }
            List<DictionaryCounty> result = GetComboCounties(text);
            return Json(result);
        }

        public List<DictionaryCounty> GetComboCounties(string filterCounty)
        {
            List<DictionaryCounty> countyModels = _dictionaryService.GetComboCounty(filterCounty);
            return countyModels;
        }

        public ActionResult AddCity (string cityName,string cityCode,string countyId)
        {
            var x = new DictionaryCity();
            if (cityName !=null && cityCode != null && countyId != null)
            {
                 x= _dictionaryService.AddDictionaryCity(cityName,cityCode,Convert.ToInt32(countyId));
            }

            return Json(x);
        }

        public IActionResult LandmarkDetails(int landmarkId = 33)
        {
            var landmarkDetails = _dictionaryService.GetLandmark(landmarkId, out List<TicketXlandmark> priceList, out DictionaryCurrencyType dictionaryCurrencyType);
            LandmarkModel landmarkModel = new LandmarkModel();
            landmarkModel.Pictures = _dictionaryService.GetLandmarkPictures(landmarkId);
            landmarkModel.LandmarkName = landmarkDetails.LandmarkName;
            landmarkModel.LandmarkDescription = landmarkDetails.LandmarkDescription;
            landmarkModel.LandmarkTicket = landmarkDetails.LandmarkTicket;
            landmarkModel.LandmarkCode = landmarkDetails.LandmarkCode;
            landmarkModel.LandmarkPeriodName = landmarkDetails.LandmarkPeriod.LandmarkPeriodName;
            landmarkModel.LandmarkType = new DictionaryLandmarkTypeModel();
            landmarkModel.City = new DictionaryCityModel();
            landmarkModel.City.CityName = landmarkDetails.City.CityName;
            for(int i = 0;i< priceList.Count;i++)
            {
                if(priceList[i].TicketTypeId == 1)
                landmarkModel.StudentPrice = Convert.ToDecimal(priceList[i].TicketValue);
                if(priceList[i].TicketTypeId ==3)
                landmarkModel.RetiredPrice = Convert.ToDecimal(priceList[i].TicketValue);
                if(priceList[i].TicketTypeId ==137)
                landmarkModel.AdultPrice = Convert.ToDecimal(priceList[i].TicketValue);
            }
            landmarkModel.LandmarkType.DictionaryItemName = landmarkDetails.LandmarkType.DictionaryItemName;
            


            return View(landmarkModel);
        }
        

        #endregion

        #region Mihnea
        public IActionResult LandmarkType()
        {


            return View();
        }
        public ActionResult AddEditLandmarkType([Bind("DictionaryItemId, DictionaryItemCode, DictionaryItemName, Description")]DictionaryLandmarkType dt, int id)
        {

            String exMessage;
            if (ModelState.IsValid && dt.DictionaryItemCode != null)
            {
                var result = _dictionaryService.AddEditDictionaryLandmarkType(dt, out exMessage);
                if (result == null)
                {
                    ModelState.AddModelError(string.Empty, exMessage);
                    return View();
                }
            }
            if (id != 0)
            {
                return View(_dictionaryService.GetDictionaryLandmarkType(id));
            }
            else
            {
                return View();
            }
        }
        public ActionResult AddDictionaryCountry(DictionaryCountryModel country)
        {
            if (ModelState.IsValid && country != null)
            {
                _dictionaryService.AddDictionaryCountry(country);
            }
            return View();
        }

        public ActionResult GetLT([DataSourceRequest] DataSourceRequest request, String LandmarkTypeId)
        {
            var x = _dictionaryService.LoadLandmarkType(Convert.ToInt32(LandmarkTypeId));
            return Json(x);

        }

        public ActionResult DeleteLandmarkType([DataSourceRequest] DataSourceRequest request, int id)
        {
            if (id != -1)
            {
                _dictionaryService.DeleteLandmarkType(id);
            }

            return Json(ModelState.ToDataSourceResult());
        }


        public ActionResult GetLandmarkType([DataSourceRequest]DataSourceRequest request, String lFilter)
        {
            int rowsNo = 0;
            var x = _dictionaryService.GetLandmarkType(request.Page, request.PageSize, out rowsNo, lFilter);
            DataSourceResult dataSource = new DataSourceResult();
            dataSource.Data = x;
            dataSource.Total = rowsNo;
            return Json(dataSource);

        }

        #endregion

        #region Corina
        public IActionResult Currency_Read([DataSourceRequest] DataSourceRequest request)  //paginare
        {
            int totalCount = 0;
            var y = _dictionaryService.GetDictionaryCurrencyType(request.Page, request.PageSize, out totalCount);
            DataSourceResult dataSource = new DataSourceResult();
            dataSource.Data = y;
            dataSource.Total = totalCount;

            return Json(dataSource);
        }

        public ActionResult GetCurrencies([DataSourceRequest]DataSourceRequest request, String lFilter)
        {
            int rowsNo = 0;
            var x = _dictionaryService.GetDictionaryCurrencyTypes(request.Page, request.PageSize, out rowsNo, lFilter);
            DataSourceResult dataSource = new DataSourceResult();
            dataSource.Data = x;
            dataSource.Total = rowsNo;
            return Json(dataSource);

        }
         public JsonResult GetAjax(String filter)
        {
            String s = filter;
            return null;
        }
        public IActionResult Currency()
        {
            return View();
        }

        public ActionResult AddEditCurrency([Bind("CurrencyTypeId, CurrencyName, CurrencyCode, CurrencyExRate")] DictionaryCurrencyType dc, int id)
        {
            String exMessage;
            if (dc.CurrencyCode != null)
            {
                var result = _dictionaryService.AddEditDictionaryCurrencyType(dc, out exMessage);
                if (result == null)
                {
                    ModelState.AddModelError(string.Empty, exMessage);
                    return View();
                }
            }
            if (id != 0)
            {
                return View(_dictionaryService.GetDictionaryCurrencyType(id));
            }
            else
            {
                return View();
            }
        }

        public ActionResult iEdit(int id, string code, string name, decimal rate)
        {
        
            String err;
            DictionaryCurrencyType ct = _dictionaryService.GetDictionaryCurrencyType(id);
            if(ct == null)
            {
                return View();
            }
            ct.CurrencyCode = code;
            ct.CurrencyExRate = rate;
            ct.CurrencyName = name;
            ct.CurrencyTypeId = id;
            _dictionaryService.AddEditDictionaryCurrencyType(ct, out err);
            return Json(ModelState.ToDataSourceResult());
        }

        private static IEnumerable<DictionaryCurrencyType> GetCurrency()
        {
            using (var northwind = new MissMarvelContext())
            {
                return northwind.DictionaryCurrencyType.Select(currency => new DictionaryCurrencyType
                {

                    CurrencyName = currency.CurrencyName,
                    CurrencyCode = currency.CurrencyCode,
                    CurrencyExRate = currency.CurrencyExRate

                }).ToList();
            }
        }


         public IActionResult AddDictionaryCurrencyType([Bind("CurrencyTypeId,CurrencyName,CurrencyCode, CurrencyExRate")] DictionaryCurrencyTypeModel currencyModel, DictionaryCurrencyTypeModel dt)
        {
            if (ModelState.IsValid && dt != null)
            {
                _dictionaryService.AddDictionaryCurrencyType(currencyModel);
            }
            return View();
        }

        public IActionResult onUpdate()
        {
            return Redirect("/Dictionary/Currency");
        } 

        public ActionResult AddEditCurrencyType(DictionaryCurrencyTypeModel dt)
        {
            if (ModelState.IsValid && dt != null)
            {
                _dictionaryService.AddDictionaryCurrencyType(dt);
            }
            return View();
        }

        public ActionResult DeleteCurrencyType([DataSourceRequest]DataSourceRequest request, int id)
        {
            if(id != -1)
            {
                _dictionaryService.DeleteCurrencyType(id);
            }
            return Json(ModelState.ToDataSourceResult());
        }


        #endregion

        #region Iuliana
        public ActionResult GetCountry([DataSourceRequest]DataSourceRequest request, String lFilter)
        {
            int rowsNo = 0;
            var x = _dictionaryService.GetCountry(request.Page, request.PageSize, out rowsNo, lFilter);
            DataSourceResult dataSource = new DataSourceResult();
            dataSource.Data = x;
            dataSource.Total = rowsNo;
            return Json(dataSource);

        }
        [HttpPost]
       
        //public List<DictionaryCounty> GetComboCounties(string filterCounty)
        //{
        //    List<DictionaryCounty> countyModels = _dictionaryService.GetComboCounty(filterCounty);
        //    return countyModels;
        //}
        public JsonResult GetJsonResult(String filter)
        {
            String s = filter;
            return null;
        }

        public ActionResult GetDictionaryCountry([DataSourceRequest] DataSourceRequest request, String CountryId)
        {
            int rowsNo = 0;
            var x = _dictionaryService.LoadCountry(Convert.ToInt32(CountryId));
            return Json(x);

        }


        public IActionResult Country()
        {
            return View();
        }



        public IActionResult Country_Read([DataSourceRequest] DataSourceRequest request)
        {
            int totalCount = 0;
            var y = _dictionaryService.GetDictionaryCountries(request.Page, request.PageSize, out totalCount);
            DataSourceResult dataSource = new DataSourceResult();
            dataSource.Data = y;
            dataSource.Total = totalCount;

            return Json(dataSource);
        }

        private static IEnumerable<DictionaryCountry> GetCountry()
        {
            using (var northwind = new MissMarvelContext())
            {
                return northwind.DictionaryCountry.Select(country => new DictionaryCountry
                {
                    CountryCode = country.CountryCode,
                    CountryName = country.CountryName

                }).ToList();
            }
        }

        public ActionResult DeleteCountry([DataSourceRequest] DataSourceRequest request, int id)
        {
            if (id != -1)
            {
                _dictionaryService.DeleteCountry(id);
            }
            return Json(ModelState.ToDataSourceResult());
        }



        public ActionResult AddDictionaryCountry([Bind("CountryId, CountryName, CountryCode")] DictionaryCountry dc, int id)
        {
            String exMessage;
            if (dc.CountryName != null)
            {
                var result = _dictionaryService.AddEditDictionaryCountry(dc, out exMessage);
                if (result == null)
                {
                    ModelState.AddModelError(string.Empty, exMessage);
                    return View();
                }
            }
            if (id != 0)
            {
                return View(_dictionaryService.GetDictionaryCountry(id));
            }
            else
            {
                return View();
            }
        }


        public ActionResult AddEditDictionaryCountry(DictionaryCountryModel country)
        {
            if (ModelState.IsValid && country != null)
            {
               _dictionaryService.AddDictionaryCountry(country);
            }
            return View();
        }


        public ActionResult Edit(int id, string name, string code)
        {
            String err;
            DictionaryCountry ct = _dictionaryService.GetDictionaryCountry(id);
            if (ct == null)
            {
                return View();
            }
            ct.CountryId = id;
            ct.CountryName = name;
            ct.CountryCode = code;
            _dictionaryService.AddEditDictionaryCountry(ct, out err);
            return Json(ModelState.ToDataSourceResult());
        }


        #endregion

        #region Gabi
        public IActionResult TicketType_Read([DataSourceRequest] DataSourceRequest request, string FilterTicketType)
        {
            int totalCount = 0;
            var data = _dictionaryService.GetDictionaryTicketType(request.Page, request.PageSize, out totalCount, FilterTicketType);
            DataSourceResult dataSourceResult = new DataSourceResult();
            dataSourceResult.Data = data;
            dataSourceResult.Total = totalCount;

            return Json(dataSourceResult);
        }

        public IActionResult TicketType()
        {
            return View();
        }

        public IActionResult AddEditTicketType(string id)
        {
            if (Convert.ToInt32(id) == 0)
            {
                return View();
            }
            else
            {
                DictionaryTicketType ticketType = _dictionaryService.GetTicketTypeId(Convert.ToInt32(id));
                return View(ticketType);
            }
        }

        public IActionResult DeleteTicketType([DataSourceRequest] DataSourceRequest request, int id)
        {
            if (id != -1)
            {
                _dictionaryService.DeleteTicket(id);
            }
            return Json(ModelState.ToDataSourceResult());

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddEditTicketType(DictionaryTicketType ticketType, int id)
        {
            if (id == 0)
            {
                _dictionaryService.AddTicket(ticketType);
                return View();
            }
            else
            {
                ticketType.TicketTypeId = id;
                _dictionaryService.EditTicket(ticketType);
                return View("TicketType");
            }

        }

        public IActionResult IndexComments()
        {
            List<LandmarkReview> landmarkReviews = null;// _dictionaryService.GetDbCommentsAll();
            return View(landmarkReviews);
        }

        public IActionResult AddReview()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddReview(LandmarkReview review, int id)
        {
            //if (id == 0)
                _dictionaryService.AddReview(review);
                return View();
        }

  




            #endregion

        #region Victor

            public IActionResult County()
        {

            return View();
        }

        public ActionResult AddEditCounty([Bind("CountyId, CountyCode, CountyName, CountryId")]DictionaryCounty dt, int id)
        {
            int k;
            String exMessage;
            if (/*ModelState.IsValid &&*/ dt.CountyCode != null)
            {
                 _dictionaryService.AddDictionaryCounty(dt);
            }
            return View();
        }

        public ActionResult GetCounty([DataSourceRequest]DataSourceRequest request, String lFilter, String text)
        {
            int rowsNo = 0;
            var x = _dictionaryService.GetCounty(request.Page, request.PageSize, out rowsNo, lFilter, Convert.ToInt32(text));
            DataSourceResult dataSource = new DataSourceResult();
            dataSource.Data = x;
            dataSource.Total = rowsNo;
            return Json(dataSource);
        }

        public JsonResult GetComboCountry(string text)
        {
            if (String.IsNullOrEmpty(text))
            {
                text = "";
            }
            List<DictionaryCountry> result = GetComboCountries(text);
            return Json(result);
        }

        public List<DictionaryCountry> GetComboCountries(string filterCountry)
        {
            List<DictionaryCountry> countyModels = _dictionaryService.GetComboCountry(filterCountry);
            return countyModels;
        }

        public ActionResult GetCO([DataSourceRequest] DataSourceRequest request, String CountyId)
        {
            int rowsNo = 0;
            var x = _dictionaryService.LoadCounty(Convert.ToInt32(CountyId));
            return Json(x);

        }

        public ActionResult DeleteCounty([DataSourceRequest] DataSourceRequest request, int id)
        {
            if (id != -1)
            {
                _dictionaryService.DeleteCounty(id);
            }

            return Json(ModelState.ToDataSourceResult());
        }

        public IActionResult onUpdateCountry()
        {
            return Redirect("/Dictionary/Country");
        }
        #endregion

        #region Dorin
        public IActionResult LandmarkPeriod()
        {
            return View();
        }
        public IActionResult LandmarkPeriod_Read([DataSourceRequest] DataSourceRequest request, string search)
        {
            int totalCount = 0;
            var data = _dictionaryService.GetDictionaryLandmarkPeriods(request.Page, request.PageSize, out totalCount, search);
            DataSourceResult dataSourceResult = new DataSourceResult();
            dataSourceResult.Data = data;
            dataSourceResult.Total = totalCount;

            return Json(dataSourceResult);
        }

        public IActionResult AddEditLandmarkPeriod(int id)
        {
            return View(_dictionaryService.GetDictionaryLandmarkPeriod(id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddEditLandmarkPeriod(DictionaryLandmarkPeriod landmarkPeriod)
        {
            if (landmarkPeriod.LandmarkPeriodId == 0 || landmarkPeriod.LandmarkPeriodId ==null)
            {
                _dictionaryService.AddEditLandmarkPeriods(landmarkPeriod);
                ModelState.Clear();
            }
            else
            {
                _dictionaryService.AddEditLandmarkPeriods(landmarkPeriod);
            }
            return View();


        }
        public ActionResult DeletePeriod(int id)
        {
            _dictionaryService.DeletePeriod(id);
            return Json(ModelState.ToDataSourceResult());
        }
        #endregion

        #region SomethingWeird
        //[HttpPost]
        //public JsonResult GetAjax(String filter)
        //{
        //    String s = filter;
        //    //return Json(_dictionaryService.GetLandmarkType(request.Page, request.PageSize).ToDataSourceResult(request));
        //    return null;
        //}




        public partial class TextBox : Controller
        {

            public ActionResult Index()
            {

                return View();
            }
        }

        public partial class ButtonController : Controller
        {

            public IActionResult Events()
            {
                // Actiune pe add

                return View();
            }
        }
        #endregion

        #region Gunoi
        //public IActionResult Cities_Read([DataSourceRequest] DataSourceRequest request)
        //{
        //    int totalCount = 0;
        //    var X = _dictionaryService.GetDictionaryCities(request.Page, request.PageSize, out totalCount);
        //    DataSourceResult dataSourceResult = new DataSourceResult();
        //    dataSourceResult.Data = X;
        //    dataSourceResult.Total = totalCount;

        //    return Json(dataSourceResult);
        //}

        //  public IActionResult AddEditCity([Bind("CityId,CityName,CityCode")] DictionaryCityModel cityModel, DictionaryCityModel dt)
        //{
        //    if (ModelState.IsValid && dt != null)
        //    {
        //        _dictionaryServicesk(cityModel);
        //    }
        //    return View();
        //}





        //public ActionResult AddEditCountryType([Bind("CountryId, CountryName, CountryCode")]DictionaryCountry dc, int id)
        //{

        //    String exMessage;
        //    if (dc.CountryCode != null)
        //    {
        //        var result = _dictionaryService.AddEditDictionaryCountry(dc, out exMessage);
        //        if (result == null)
        //        {
        //            ModelState.AddModelError(string.Empty, exMessage);
        //            return View();
        //        }
        //    }
        //    if (id != 0)
        //    {
        //        return View(_dictionaryService.GetDictionaryCountry(id));
        //    }
        //    else
        //    {
        //        return View();
        //    }
        //}





        //[HttpPost]
        //public JsonResult GetAjax(String filter)
        //{
        //    String s = filter;

        //    return null;
        //}

        //private static IEnumerable<DictionaryCounty> GetCounty()
        //{
        //    using (var northwind = new MissMarvelContext())
        //    {
        //        return northwind.DictionaryCounty.Select(county => new DictionaryCounty
        //        {
        //            CountyCode = county.CountyCode,
        //            CountyName = county.CountyName

        //        }).ToList();
        //    }
        //}

        // ------------------- Country



        // ------------------- LandmarkPeriod




        #endregion

    }

}

