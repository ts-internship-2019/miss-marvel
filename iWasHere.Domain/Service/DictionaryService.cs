using iWasHere.Domain.DTOs;
using iWasHere.Domain.Model;
using iWasHere.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iWasHere.Domain.Service
{
    public class DictionaryService
    {
        private readonly MissMarvelContext _dbContext;
        public DictionaryService(MissMarvelContext databaseContext)
        {
            _dbContext = databaseContext;
        }

        public List<DictionaryLandmarkTypeModel> GetDictionaryLandmarkTypeModels()
        {
            List<DictionaryLandmarkTypeModel> dictionaryLandmarkTypeModels = _dbContext.DictionaryLandmarkType.Select(a => new DictionaryLandmarkTypeModel()
            {
                Id = a.DictionaryItemId,
                Name = a.DictionaryItemName
            }).ToList();

            return dictionaryLandmarkTypeModels;
        }

        //public List<Models.DictionaryLandmarkType> GetDictionaryLandmarkType(String searchText)
        //{

        //    List<Models.DictionaryLandmarkType> dictionaryLandmarkType;
        //    //if (!String.IsNullOrWhiteSpace(searchText))
        //    //{
        //    //    dictionaryLandmarkType = _dbContext.DictionaryLandmarkType.Where(a => a.DictionaryItemName.StartsWith(searchText)).Select(a => new Models.DictionaryLandmarkType()
        //    //    {
        //    //        DictionaryItemId = a.DictionaryItemId,
        //    //        DictionaryItemName = a.DictionaryItemName,
        //    //        DictionaryItemCode = a.DictionaryItemCode,
        //    //        Description = a.Description

        //    //    }).ToList();
        //    //}
        //    //else
            
        //        dictionaryLandmarkType = _dbContext.DictionaryLandmarkType.Select(a => new Models.DictionaryLandmarkType()
        //        {
        //            DictionaryItemId = a.DictionaryItemId,
        //            DictionaryItemName = a.DictionaryItemName,
        //            DictionaryItemCode = a.DictionaryItemCode,
        //            Description = a.Description

        //        }).ToList();
            

        //    return dictionaryLandmarkType;
        //}

        public IEnumerable<Models.DictionaryLandmarkType> GetLandmarkType()
        {
           
            return _dbContext.DictionaryLandmarkType.Select(LandmarkType => new Models.DictionaryLandmarkType
            {
                DictionaryItemId = LandmarkType.DictionaryItemId,
                DictionaryItemName = LandmarkType.DictionaryItemName,
                DictionaryItemCode = LandmarkType.DictionaryItemCode,
                Description = LandmarkType.Description
            });
        }


        public Models.DictionaryLandmarkType AddDictionaryLandmarkType(Models.DictionaryLandmarkType landmarkType)
        {
            if (!String.IsNullOrWhiteSpace(landmarkType.DictionaryItemName))
            {
                _dbContext.Add(landmarkType);
                _dbContext.SaveChanges();
            }
            return landmarkType;
        }


        public List<DictionaryCity> GetDictionaryCities()
        {
            List<DictionaryCity> dictionaryCities = _dbContext.DictionaryCity.Select(a => new DictionaryCity()
            {
                CityName = a.CityName,
                CityCode = a.CityCode
            }
            ).ToList();

            return dictionaryCities;
        }
    }
}
