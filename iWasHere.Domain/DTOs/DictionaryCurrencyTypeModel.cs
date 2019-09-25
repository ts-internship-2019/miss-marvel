using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace iWasHere.Domain.DTOs
{
    
    public class DictionaryCurrencyTypeModel
    {
     
        public int CurrencyTypeId { get; set; }
        [Required]
        [DisplayName("Take your time")]
        public string CurrencyName { get; set; }
        [Required(ErrorMessage = "This field is mandatory")]
        public string CurrencyCode { get; set; }
        [Required(ErrorMessage = "This field is mandatory")]
        public decimal CurrencyExRate { get; set; }

       
    }
    
}
