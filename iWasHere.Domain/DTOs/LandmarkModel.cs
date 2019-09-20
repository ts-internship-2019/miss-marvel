using System;
using System.Collections.Generic;
using System.Text;

namespace iWasHere.Domain.DTOs
{
    public class LandmarkModel
    {
        public int LandmarkId { get; set; }
        public string LandmarkName { get; set; }
        public string LandmarkDescription { get; set; }
        public bool? LandmarkTicket { get; set; }
        public string LandmarkCode { get; set; }
        public decimal StudentPrice { get; set; }
        public decimal AdultPrice { get; set; }
        public decimal RetiredPrice { get; set; }
        
        public int CityId { get; set; }
        public int LandmarkTypeId { get; set; }
        public int LandmarkPeriodId { get; set; }
        public virtual DictionaryCityModel City { get; set; }
        public virtual LandmarkPeriodModel LandmarkPeriod { get; set; }
        public virtual DictionaryLandmarkTypeModel LandmarkType { get; set; }
    }
}
