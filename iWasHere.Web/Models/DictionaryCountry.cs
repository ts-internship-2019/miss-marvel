using System;
using System.Collections.Generic;

namespace iWasHere.Web.Models
{
    public partial class DictionaryCountry
    {
        public DictionaryCountry()
        {
            DictionaryCounty = new HashSet<DictionaryCounty>();
        }

        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public string CountryCode { get; set; }

        public virtual ICollection<DictionaryCounty> DictionaryCounty { get; set; }
    }
}
