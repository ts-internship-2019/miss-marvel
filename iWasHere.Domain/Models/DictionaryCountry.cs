using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace iWasHere.Domain.Models
{
    public partial class DictionaryCountry
    {
        public DictionaryCountry()
        {
            DictionaryCounty = new HashSet<DictionaryCounty>();
        }

        public int CountryId { get; set; }
        [Required]
        [DisplayName("Country Name")]
        public string CountryName { get; set; }
        [DisplayName("Country Code")]
        public string CountryCode { get; set; }

        public virtual ICollection<DictionaryCounty> DictionaryCounty { get; set; }
    }
}
