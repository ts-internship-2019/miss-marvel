using System;
using System.Collections.Generic;
using System.Text;

namespace iWasHere.Domain.DTOs
{
    public class LandmarkModelList
    {
        public List<LandmarkModel> LandmarkList { get; set; }

        public string LandmarkName { get; set; }
        public string City { get; set; }
    }
}
