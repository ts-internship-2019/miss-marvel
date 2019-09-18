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
            List<DictionaryLandmarkTypeModel> dictionaryLandmarkTypeModels = _dbContext.DictionaryLandmarkType.Select(LandmarkType => new DictionaryLandmarkTypeModel()
            {
                DictionaryItemId = LandmarkType.DictionaryItemId,
                DictionaryItemName = LandmarkType.DictionaryItemName,
                DictionaryItemCode = LandmarkType.DictionaryItemCode,
                Description = LandmarkType.Description
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

        public IEnumerable<DictionaryLandmarkTypeModel> GetLandmarkType(int pageNo, int pageSize, out int rowsNo, string lFilter )
        {
            rowsNo = 0;
            rowsNo = _dbContext.DictionaryLandmarkType.Count();
            if(lFilter == null)
                return _dbContext.DictionaryLandmarkType.Skip((pageNo - 1) * pageSize ).Take(pageSize).Select(LandmarkType => new DictionaryLandmarkTypeModel
                {
                    DictionaryItemId = LandmarkType.DictionaryItemId,
                    DictionaryItemName = LandmarkType.DictionaryItemName,
                    DictionaryItemCode = LandmarkType.DictionaryItemCode,
                    Description = LandmarkType.Description
                });
            else
                return _dbContext.DictionaryLandmarkType.Where(a => a.DictionaryItemName.Contains(lFilter)).Skip((pageNo - 1) * pageSize).Take(pageSize).Select(LandmarkType => new DictionaryLandmarkTypeModel
                {
                    DictionaryItemId = LandmarkType.DictionaryItemId,
                    DictionaryItemName = LandmarkType.DictionaryItemName,
                    DictionaryItemCode = LandmarkType.DictionaryItemCode,
                    Description = LandmarkType.Description
                });

        }


        public DictionaryLandmarkTypeModel AddDictionaryLandmarkType(DictionaryLandmarkTypeModel landmarkType)
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
