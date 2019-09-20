using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace iWasHere.Domain.DTOs
{
    public class LandmarkPeriodModel
    {
        public LandmarkPeriodModel()
        {

        }
       
        public int LandmarkPeriodId { get; set; }
        [Column(TypeName = "varchar(256)")]
        [Required]
        [DisplayName("Interval")]
        public string LandmarkPeriodName { get; set; }
    }
}
