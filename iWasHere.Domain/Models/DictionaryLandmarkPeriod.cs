using System;
using System.Collections.Generic;

namespace iWasHere.Domain.Models
{
    public partial class DictionaryLandmarkPeriod
    {
        public DictionaryLandmarkPeriod()
        {
            Landmark = new HashSet<Landmark>();
        }

        public int LandmarkPeriodId { get; set; }
        public string LandmarkPeriodName { get; set; }

        public virtual ICollection<Landmark> Landmark { get; set; }
    }
}
