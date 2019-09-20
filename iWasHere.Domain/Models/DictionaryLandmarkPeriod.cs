using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace iWasHere.Domain.Models
{
    public partial class DictionaryLandmarkPeriod
    {
        public DictionaryLandmarkPeriod()
        {
            Landmark = new HashSet<Landmark>();
        }

        public int LandmarkPeriodId { get; set; }
        [Column(TypeName = "varchar(256)")]
        [Required(ErrorMessage ="Cimp obligatoriu")]
        [DisplayName("Interval")]
        public string LandmarkPeriodName { get; set; }

        public virtual ICollection<Landmark> Landmark { get; set; }
    }
}
