﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace iWasHere.Domain.DTOs
{
    public class DictionaryCountryModel
    {
        public int CountryId { get; set; }
       
        public string CountryName { get; set; }
       
        public string CountryCode { get; set; }
    }
}
