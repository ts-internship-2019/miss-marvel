using iWasHere.Domain.DTOs;
using iWasHere.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iWasHere.Domain.Models;
using System.Configuration; 


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

        public List<DictionaryCurrencyType> GetDictionaryCurrencyType()

        {
            List<DictionaryCurrencyType> dictionaryCurrencyType = _dbContext.DictionaryCurrencyType.Select(a => new DictionaryCurrencyType()
            {
                CurrencyTypeId = a.CurrencyTypeId,
                CurrencyName = a.CurrencyName,
                CurrencyCode = a.CurrencyCode,
                CurrencyExRate = a.CurrencyExRate

            }).ToList();

            return dictionaryCurrencyType;
        }
    }
}

