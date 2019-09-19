using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace iWasHere.Domain.Models
{
    public partial class DictionaryLandmarkType
    {
        public DictionaryLandmarkType()
        {
            Landmark = new HashSet<Landmark>();
        }

        public int DictionaryItemId { get; set; }
        //[DisplayName("Item Code")]
        //[Required (ErrorMessage = "This field is required")]
        public string DictionaryItemCode { get; set; }
        //[Required(ErrorMessage = "This field is required")]
        //[DisplayName("Item Name")]
        public string DictionaryItemName { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Landmark> Landmark { get; set; }
    }
}
