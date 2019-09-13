using System;
using System.Collections.Generic;

namespace iWasHere.Web.Models
{
    public partial class DictionaryCounty
    {
        public DictionaryCounty()
        {
            DictionaryCity = new HashSet<DictionaryCity>();
        }

        public int CountyId { get; set; }
        public string CountyName { get; set; }
        public int? CountryId { get; set; }
        public string CountyCode { get; set; }

        public virtual DictionaryCountry Country { get; set; }
        public virtual ICollection<DictionaryCity> DictionaryCity { get; set; }
    }
}
