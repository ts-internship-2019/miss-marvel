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

        public IActionResult AddEditLandmarkType([Bind("LandmarkTypeId,DictionaryItemCode,DictionaryItemName,Description")] DictionaryLandmarkTypeModel landmarkType, DictionaryLandmarkTypeModel dt)
        {
            if (ModelState.IsValid && dt != null)
            {
                _dictionaryService.AddDictionaryLandmarkType(landmarkType);
            }
            return View();
        }


        public ActionResult GetLandmarkType([DataSourceRequest]DataSourceRequest request)
        {
            int rowsNo = 0;
            var x = _dictionaryService.GetLandmarkType(request.Page, request.PageSize, out rowsNo);
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
            List<DictionaryCity> dictionaryCities = _dictionaryService.GetDictionaryCities();

            return View(dictionaryCities);
        }
    }
}