using System;
using System.Collections.Generic;

namespace iWasHere.Web.Models
{
    public partial class LandmarkReview
    {
        public int LandmarkReviewId { get; set; }
        public string ReviewTitle { get; set; }
        public string ReviewComment { get; set; }
        public int? LandmarkId { get; set; }
        public string UserId { get; set; }
        public int Rating { get; set; }

        public virtual Landmark Landmark { get; set; }
    }
}
