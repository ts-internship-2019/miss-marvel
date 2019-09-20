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
        // trebuie mutat in Landmark service dupa ce invat dependency inversion
        public List<DictionaryLandmarkTypeModel> GetLandmarkType(String filter)
        {
            
           
                return _dbContext.DictionaryLandmarkType.Where((a => a.DictionaryItemName.Contains(filter))).Take(10).Select(LandmarkType => new DictionaryLandmarkTypeModel
                {
                    DictionaryItemId = LandmarkType.DictionaryItemId,
                    DictionaryItemName = LandmarkType.DictionaryItemName,
                    DictionaryItemCode = LandmarkType.DictionaryItemCode,
                    Description = LandmarkType.Description
                }).ToList();
           
        }

        public List<LandmarkPeriodModel> GetLandmarkPeriod(String filter)
        {

            return _dbContext.DictionaryLandmarkPeriod.Where((a => a.LandmarkPeriodName.Contains(filter))).Take(10).Select(LandmarkPeriod => new LandmarkPeriodModel
            {
                LandmarkPeriodId = LandmarkPeriod.LandmarkPeriodId,
                LandmarkPeriodName = LandmarkPeriod.LandmarkPeriodName
            }).ToList();

        }

        public List<DictionaryCityModel> GetCities(String filter)
        {

            return _dbContext.DictionaryCity.Where((a => a.CityName.Contains(filter))).Take(10).Select(
                city => new DictionaryCityModel
                {
                    CityId = city.CityId,
                    CityName = city.CityName,
                    CountyId = Convert.ToInt32(city.CountyId)
                }).ToList();

        }

        public List<DictionaryCurrencyTypeModel> GetCurrencyType(String filter)
        {

            return _dbContext.DictionaryCurrencyType.Where((a => a.CurrencyName.Contains(filter))).Take(10).Select(
                currency => new DictionaryCurrencyTypeModel
                {
                    CurrencyTypeId = currency.CurrencyTypeId,
                    CurrencyCode = currency.CurrencyCode,
                    CurrencyName = currency.CurrencyName,
                    CurrencyExRate = currency.CurrencyExRate
                }).ToList();

        }

        public Models.Landmark AddEditLandmark(Models.Landmark landmark, List<Models.TicketXlandmark> priceList, out String errMsg)
        {
            errMsg = null;
            try
            {

                if (landmark.LandmarkId == 0)
                {
                    _dbContext.Add(landmark);
                    _dbContext.SaveChanges();
                    Models.Landmark lastInserted = _dbContext.Landmark.Last();
                    foreach(Models.TicketXlandmark price in priceList)
                    {
                        price.LandmarkId = lastInserted.LandmarkId;
                        _dbContext.TicketXlandmark.Add(price);
                        _dbContext.SaveChanges();
                    }
                }
                else
                {
                    _dbContext.Update(landmark);
                    _dbContext.SaveChanges();
                }

                return landmark;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message.ToString();
                return null;
            }
        }

        public Models.Landmark GetLandmark(int landmarkId, out List<Models.TicketXlandmark> priceList)
        {
            priceList = _dbContext.TicketXlandmark.Where((a => Convert.ToInt32(a.LandmarkId) == landmarkId)).Select(
                price => new TicketXlandmark
            {
                TicketXlandmarkId = price.TicketXlandmarkId,
                LandmarkId = price.LandmarkId,
                TicketTypeId = price.TicketTypeId,
                CurrencyTypeId = price.CurrencyTypeId,
                TicketValue = price.TicketValue
            })
            .ToList();
               
                
            Models.Landmark landmark = _dbContext.Landmark.Find(landmarkId);
            return landmark;
        }
        //pana aici

        public Models.DictionaryLandmarkType AddEditDictionaryLandmarkType(Models.DictionaryLandmarkType landmarkType, out String errMsg)
        {
            errMsg = null;
            try
            {

                if (landmarkType.DictionaryItemId == 0)
                {
                    _dbContext.Add(landmarkType);
                    _dbContext.SaveChanges();
                }
                else
                {
                    _dbContext.Update(landmarkType);
                    _dbContext.SaveChanges();
                }

                return landmarkType;
            }
            catch(Exception ex)
            {
                errMsg = ex.Message.ToString();
                return null;
            }
        }

        public void DeleteLandmarkType(int id)
        {
          Models.DictionaryLandmarkType landmarkType = new Models.DictionaryLandmarkType() { DictionaryItemId = id };

            _dbContext.DictionaryLandmarkType.Remove(landmarkType);
            _dbContext.SaveChanges();


        }
        public Models.DictionaryLandmarkType GetDictionaryLandmarkType(int landmarkTypeId)
        {
            Models.DictionaryLandmarkType landmarkType = _dbContext.DictionaryLandmarkType.Find(landmarkTypeId);
            return landmarkType;
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

        public IEnumerable<DictionaryCityModel> GetCities(int pageNo, int pageSize, out int rowsNo, string lFilter,int text)
        {
            rowsNo = 0;
            rowsNo = _dbContext.DictionaryCity.Count();
            if (lFilter == null && text <1)
                return _dbContext.DictionaryCity.Skip((pageNo - 1) * pageSize).Take(pageSize).Select(Cities => new DictionaryCityModel
                {
                    CityId = Cities.CityId,
                    CityName = Cities.CityName,
                    CityCode = Cities.CityCode,
                    CountyId = Convert.ToInt32(Cities.CountyId)

                });
            else if (lFilter != null && text < 1)
            {
                rowsNo = _dbContext.DictionaryCity.Where(a => a.CityName.Contains(lFilter)).Count();
                return _dbContext.DictionaryCity.Where(a => a.CityName.Contains(lFilter)).Skip((pageNo - 1) * pageSize).Take(pageSize).Select(Cities => new DictionaryCityModel
                {
                    CityId = Cities.CityId,
                    CityName = Cities.CityName,
                    CityCode = Cities.CityCode,
                    CountyId = Convert.ToInt32(Cities.CountyId)
                });
            }
            else if (lFilter == null && text > 0)
            {
                rowsNo = _dbContext.DictionaryCity.Where(a => a.CountyId.Equals(text)).Count();
                return _dbContext.DictionaryCity.Where(a => a.CountyId.Equals(text)).Skip((pageNo - 1) * pageSize).Take(pageSize).Select(Cities => new DictionaryCityModel
                {
                    CityId = Cities.CityId,
                    CityName = Cities.CityName,
                    CityCode = Cities.CityCode,
                    CountyId = Convert.ToInt32(Cities.CountyId)
                });
            }
            else
            {
                rowsNo = _dbContext.DictionaryCity.Where(a => a.CityName.Contains(lFilter) && a.CountyId.Equals(text)).Count();
                return _dbContext.DictionaryCity.Where(a => a.CityName.Contains(lFilter) && a.CountyId.Equals(text)).Skip((pageNo - 1) * pageSize).Take(pageSize).Select(Cities => new DictionaryCityModel
                {
                    CityId = Cities.CityId,
                    CityName = Cities.CityName,
                    CityCode = Cities.CityCode,
                    CountyId = Convert.ToInt32(Cities.CountyId)
                });
            }

        }

        public List<DictionaryCounty> GetComboCounty(string text)
        {
            List<DictionaryCounty> comboCounty = _dbContext.DictionaryCounty.Select(a => new DictionaryCounty()
            {
                CountyId = a.CountyId,
                CountyName = a.CountyName,

            }).Where(a => a.CountyName.Contains(text)).Take(10).ToList();
                return comboCounty;
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


        public IEnumerable<DictionaryTicketTypeModel> GetDictionaryTicketType(int pgNo, int pgSize, out int countRows, string FilterTicketType)
        {
            countRows = _dbContext.DictionaryTicketType.Count();
            int toSkip = (pgNo - 1) * pgSize;
            if (FilterTicketType == null)
                return _dbContext.DictionaryTicketType.Skip(toSkip).Take(pgSize).Select(a => new DictionaryTicketTypeModel
                {
                    TicketTypeName = a.TicketTypeName

                });
            else
                return _dbContext.DictionaryTicketType.Where(a => a.TicketTypeName.Contains(FilterTicketType)).Skip(toSkip).Take(pgSize).Select(a => new DictionaryTicketTypeModel
                {
                    TicketTypeName = a.TicketTypeName
                });
        }

 





    }
}

