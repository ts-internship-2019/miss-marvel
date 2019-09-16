﻿using iWasHere.Domain.DTOs;
using iWasHere.Domain.Model;
using iWasHere.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
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

    }
}
