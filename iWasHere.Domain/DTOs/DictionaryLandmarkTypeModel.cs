using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace iWasHere.Domain.DTOs
{
   public  class DictionaryLandmarkTypeModel
    {
        public int DictionaryItemId { get; set; }
        public string DictionaryItemCode { get; set; }
        public string DictionaryItemName { get; set; }

        public string Description { get; set; }
    }
}
