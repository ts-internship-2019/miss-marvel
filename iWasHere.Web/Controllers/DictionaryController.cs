﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iWasHere.Domain.DTOs;
using iWasHere.Domain.Models;
using iWasHere.Domain.Service;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using iWasHere.Domain.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System.Linq;


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

        public IActionResult Country()
        {
            List<DictionaryCountry> dictionaryCountries = _dictionaryService.GetDictionaryCountries();
            return View(dictionaryCountries);
        }



        public IActionResult LandmarkType(String searchString)
        {

            List<DictionaryLandmarkType> dictionaryLandmarkType = _dictionaryService.GetDictionaryLandmarkType(searchString);

            return View(dictionaryLandmarkType);
        }

        public IActionResult Cities()
        {
            List<DictionaryCity> dictionaryCities = _dictionaryService.GetDictionaryCities();

            return View(dictionaryCities);
        }
        
        public IActionResult Currency()
        {
            List<DictionaryCurrencyType> dictionaryCurrencies = _dictionaryService.GetDictionaryCurrencyType();
            return View(dictionaryCurrencies);
        }


        
     
        

            public IActionResult Currency_Read([DataSourceRequest] DataSourceRequest request)
            {
                return Json(GetCurrency().ToDataSourceResult(request));
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

         
            

       

        public IActionResult County()
        {
            List<DictionaryCounty> dictionaryCounty = _dictionaryService.GetDictionaryCounty();

            return View(dictionaryCounty);

        }



       

   

        public IActionResult Country_Read([DataSourceRequest] DataSourceRequest request)
        {
            return Json(GetCountry().ToDataSourceResult(request));
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
}