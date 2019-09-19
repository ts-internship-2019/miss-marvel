using iWasHere.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace iWasHere.Domain.DTOs
{
    public class DictionaryCityModel
    {
        public string CityName { get; set; }
        public string CityCode { get; set; }
        public int CityId { get; set; }
        public int CountyId { get; set; }
      

    }
}
