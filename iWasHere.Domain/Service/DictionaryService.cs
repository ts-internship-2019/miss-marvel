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
       
        public Models.DictionaryCity AddDictionaryCity(string cityName,string cityCode,int countyId)
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

        public IEnumerable<DictionaryLandmarkTypeModel> GetLandmarkType(out int totalRows)
        {
            totalRows = _dbContext.DictionaryLandmarkType.Count();
            return _dbContext.DictionaryLandmarkType.Select(LandmarkType => new DictionaryLandmarkTypeModel
            {
                DictionaryItemId = LandmarkType.DictionaryItemId,
                DictionaryItemName = LandmarkType.DictionaryItemName,
                DictionaryItemCode = LandmarkType.DictionaryItemCode,
                Description = LandmarkType.Description
            });
        }


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
            catch (Exception ex)
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

        public DictionaryCurrencyType AddEditDictionaryCurrencyType(DictionaryCurrencyType currencyType)
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
            if (!String.IsNullOrWhiteSpace(landmarkPeriod.LandmarkPeriodName))
            {
                _dbContext.Add(landmarkPeriod);
                _dbContext.SaveChanges();
            }
            return landmarkPeriod;
        }
        public void DeletePeriod(int id )
        {
            var landmarkPeriod = _dbContext.DictionaryLandmarkPeriod.Find(id);
            _dbContext.Remove(landmarkPeriod);
            _dbContext.SaveChanges();

        }
        //public IEnumerable<LandmarkPeriodModel> EditLandmarkPeriods()
        //{
        //    int toskip;
        //    totalcount = 0;
        //    totalcount = _dbContext.DictionaryLandmarkPeriod.Count();
        //    toskip = (pageNo - 1) * pageSize;
        //    if (string.IsNullOrEmpty(search))
        //    {

        //        return _dbContext.DictionaryLandmarkPeriod.Skip((pageNo - 1) * pageSize).Take(pageSize).Select(landmarkPeriod => new LandmarkPeriodModel
        //        {
        //            LandmarkPeriodId = landmarkPeriod.LandmarkPeriodId,
        //            LandmarkPeriodName = landmarkPeriod.LandmarkPeriodName,
        //        });
        //    }
        //    else
        //    {
        //        return _dbContext.DictionaryLandmarkPeriod.Where(a => a.LandmarkPeriodName.Contains(search)).Skip((pageNo - 1) * pageSize).Take(pageSize).Select(landmarkPeriod => new LandmarkPeriodModel
        //        {
        //            LandmarkPeriodId = landmarkPeriod.LandmarkPeriodId,
        //            LandmarkPeriodName = landmarkPeriod.LandmarkPeriodName,
        //        });
        //    }
        //}

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

        public DictionaryCountryModel AddDictionaryCountry(DictionaryCountryModel country)
        {
            if (country.CountryId != null)
                if (!String.IsNullOrWhiteSpace(country.CountryName))
                {
                    _dbContext.Add(country);
                    _dbContext.SaveChanges();
                }
            return null;
        }

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

                if (county.CountyId == 0)
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

