using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iWasHere.Domain.DTOs;
using iWasHere.Domain.Models;
using iWasHere.Domain.Service;
using Microsoft.AspNetCore.Mvc;
using iWasHere.Domain.Models;

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


        public IActionResult County()
        {
            List<DictionaryCounty> dictionaryCounty = _dictionaryService.GetDictionaryCounty();

            return View(dictionaryCounty);

        }

    }
}