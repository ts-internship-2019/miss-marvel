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

        #region Cata

        public IEnumerable<DictionaryCityModel> GetCities(int pageNo, int pageSize, out int rowsNo, string lFilter, int text)
        {
            rowsNo = 0;
            rowsNo = _dbContext.DictionaryCity.Count();
            if (lFilter == null && text < 1)
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

  

        public Models.DictionaryCity GetDictionaryCity(int cityId)
        {
            Models.DictionaryCity dictionaryCity = _dbContext.DictionaryCity.Find(cityId);
            return dictionaryCity;
        }
        public void DeleteCity(int id)
        {
            DictionaryCity language = new DictionaryCity() { CityId = id };

            _dbContext.DictionaryCity.Remove(language);
            _dbContext.SaveChanges();

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

        public Models.DictionaryCity AddDictionaryCity(string cityName, string cityCode, int countyId)
        {
            DictionaryCity dictionaryCity = new DictionaryCity();
            dictionaryCity.CityName = cityName;
            dictionaryCity.CityCode = cityCode;
            dictionaryCity.CountyId = countyId;


            if (!String.IsNullOrWhiteSpace(dictionaryCity.CityName))
            {
                _dbContext.DictionaryCity.Add(dictionaryCity);
                _dbContext.SaveChanges();
            }
            return dictionaryCity;
        }

        public Models.DictionaryCity EditDictionaryCity(Models.DictionaryCity dicionaryCity, out String errMsg)
        {
            errMsg = null;
            try
            {


                _dbContext.Update(dicionaryCity);
                _dbContext.SaveChanges();


                return dicionaryCity;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message.ToString();
                return null;
            }
        }

       public List<LandmarkPicture> GetLandmarkPictures(int landmarkId)
        {
            List<LandmarkPicture> landmarkPictures = _dbContext.LandmarkPicture.Where(a=> a.LandmarkId == landmarkId).Select(a => new LandmarkPicture()
            {
                PictureName = a.PictureName
            }).ToList();

            return landmarkPictures;
        }
    
        public List<LandmarkModel> CheckLandmark(int cityId)
        {
            List<LandmarkModel> landmarkModel = _dbContext.Landmark.Where(a=> a.CityId==cityId).Select(a => new LandmarkModel()
            {
                LandmarkName = a.LandmarkName
            }).ToList();

            return landmarkModel;
        }
        public List<LandmarkModel> GetLandmarkList()
        {
            List<LandmarkModel> landmarkModel = _dbContext.Landmark.Select(a => new LandmarkModel()
            {
                LandmarkId = a.LandmarkId,
                LandmarkName = a.LandmarkName,
                LandmarkDescription = a.LandmarkDescription,
                CityName = a.City.CityName,   
            }).ToList();

            return landmarkModel;
        }

        public List<LandmarkModel> GetLandmarkListFiltered(string landmarkName,string City)
        {
            List<LandmarkModel> landmarkModel = new List<LandmarkModel>();
            if (landmarkName != null && City != null)
            {
                 landmarkModel = _dbContext.Landmark.Where(a => a.LandmarkName.Contains(landmarkName) && a.City.CityName.Contains(City)).Select(a => new LandmarkModel()
                {
                    LandmarkId = a.LandmarkId,
                    LandmarkName = a.LandmarkName,
                    LandmarkDescription = a.LandmarkDescription,
                    CityName = a.City.CityName,
                    
                 }).ToList();
            }
            else if(landmarkName != null && City == null)
            {
                 landmarkModel = _dbContext.Landmark.Where(a => a.LandmarkName.Contains(landmarkName)).Select(a => new LandmarkModel()
                {
                    LandmarkId = a.LandmarkId,
                    LandmarkName = a.LandmarkName,
                    LandmarkDescription = a.LandmarkDescription,
                    CityName = a.City.CityName,
                   
                }).ToList();
            }
            else if(landmarkName == null && City != null)
            {
                 landmarkModel = _dbContext.Landmark.Where(a => a.City.CityName.Contains(City)).Select(a => new LandmarkModel()
                {
                    LandmarkId = a.LandmarkId,
                    LandmarkName = a.LandmarkName,
                    LandmarkDescription = a.LandmarkDescription,
                    CityName = a.City.CityName,
                     
                 }).ToList();
            }
            else
            {
                landmarkModel = _dbContext.Landmark.Select(a => new LandmarkModel()
                {
                    LandmarkId = a.LandmarkId,
                    LandmarkName = a.LandmarkName,
                    LandmarkDescription = a.LandmarkDescription,
                    CityName = a.City.CityName,
                    
                }).ToList();
            }
           
            for(int i = 0;i< landmarkModel.Count;i++)
            {

                List<ReviewsModel> reviews = _dbContext.LandmarkReview.Where(b=>b.LandmarkId == landmarkModel[i].LandmarkId).Select(a => new ReviewsModel()
                {
                    Rating = a.Rating
                }).ToList();
                int sum = 0;
                for(int x = 0; x<reviews.Count;x++ )
                {
                    sum = sum + reviews[x].Rating;
                }
                if(reviews.Count !=0)
                landmarkModel[i].Rating = Convert.ToInt32(sum / reviews.Count);
            }

            
            

            return landmarkModel;
        }


        #endregion

        #region Mihnea
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


        public IEnumerable<DictionaryLandmarkTypeModel> GetLandmarkType(int pageNo, int pageSize, out int rowsNo, string lFilter)
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

        //public List<DictionaryCurrencyTypeModel> GetCurrencyTypeCombo(String filter)
        //{

        //    return _dbContext.DictionaryCurrencyType.Where((a => a.CurrencyName.Contains(filter))).Take(10).Select(
        //        currency => new DictionaryCurrencyTypeModel
        //        {
        //            CurrencyTypeId = currency.CurrencyTypeId,
        //            CurrencyCode = currency.CurrencyCode,
        //            CurrencyName = currency.CurrencyName,
        //            CurrencyExRate = currency.CurrencyExRate
        //        }).ToList();

        //}

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


        public Models.Landmark AddEditLandmark(Models.Landmark landmark, List<Models.TicketXlandmark> priceList,
            out String errMsg, List<LandmarkPicture> pictures)
        {
            errMsg = null;
            try
            {
                Models.Landmark validation = _dbContext.Landmark.FirstOrDefault(a => a.LandmarkName.Equals(landmark.LandmarkName));
                if (validation != null)
                {
                    errMsg = "The name exists already";
                    return null;
                }
                validation = _dbContext.Landmark.FirstOrDefault(a => a.LandmarkCode.Equals(landmark.LandmarkCode));
                if (validation != null)
                {
                    errMsg = "The code exists already";
                    return null;
                }
                if (landmark.LandmarkId == 0)
                {
                  
                    _dbContext.Add(landmark);
                    _dbContext.SaveChanges();
                    Models.Landmark lastInserted = _dbContext.Landmark.Last();
                    foreach (Models.TicketXlandmark price in priceList)
                    {
                        price.LandmarkId = lastInserted.LandmarkId;
                        _dbContext.TicketXlandmark.Add(price);
                        _dbContext.SaveChanges();
                    }
                    foreach (LandmarkPicture picture in pictures)
                    {
                        picture.LandmarkId = lastInserted.LandmarkId;
                        _dbContext.LandmarkPicture.Add(picture);
                        _dbContext.SaveChanges();
                    }
                }
                else
                {

                    _dbContext.Update(landmark);
                    _dbContext.SaveChanges();
                    List<TicketXlandmark> oldPriceList = _dbContext.TicketXlandmark.Where((a => Convert.ToInt32(a.LandmarkId) == landmark.LandmarkId)).Select(
                 price => new TicketXlandmark
                 {
                     TicketXlandmarkId = price.TicketXlandmarkId,
                     LandmarkId = price.LandmarkId,
                     TicketTypeId = price.TicketTypeId,
                     CurrencyTypeId = price.CurrencyTypeId,
                     TicketValue = price.TicketValue

                 })
             .ToList();
                    for (int i = 0; i < priceList.Count; i++)
                    {
                        priceList[i].TicketXlandmarkId = oldPriceList[i].TicketXlandmarkId;
                        _dbContext.TicketXlandmark.Update(priceList[i]);
                        _dbContext.SaveChanges();
                    }
                }

                return landmark;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message.ToString();
                return null;
            }
        }



        public Models.Landmark GetLandmark(int landmarkId, out List<Models.TicketXlandmark> priceList, out DictionaryCurrencyType currency)
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
            currency = null;
            if (priceList.Count > 0)
            {
                int currencyId = Convert.ToInt32(priceList[0].CurrencyTypeId);
                currency = _dbContext.DictionaryCurrencyType.FirstOrDefault(a => a.CurrencyTypeId == currencyId);
            }
            Models.Landmark landmark = _dbContext.Landmark.Include(a => a.City)
                .Include(a =>a.LandmarkReview)
                .Include(a => a.LandmarkType)
                .Include(a => a.LandmarkPeriod)
                .FirstOrDefault(a => a.LandmarkId == landmarkId);
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
                    Models.DictionaryLandmarkType validation = _dbContext.DictionaryLandmarkType.FirstOrDefault(a => a.DictionaryItemName.Equals(landmarkType.DictionaryItemName));
                    if(validation != null)
                    {
                        errMsg = "The name exists already";
                        return null;
                    }
                    validation = _dbContext.DictionaryLandmarkType.FirstOrDefault(a => a.DictionaryItemCode.Equals(landmarkType.DictionaryItemCode));
                   if(validation != null)
                    {
                        errMsg = "The code exists already";
                        return null;
                    }
                    
                       
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
            catch (Exception ex)
            {
                errMsg = ex.Message.ToString();
                return null;
            }
        }

        public int DeleteLandmarkType(int id)
        {
            Models.DictionaryLandmarkType landmarkType = new Models.DictionaryLandmarkType() { DictionaryItemId = id };
            int deleted = 0;
            try
            {
                Models.Landmark validation = _dbContext.Landmark.FirstOrDefault(a => a.LandmarkTypeId == id);
                if (validation == null)
                {
                    _dbContext.DictionaryLandmarkType.Remove(landmarkType);
                    _dbContext.SaveChanges();
                    deleted = 1;
                    
                }
                return deleted;
            }catch(Exception ex)
            {
                return deleted;
            }

        }
        public Models.DictionaryLandmarkType GetDictionaryLandmarkType(int landmarkTypeId)
        {
            Models.DictionaryLandmarkType landmarkType = _dbContext.DictionaryLandmarkType.Find(landmarkTypeId);
            return landmarkType;
        }

        public Models.DictionaryLandmarkType AddDictionaryLandmarkType(Models.DictionaryLandmarkType cityModel)
        {
            if (!String.IsNullOrWhiteSpace(cityModel.DictionaryItemName))
            {
                _dbContext.Add(cityModel);
                _dbContext.SaveChanges();
            }
            return cityModel;
        }
        #endregion

        #region Corina


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

        public void DeleteCurrencyType(int id)
        {
            Models.DictionaryCurrencyType currency = new Models.DictionaryCurrencyType() { CurrencyTypeId = id };
            _dbContext.DictionaryCurrencyType.Remove(currency);
            _dbContext.SaveChanges();
        }

        public Models.DictionaryCurrencyType GetDictionaryCurrencyType(int currencyTypeId)
        {
            Models.DictionaryCurrencyType currencyType = _dbContext.DictionaryCurrencyType.Find(currencyTypeId);
            return currencyType;
        }

        public Models.DictionaryCurrencyType AddEditDictionaryCurrencyType(Models.DictionaryCurrencyType currencyType, out String errMsg)
        {
            errMsg = null;
            try
            {

                if (currencyType.CurrencyTypeId == 0)
                {
                    _dbContext.Add(currencyType);
                    _dbContext.SaveChanges();
                }
                else
                {
                    _dbContext.Update(currencyType);
                    _dbContext.SaveChanges();
                }

                return currencyType;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message.ToString();
                return null;
            }
        }


        public IEnumerable<DictionaryCurrencyType> LoadCurrency(int CurrencyId)
        {
            return _dbContext.DictionaryCurrencyType.Where(a => a.CurrencyTypeId == CurrencyId).Select(a => new DictionaryCurrencyType
            {
                CurrencyTypeId = a.CurrencyTypeId,
                CurrencyName = a.CurrencyName,
                CurrencyExRate = a.CurrencyExRate
            });



        }

        public DictionaryCurrencyTypeModel AddDictionaryCurrencyType(DictionaryCurrencyTypeModel id)
        {
            DictionaryCurrencyType currType = new DictionaryCurrencyType();
            currType.CurrencyName = id.CurrencyName;
            currType.CurrencyCode = id.CurrencyCode;
            currType.CurrencyExRate = id.CurrencyExRate;


            if (id.CurrencyTypeId != null)
                if (!String.IsNullOrWhiteSpace(id.CurrencyName))
                {
                    _dbContext.DictionaryCurrencyType.Add(currType);
                    _dbContext.SaveChanges();
                }
            return null;
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





        #endregion

        #region Dorin
        //LandmarkPeriods

        public List<LandmarkPeriodModel> GetLandmarkPeriod(String filter)
        {

            return _dbContext.DictionaryLandmarkPeriod.Where((a => a.LandmarkPeriodName.Contains(filter))).Take(10).Select(LandmarkPeriod => new LandmarkPeriodModel
            {
                LandmarkPeriodId = LandmarkPeriod.LandmarkPeriodId,
                LandmarkPeriodName = LandmarkPeriod.LandmarkPeriodName
            }).ToList();

        }
        public Models.DictionaryLandmarkPeriod GetDictionaryLandmarkPeriod(int id)
        {
            Models.DictionaryLandmarkPeriod landmarkPeriod = _dbContext.DictionaryLandmarkPeriod.Find(id);
            return landmarkPeriod;
        }
        public IEnumerable<LandmarkPeriodModel> GetDictionaryLandmarkPeriods(int pageNo, int pageSize, out int totalcount, string search)
        {
            int toskip;
            totalcount = 0;
            totalcount = _dbContext.DictionaryLandmarkPeriod.Count();
            toskip = (pageNo - 1) * pageSize;
            if (string.IsNullOrEmpty(search))
            {

                return _dbContext.DictionaryLandmarkPeriod.Skip((pageNo - 1) * pageSize).Take(pageSize).Select(landmarkPeriod => new LandmarkPeriodModel
                {
                    LandmarkPeriodId = landmarkPeriod.LandmarkPeriodId,
                    LandmarkPeriodName = landmarkPeriod.LandmarkPeriodName,
                });
            }
            else
            {
                return _dbContext.DictionaryLandmarkPeriod.Where(a => a.LandmarkPeriodName.Contains(search)).Skip((pageNo - 1) * pageSize).Take(pageSize).Select(landmarkPeriod => new LandmarkPeriodModel
                {
                    LandmarkPeriodId = landmarkPeriod.LandmarkPeriodId,
                    LandmarkPeriodName = landmarkPeriod.LandmarkPeriodName,
                });
            }
        }
        public DictionaryLandmarkPeriod AddEditLandmarkPeriods(DictionaryLandmarkPeriod landmarkPeriod)
        {
            try
            {
                if (!String.IsNullOrWhiteSpace(landmarkPeriod.LandmarkPeriodName))

                    if (landmarkPeriod.LandmarkPeriodId == 0 || landmarkPeriod.LandmarkPeriodId == null)

                    {
                        _dbContext.Add(landmarkPeriod);
                        _dbContext.SaveChanges();
                    }
                    else
                    {
                        _dbContext.Update(landmarkPeriod);
                        _dbContext.SaveChanges();
                    }

                return landmarkPeriod;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        public void DeletePeriod(int id)
        {
            try
            {
                var landmarkPeriod = _dbContext.DictionaryLandmarkPeriod.Find(id);
                _dbContext.Remove(landmarkPeriod);
                _dbContext.SaveChanges();
            }
            catch
            {

            }

        }


        #endregion

        #region Iuliana
        public void DeleteCountry(int id)
        {
            Models.DictionaryCountry country = new Models.DictionaryCountry() { CountryId = id };

            _dbContext.DictionaryCountry.Remove(country);
            _dbContext.SaveChanges();
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

        public DictionaryCountryModel AddDictionaryCountry(DictionaryCountryModel id)
        {
            DictionaryCountry dictionaryCountry = new DictionaryCountry();
            dictionaryCountry.CountryName = id.CountryName;
            dictionaryCountry.CountryCode = id.CountryCode;

            if (id.CountryId != null)
                if (!String.IsNullOrWhiteSpace(id.CountryName))
                {
                    _dbContext.DictionaryCountry.Add(dictionaryCountry);
                    _dbContext.SaveChanges();
                }
            return null;
        }

        //-------------------------------------------------------------------------------


        //public Models.DictionaryCountry AddDictionaryCountry(Models.DictionaryCountry dc)
        //{
        //    if (!String.IsNullOrWhiteSpace(dc.CountryName))
        //    {
        //        _dbContext.Add(dc);
        //        _dbContext.SaveChanges();
        //    }
        //    return dc;
        //}


        public IEnumerable<DictionaryCountryModel> GetCountry(int pageNo, int pageSize, out int rowsNo, string lFilter)
        {
            rowsNo = 0;
            rowsNo = _dbContext.DictionaryCountry.Count();
            if (lFilter == null)
                return _dbContext.DictionaryCountry.Skip((pageNo - 1) * pageSize).Take(pageSize).Select(a => new DictionaryCountryModel
                {
                    CountryId = a.CountryId,
                    CountryName = a.CountryName,
                    CountryCode = a.CountryCode
                });
            else
                return _dbContext.DictionaryCountry.Where(a => a.CountryName.Contains(lFilter)).Skip((pageNo - 1) * pageSize).Take(pageSize).Select(a => new DictionaryCountryModel
                {
                    CountryId = a.CountryId,
                    CountryName = a.CountryName,
                    CountryCode = a.CountryCode
                });



        }

        public Models.DictionaryCountry AddEditDictionaryCountry(Models.DictionaryCountry country, out String errMsg)
        {
            errMsg = null;
            try
            {

                if (country.CountryId == 0)
                {
                    _dbContext.DictionaryCountry.Add(country);
                    _dbContext.SaveChanges();
                }
                else
                {
                    _dbContext.DictionaryCountry.Update(country);
                    _dbContext.SaveChanges();
                }

                return country;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message.ToString();
                return null;
            }
        }

        public IEnumerable<DictionaryCountryModel> LoadCountry(int CountryId)
        {
            return _dbContext.DictionaryCountry.Where(a => a.CountryId == CountryId).Select(a => new DictionaryCountryModel
            {
                CountryId = a.CountryId,
                CountryName = a.CountryName,
                CountryCode = a.CountryCode
            });


        }

        public List<DictionaryCurrencyTypeModel> GetCurrencyTypeCombo(String filter)
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
            public Models.DictionaryCountry GetDictionaryCountry(int countryId)
            {
                Models.DictionaryCountry country = _dbContext.DictionaryCountry.Find(countryId);
                return country;
            }









            #endregion

        #region Victor

            public IEnumerable<DictionaryCountyModel> GetCounty(int pageNo, int pageSize, out int rowsNo, string lFilter, int text)
            {
                rowsNo = 0;
                rowsNo = _dbContext.DictionaryCounty.Count();
                if (lFilter == null && text < 1)
                    return _dbContext.DictionaryCounty.Skip((pageNo - 1) * pageSize).Take(pageSize).Select(County => new DictionaryCountyModel
                    {
                        CountyId = County.CountyId,
                        CountyName = County.CountyName,
                        CountryId = Convert.ToInt32(County.CountryId),
                        CountyCode = County.CountyCode
                    });
                else if (lFilter != null && text < 1)
                {
                    rowsNo = _dbContext.DictionaryCounty.Where(a => a.CountyName.Contains(lFilter)).Count();
                    return _dbContext.DictionaryCounty.Where(a => a.CountyName.Contains(lFilter)).Skip((pageNo - 1) * pageSize).Take(pageSize).Select(County => new DictionaryCountyModel
                    {
                        CountyId = County.CountyId,
                        CountyName = County.CountyName,
                        CountryId = Convert.ToInt32(County.CountryId),
                        CountyCode = County.CountyCode
                    });
                }
                else if (lFilter == null && text > 0)
                {
                    rowsNo = _dbContext.DictionaryCounty.Where(a => a.CountryId.Equals(text)).Count();
                    return _dbContext.DictionaryCounty.Where(a => a.CountryId.Equals(text)).Skip((pageNo - 1) * pageSize).Take(pageSize).Select(County => new DictionaryCountyModel
                    {
                        CountyId = County.CountyId,
                        CountyName = County.CountyName,
                        CountryId = Convert.ToInt32(County.CountryId),
                        CountyCode = County.CountyCode
                    });
                }
                else
                {
                    rowsNo = _dbContext.DictionaryCounty.Where(a => a.CountyName.Contains(lFilter) && a.CountyId.Equals(text)).Count();
                    return _dbContext.DictionaryCounty.Where(a => a.CountyName.Contains(lFilter) && a.CountyId.Equals(text)).Skip((pageNo - 1) * pageSize).Take(pageSize).Select(County => new DictionaryCountyModel
                    {
                        CountyId = County.CountyId,
                        CountyName = County.CountyName,
                        CountryId = Convert.ToInt32(County.CountryId),
                        CountyCode = County.CountyCode
                    });
                }
            }

            public IEnumerable<DictionaryCountyModel> LoadCounty(int CountyId)
            {
                return _dbContext.DictionaryCounty.Where(a => a.CountyId == CountyId).Select(County => new DictionaryCountyModel
                {
                    CountyId = County.CountyId,
                    CountyName = County.CountyName,
                    CountyCode = County.CountyCode,
                    CountryId = Convert.ToInt32(County.CountryId)
                });
            }

            public IEnumerable<DictionaryCountyModel> GetCounty(out int totalRows)
            {
                totalRows = _dbContext.DictionaryCounty.Count();
                return _dbContext.DictionaryCounty.Select(County => new DictionaryCountyModel
                {
                    CountyId = County.CountyId,
                    CountyName = County.CountyName,
                    CountyCode = County.CountyCode,
                    CountryId = Convert.ToInt32(County.CountryId)
                });
            }

            public Models.DictionaryCounty AddEditDictionaryCounty(Models.DictionaryCounty county, out String errMsg)
            {
                errMsg = null;
                try
                {

                    if (county.CountyId == 0 /*|| county.CountyId == null*/)
                    {
                        _dbContext.Add(county);
                        _dbContext.SaveChanges();
                    }
                    else
                    {
                        _dbContext.Update(county);
                        _dbContext.SaveChanges();
                    }

                    return county;
                }
                catch (Exception ex)
                {
                    errMsg = ex.Message.ToString();
                    return null;
                }
            }

            public void DeleteCounty(int id)
            {
                Models.DictionaryCounty county = new Models.DictionaryCounty() { CountyId = id };

                _dbContext.DictionaryCounty.Remove(county);
                _dbContext.SaveChanges();

            }


            public List<DictionaryCountry> GetComboCountry(string text)
            {
                List<DictionaryCountry> comboCountry = _dbContext.DictionaryCountry.Select(a => new DictionaryCountry()
                {
                    CountryId = a.CountryId,
                    CountryName = a.CountryName

                }).Where(a => a.CountryName.Contains(text)).Take(10).ToList();
                return comboCountry;
            }

            public Models.DictionaryCounty GetDictionaryCounty(int countyId)
            {
                Models.DictionaryCounty county = _dbContext.DictionaryCounty.Find(countyId);
                return county;
            }

            public Models.DictionaryCounty AddDictionaryCounty(Models.DictionaryCounty countyModel)
            {
                if (!String.IsNullOrWhiteSpace(countyModel.CountyName))
                {
                    _dbContext.Add(countyModel);
                    _dbContext.SaveChanges();
                }
                return countyModel;
            }


            public DictionaryCountyModel AddDictionaryCounty(DictionaryCountyModel county)
            {
                if (!String.IsNullOrWhiteSpace(county.CountyName))
                {
                    _dbContext.Add(county);
                    _dbContext.SaveChanges();
                }
                return county;
            }

            #endregion

        #region Gabi
            public IEnumerable<DictionaryTicketTypeModel> GetDictionaryTicketType(int pgNo, int pgSize, out int countRows, string FilterTicketType)
            {
                countRows = _dbContext.DictionaryTicketType.Count();
                int toSkip = (pgNo - 1) * pgSize;
                if (FilterTicketType == null)
                    return _dbContext.DictionaryTicketType.Skip(toSkip).Take(pgSize).Select(x => new DictionaryTicketTypeModel
                    {
                        TicketTypeId = x.TicketTypeId,
                        TicketTypeName = x.TicketTypeName

                    });
                else
                    return _dbContext.DictionaryTicketType.Where(x => x.TicketTypeName.Contains(FilterTicketType)).Skip(toSkip).Take(pgSize).Select(x => new DictionaryTicketTypeModel
                    {
                        TicketTypeId = x.TicketTypeId,
                        TicketTypeName = x.TicketTypeName
                    });
            }

            public DictionaryTicketType GetTicketTypeId(int ticketTypeId)
            {
                DictionaryTicketType ticketType = _dbContext.DictionaryTicketType.Where(x => x.TicketTypeId == ticketTypeId)
                    .Select(x => new DictionaryTicketType()
                    {
                        TicketTypeId = x.TicketTypeId,
                        TicketTypeName = x.TicketTypeName
                    }).First();

                return ticketType;
            }



            public void AddTicket(DictionaryTicketType ticketType)
            {
                _dbContext.DictionaryTicketType.Add(new DictionaryTicketType
                {
                    TicketTypeName = ticketType.TicketTypeName
                });
                _dbContext.SaveChanges();
            }

            public void EditTicket(DictionaryTicketType ticketType)
            {
                _dbContext.DictionaryTicketType.Update(new DictionaryTicketType
                {
                    TicketTypeId = ticketType.TicketTypeId,
                    TicketTypeName = ticketType.TicketTypeName
                });

                _dbContext.SaveChanges();

            }

            public void DeleteTicket(int ticketTypeId)
            {
                DictionaryTicketType ticketType = new DictionaryTicketType() { TicketTypeId = ticketTypeId };

                _dbContext.DictionaryTicketType.Remove(ticketType);
                _dbContext.SaveChanges();
            }

        //public string AddReview(LandmarkReview review)
        //{
        //    try
        //    {
        //        _dbContext.LandmarkReview.Add(new LandmarkReview
        //        {
        //            ReviewTitle = review.ReviewTitle,
        //            ReviewComment = review.ReviewComment,
        //            LandmarkId = review.LandmarkId,
        //            UserId = review.UserId,
        //            Rating = review.Rating
        //        });
        //        _dbContext.SaveChanges();
        //        return null;
        //    }
        //    catch (Exception ex)
        //    {
        //        return "Please fill the required fields";
        //    }
        //}


        //public List<LandmarkReview> GetDbCommentsAll(int landmarkId)
        //{
        //    List<LandmarkReview> landmarkReview = _dbContext.LandmarkReview.Select(x => new LandmarkReview()
        //    {
        //        UserId = x.UserId,
        //        ReviewTitle = x.ReviewTitle,
        //        ReviewComment = x.ReviewComment,
        //        Rating = x.Rating

        //    }).Where(x => x.LandmarkId == landmarkId).ToList();

        //    return landmarkReview;
        //}

        #endregion

        #region Gunoi
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
        //paginare
        //  public List<DictionaryCurrencyTypeModel> GetDictionaryCurrencyType(int pageNo, int pageSize, out int totalCount)
        #endregion

    }
    }


