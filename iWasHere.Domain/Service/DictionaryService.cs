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
            List<DictionaryLandmarkTypeModel> dictionaryLandmarkTypeModels = _dbContext.DictionaryLandmarkType.Select(LandmarkType => new DictionaryLandmarkTypeModel()
            {
                DictionaryItemId = LandmarkType.DictionaryItemId,
                DictionaryItemName = LandmarkType.DictionaryItemName,
                DictionaryItemCode = LandmarkType.DictionaryItemCode,
                Description = LandmarkType.Description
            }).ToList();

            return dictionaryLandmarkTypeModels;
        }


        public IEnumerable<DictionaryLandmarkTypeModel> GetLandmarkType(int pageNo, int pageSize, out int rowsNo, string lFilter )
        {
            rowsNo = 0;
            rowsNo = _dbContext.DictionaryLandmarkType.Count();
            if (lFilter == null)
                return _dbContext.DictionaryLandmarkType.Skip((pageNo - 1) * pageSize).Take(pageSize).Select(LandmarkType => new DictionaryLandmarkTypeModel
                {
                    DictionaryItemId = LandmarkType.DictionaryItemId,
                    DictionaryItemName = LandmarkType.DictionaryItemName,
                    DictionaryItemCode = LandmarkType.DictionaryItemCode,
                    Description = LandmarkType.Description
                });
            else
            {
                rowsNo = _dbContext.DictionaryLandmarkType.Where(a => a.DictionaryItemName.Contains(lFilter)).Count();
                return _dbContext.DictionaryLandmarkType.Where(a => a.DictionaryItemName.Contains(lFilter)).Skip((pageNo - 1) * pageSize).Take(pageSize).Select(LandmarkType => new DictionaryLandmarkTypeModel
                {
                    DictionaryItemId = LandmarkType.DictionaryItemId,
                    DictionaryItemName = LandmarkType.DictionaryItemName,
                    DictionaryItemCode = LandmarkType.DictionaryItemCode,
                    Description = LandmarkType.Description
                });
            }
        }

        public IEnumerable<DictionaryLandmarkTypeModel> LoadLandmarkType(int LandmarkTypeId)
        {
            return _dbContext.DictionaryLandmarkType.Where(a => a.DictionaryItemId == LandmarkTypeId).Select(LandmarkType => new DictionaryLandmarkTypeModel
            {
                DictionaryItemId = LandmarkType.DictionaryItemId,
                DictionaryItemName = LandmarkType.DictionaryItemName,
                DictionaryItemCode = LandmarkType.DictionaryItemCode,
                Description = LandmarkType.Description
            });

        }

        public DictionaryLandmarkTypeModel AddDictionaryLandmarkType(DictionaryLandmarkTypeModel landmarkType)
        {
            if(landmarkType.DictionaryItemId != null)
            if (!String.IsNullOrWhiteSpace(landmarkType.DictionaryItemName))
            {
                _dbContext.Add(landmarkType);
                _dbContext.SaveChanges();
            }
            return null;
        }


        //public List<DictionaryCityModel> GetDictionaryCities(int skip, int take, out int totalCount)
        //{
        //    totalCount = _dbContext.DictionaryCity.Count();
        //    int toSkip = (skip-1) * take;

        //    List<DictionaryCityModel> dictionaryCities = _dbContext.DictionaryCity.Select(a => new DictionaryCityModel()
        //    {
        //        CityName = a.CityName,
        //        CityCode = a.CityCode,
        //        CityId = a.CityId
        //    }
        //    ).Skip(toSkip).Take(take).ToList();

        //    return dictionaryCities;
        //}

        public IEnumerable<DictionaryCityModel> GetCities(int pageNo, int pageSize, out int rowsNo, string lFilter)
        {
            rowsNo = 0;
            rowsNo = _dbContext.DictionaryCity.Count();
            if (lFilter == null)
                return _dbContext.DictionaryCity.Skip((pageNo - 1) * pageSize).Take(pageSize).Select(Cities => new DictionaryCityModel
                {
                    CityId = Cities.CityId,
                    CityName = Cities.CityName,
                    CityCode = Cities.CityCode

                }) ;
            else
                return _dbContext.DictionaryCity.Where(a => a.CityName.Contains(lFilter)).Skip((pageNo - 1) * pageSize).Take(pageSize).Select(Cities => new DictionaryCityModel
                {
                    CityId = Cities.CityId,
                    CityName = Cities.CityName,
                    CityCode = Cities.CityCode
                });


        }



        public DictionaryCityModel AddDictionaryLandmarkType(DictionaryCityModel cityModel)
        {
            if (!String.IsNullOrWhiteSpace(cityModel.CityName))
            {
                _dbContext.Add(cityModel);
                _dbContext.SaveChanges();
            }
            return cityModel;
        }

        public List<DictionaryCountryModel> GetDictionaryCountries(int pageNo, int pageSize, out int totalCount)

        {
            totalCount = _dbContext.DictionaryCountry.Count();
            int skip = (pageNo-1) * pageSize;
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

        public List<LandmarkPeriodModel> GetDictionaryLandmarkPeriods(int pageNo, int pageSize, out int totalcount)
        {
            int toskip;
            totalcount = _dbContext.DictionaryLandmarkPeriod.Count();
            toskip = (pageNo - 1) * pageSize;

            List<LandmarkPeriodModel> dictionaryPeriods = _dbContext.DictionaryLandmarkPeriod.Select(a => new LandmarkPeriodModel()
            {
                LandmarkPeriodId = a.LandmarkPeriodId,
                LandmarkPeriodName = a.LandmarkPeriodName
            }
            ).Skip(toskip).Take(pageSize).ToList();

            return dictionaryPeriods;
        }

        public List<TicketTypeModel> GetDictionaryTicketType(int pgNo, int pgSize, out int totalCount)
        {
           
            totalCount = _dbContext.DictionaryTicketType.Count();
            int toSkip = (pgNo - 1) * pgSize;

            List<TicketTypeModel> dictionaryTicketTypes = _dbContext.DictionaryTicketType.Select(x => new TicketTypeModel()
            {
                TicketTypeName = x.TicketTypeName
            }).Skip(toSkip).Take(pgSize).ToList();

            return dictionaryTicketTypes;

        }



    }
}

