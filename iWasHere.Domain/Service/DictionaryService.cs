﻿using iWasHere.Domain.DTOs;
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
            int toSkip = (skip-1) * take;

            List<DictionaryCityModel> dictionaryCities = _dbContext.DictionaryCity.Select(a => new DictionaryCityModel()
            {
                CityName = a.CityName,
                CityCode = a.CityCode,
                CityId = a.CityId
            }
            ).Skip(toSkip).Take(take).ToList();

            return dictionaryCities;
        }

        public List<DictionaryCountry> GetDictionaryCountries()
        {
            List<DictionaryCountry> dictionaryCountries = _dbContext.DictionaryCountry.Select(a => new DictionaryCountry()
            {
                CountryId = a.CountryId,
                CountryName = a.CountryName,
                CountryCode = a.CountryCode

            }).ToList();

            return dictionaryCountries;
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

        public List<DictionaryCounty> GetDictionaryCounty()
        {
            List<DictionaryCounty> dictionaryCounty = _dbContext.DictionaryCounty.Select(a => new DictionaryCounty()
            {
                CountyId = a.CountyId,
                CountyName = a.CountyName, 
                CountryId = a.CountryId,
                CountyCode = a.CountyCode
            }).ToList();

            return dictionaryCounty;
        }
       


       
    }
}

