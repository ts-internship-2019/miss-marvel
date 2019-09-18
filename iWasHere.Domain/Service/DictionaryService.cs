using iWasHere.Domain.DTOs;
using iWasHere.Domain.Model;
using iWasHere.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        public List<Models.DictionaryLandmarkType> GetDictionaryLandmarkType(String searchText)
        {

            List<Models.DictionaryLandmarkType> dictionaryLandmarkType;
            if (!String.IsNullOrWhiteSpace(searchText))
            {
                dictionaryLandmarkType = _dbContext.DictionaryLandmarkType.Where(a => a.DictionaryItemName.StartsWith(searchText)).Select(a => new Models.DictionaryLandmarkType()
                {
                    DictionaryItemId = a.DictionaryItemId,
                    DictionaryItemName = a.DictionaryItemName,
                    DictionaryItemCode = a.DictionaryItemCode,
                    Description = a.Description

                }).ToList();
            }
            else
            {
                dictionaryLandmarkType = _dbContext.DictionaryLandmarkType.Select(a => new Models.DictionaryLandmarkType()
                {
                    DictionaryItemId = a.DictionaryItemId,
                    DictionaryItemName = a.DictionaryItemName,
                    DictionaryItemCode = a.DictionaryItemCode,
                    Description = a.Description

                }).ToList();
            }

            return dictionaryLandmarkType;
        }


        public List<DictionaryCityModel> GetDictionaryCities(int skip, int take, out int totalCount)
        {
            totalCount = _dbContext.DictionaryCity.Count();
            int toSkip = (skip - 1) * take;

            List<DictionaryCityModel> dictionaryCities = _dbContext.DictionaryCity.Select(a => new DictionaryCityModel()
            {
                CityName = a.CityName,
                CityCode = a.CityCode,
                CityId = a.CityId
            }
            ).Skip(toSkip).Take(take).ToList();

            return dictionaryCities;
        }

        public List<DictionaryCountryModel> GetDictionaryCountries(int pageNo, int pageSize, out int totalCount)

        {
            totalCount = _dbContext.DictionaryCountry.Count();
            int skip = (pageNo - 1) * pageSize;
            List<DictionaryCountryModel> dictionaryCountries = _dbContext.DictionaryCountry.Select(a => new DictionaryCountryModel()
            {
                CountryId = a.CountryId,
                CountryName = a.CountryName,
                CountryCode = a.CountryCode

            }).Skip(skip).Take(pageSize).ToList();

            return dictionaryCountries;
        }
        //paginare
        public List<DictionaryCurrencyTypeModel> GetDictionaryCurrencyType(int pageNo, int pageSize, out int totalCount)

        {
            totalCount = _dbContext.DictionaryCurrencyType.Count();
            int skip = (pageNo - 1) * pageSize;

            List<DictionaryCurrencyTypeModel> dictionaryCurrencyType = _dbContext.DictionaryCurrencyType.Select(a => new DictionaryCurrencyTypeModel()
            {
                CurrencyTypeId = a.CurrencyTypeId,
                CurrencyName = a.CurrencyName,
                CurrencyCode = a.CurrencyCode,
                CurrencyExRate = a.CurrencyExRate

            }).Skip(skip).Take(pageSize).ToList();

            return dictionaryCurrencyType;
        }

        public List<DictionaryCountyModel> GetDictionaryCounty(int skip, int take, out int totalCount)
        {
            totalCount = _dbContext.DictionaryCounty.Count();
            int toSkip = (skip - 1) * take;

            List<DictionaryCountyModel> dictionaryCounty = _dbContext.DictionaryCounty.Select(a => new DictionaryCountyModel()
            {
                CountyId = a.CountyId,
                CountyName = a.CountyName,
                //CountryId = a.CountryId, //doar daca ne trebuie legatura
                CountyCode = a.CountyCode
            }).Skip(toSkip).Take(take).ToList();

            return dictionaryCounty;
        }

    

    public IEnumerable<DictionaryCurrencyTypeModel> GetDictionaryCurrencyTypes(int pageNo, int pageSize, out int rowsNo, string lFilter)
    {
        rowsNo = 0;
        rowsNo = _dbContext.DictionaryCurrencyType.Count();
        if (lFilter == null)
            return _dbContext.DictionaryCurrencyType.Skip((pageNo - 1) * pageSize).Take(pageSize).Select(CurrencyType => new DictionaryCurrencyTypeModel
            {
                CurrencyTypeId = CurrencyType.CurrencyTypeId,
                CurrencyName = CurrencyType.CurrencyName,
                CurrencyCode = CurrencyType.CurrencyCode,
                CurrencyExRate = CurrencyType.CurrencyExRate
            }); 
        else
            return _dbContext.DictionaryCurrencyType.Where(a => a.CurrencyName.Contains(lFilter)).Skip((pageNo - 1) * pageSize).Take(pageSize).Select(CurrencyType => new DictionaryCurrencyTypeModel
            {
                CurrencyTypeId = CurrencyType.CurrencyTypeId,
                CurrencyName = CurrencyType.CurrencyName,
                CurrencyCode = CurrencyType.CurrencyCode,
                CurrencyExRate = CurrencyType.CurrencyExRate
                
            });


    }

    }
}

