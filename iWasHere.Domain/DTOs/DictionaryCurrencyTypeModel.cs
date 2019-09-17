using System;
using System.Collections.Generic;
using System.Text;

namespace iWasHere.Domain.DTOs
{
    public class DictionaryCurrencyTypeModel
    {
        public int CurrencyTypeId { get; set; }
        public string CurrencyName { get; set; }
        public string CurrencyCode { get; set; }
        public decimal CurrencyExRate { get; set; }
    }
}
