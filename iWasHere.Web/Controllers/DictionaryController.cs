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

            return View();
        }

        public IActionResult LandmarkType()
        {


            return View();
        }

        public ActionResult AddEditLandmarkType(DictionaryLandmarkTypeModel dt)
        {
            if (ModelState.IsValid && dt != null)
            {
                _dictionaryService.AddDictionaryLandmarkType(dt);
            }
            return View();
        }
        public ActionResult GetLT([DataSourceRequest] DataSourceRequest request, String LandmarkTypeId)
        {
            int rowsNo = 0;
            var x = _dictionaryService.LoadLandmarkType(Convert.ToInt32(LandmarkTypeId));
            return Json(x);

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
        [HttpPost]
        public JsonResult GetAjax(String filter)
        {
            String s = filter;
            //return Json(_dictionaryService.GetLandmarkType(request.Page, request.PageSize).ToDataSourceResult(request));
            return null;
        }
        public IActionResult Cities()
        {
          

            return View();
        }

        //public IActionResult Cities_Read([DataSourceRequest] DataSourceRequest request)
        //{
        //    int totalCount = 0;
        //    var X = _dictionaryService.GetDictionaryCities(request.Page, request.PageSize, out totalCount);
        //    DataSourceResult dataSourceResult = new DataSourceResult();
        //    dataSourceResult.Data = X;
        //    dataSourceResult.Total = totalCount;

        //    return Json(dataSourceResult);
        //}

          public IActionResult AddEditCity([Bind("CityId,CityName,CityCode")] DictionaryCityModel cityModel, DictionaryCityModel dt)
        {
            if (ModelState.IsValid && dt != null)
            {
                _dictionaryService.AddDictionaryLandmarkType(cityModel);
            }
            return View();
        }



        public ActionResult GetCities([DataSourceRequest]DataSourceRequest request, String lFilter,String text)
        {
            int rowsNo = 0;
            var x = _dictionaryService.GetCities(request.Page, request.PageSize, out rowsNo, lFilter,Convert.ToInt32(text));
            DataSourceResult dataSource = new DataSourceResult();
            dataSource.Data = x;
            dataSource.Total = rowsNo;
            return Json(dataSource);
            
        }

        //public ActionResult GetComboCountyyy()
        //{
        //    List<DictionaryCountyModel> comboCounty = _dictionaryService.GetComboCounty();


        //    return Json(comboCounty);
        //}
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

        //[HttpPost]
        //public JsonResult GetAjax(String filter)
        //{
        //    String s = filter;

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

        // ------------------- Currency
         public IActionResult Currency()
        {
            return View();
        }

            public IActionResult Currency_Read([DataSourceRequest] DataSourceRequest request)  //paginare
            {
            int totalCount = 0;
            var y = _dictionaryService.GetDictionaryCurrencyType(request.Page, request.PageSize, out totalCount);
            DataSourceResult dataSource = new DataSourceResult();
            dataSource.Data = y;
            dataSource.Total = totalCount;

                return Json(dataSource);
            }

            private static IEnumerable<DictionaryCurrencyType> GetCurrency()
            {
                using (var northwind = new MissMarvelContext())
                {
                    return northwind.DictionaryCurrencyType.Select(currency => new DictionaryCurrencyType
                    {
                 
                        CurrencyName = currency.CurrencyName,
                        CurrencyCode = currency.CurrencyCode,
                        CurrencyExRate= currency.CurrencyExRate

                    }).ToList();
                }
            }

        // ------------------- County

        public IActionResult County()
        {

            return View();
        }

        public IActionResult County_Read([DataSourceRequest] DataSourceRequest request)
        {
            int totalCount = 0;
            var x = _dictionaryService.GetDictionaryCounty(request.Page, request.PageSize, out totalCount);
            DataSourceResult dataSourceResult = new DataSourceResult();
            dataSourceResult.Data = x;
            dataSourceResult.Total = totalCount;

            return Json(dataSourceResult);
        }

        private static IEnumerable<DictionaryCounty> GetCounty()
        {
            using (var northwind = new MissMarvelContext())
            {
                return northwind.DictionaryCounty.Select(county => new DictionaryCounty
                {
                    CountyCode = county.CountyCode,
                    CountyName = county.CountyName

                }).ToList();
            }
        }

        // ------------------- Country

        public IActionResult Country()
        {
            //List<DictionaryCountry> dictionaryCountries = _dictionaryService.GetDictionaryCountries();
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

        // ------------------- LandmarkPeriod

        public IActionResult LandmarkPeriod()
        {
            return View();
        }
        public IActionResult LandmarkPeriod_Read([DataSourceRequest] DataSourceRequest request)
        {
            int totalCount = 0;
            var data = _dictionaryService.GetDictionaryLandmarkPeriods(request.Page, request.PageSize, out totalCount);
            DataSourceResult dataSourceResult = new DataSourceResult();
            dataSourceResult.Data = data;
            dataSourceResult.Total = totalCount;

            return Json(dataSourceResult);
        }

        // ------------------- TicketType

        public IActionResult TicketType()
        {
            return View();
        }

        //public IActionResult TicketType_Read([DataSourceRequest] DataSourceRequest request,string FilterTicketType)
        //{
        //    int totalCount = 0;
        //    var data = _dictionaryService.GetDictionaryTicketType(request.Page, request.PageSize, out totalCount, FilterTicketType);
        //    DataSourceResult dataSourceResult = new DataSourceResult();
        //    dataSourceResult.Data = data;
        //    dataSourceResult.Total = totalCount;

        //    return Json(dataSourceResult);
        //}

        public IActionResult InsertUpdateTicketType()
        {
            return View();
        }





    }

}
