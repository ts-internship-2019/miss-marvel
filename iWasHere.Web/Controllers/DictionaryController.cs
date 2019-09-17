using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iWasHere.Domain.DTOs;
using iWasHere.Domain.Models;
using iWasHere.Domain.Service;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;

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

        public IActionResult AddEditLandmarkType([Bind("LandmarkTypeId,DictionaryItemCode,DictionaryItemName,Description")] DictionaryLandmarkType landmarkType)
        {
            if (ModelState.IsValid)
            {
                _dictionaryService.AddDictionaryLandmarkType(landmarkType);
            }
            return View();
        }


        public ActionResult GetLandmarkType([DataSourceRequest]DataSourceRequest request)
        {
            return Json(_dictionaryService.GetLandmarkType().ToDataSourceResult(request));
        }

        public IActionResult Cities()
        {
            List<DictionaryCity> dictionaryCities = _dictionaryService.GetDictionaryCities();

            return View(dictionaryCities);
        }
    }
}