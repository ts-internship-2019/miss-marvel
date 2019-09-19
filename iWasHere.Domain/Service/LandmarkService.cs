using iWasHere.Domain.DTOs;
using iWasHere.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iWasHere.Domain.Service
{
    public class LandmarkService
    {
        private readonly MissMarvelContext _dbContext;
        public LandmarkService(MissMarvelContext databaseContext)
        {
            _dbContext = databaseContext;
        }

        public IEnumerable<DictionaryLandmarkTypeModel> GetLandmarkType()
        {

                return _dbContext.DictionaryLandmarkType.Select(LandmarkType => new DictionaryLandmarkTypeModel
                {
                    DictionaryItemId = LandmarkType.DictionaryItemId,
                    DictionaryItemName = LandmarkType.DictionaryItemName,
                    DictionaryItemCode = LandmarkType.DictionaryItemCode,
                    Description = LandmarkType.Description
                });
            }
        }
    }

