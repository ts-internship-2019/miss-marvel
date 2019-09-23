﻿using System;
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
       
        [Key]
        public int LandmarkPeriodId { get; set; }
        [Column(TypeName = "varchar(256)")]
        [Required]
        [DisplayName("Interval pentru vizite")]
        public string LandmarkPeriodName { get; set; }
    }
}
