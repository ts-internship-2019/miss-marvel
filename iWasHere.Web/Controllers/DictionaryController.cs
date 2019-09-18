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
using Kendo.Mvc.UI;


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

        public IActionResult LandmarkType(String searchString)
        {

            List<DictionaryLandmarkType> dictionaryLandmarkType = _dictionaryService.GetDictionaryLandmarkType(searchString);

            return View(dictionaryLandmarkType);
        }

        public IActionResult Cities()
        {
          

            return View();
        }

        public IActionResult Cities_Read([DataSourceRequest] DataSourceRequest request)
        {
            int totalCount = 0;
            var X = _dictionaryService.GetDictionaryCities(request.Page, request.PageSize, out totalCount);
            DataSourceResult dataSourceResult = new DataSourceResult();
            dataSourceResult.Data = X;
            dataSourceResult.Total = totalCount;

            return Json(dataSourceResult);
        }
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
    }


    public partial class GridController : Controller
    {

        public IActionResult Period_Read([DataSourceRequest] DataSourceRequest request)
        {
            return Json(GetPeriod().ToDataSourceResult(request));
        }

        private static IEnumerable<DictionaryLandmarkPeriod> GetPeriod()
        {
            using (var landperiod = new MissMarvelContext())
            {
                return landperiod.DictionaryLandmarkPeriod.Select(period => new DictionaryLandmarkPeriod
                {
                    LandmarkPeriodName = period.LandmarkPeriodName
                }).ToList();
            }
        }
    }


   

}
