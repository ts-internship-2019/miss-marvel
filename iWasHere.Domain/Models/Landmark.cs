using System;
using System.Collections.Generic;

namespace iWasHere.Domain.Models
{
    public partial class Landmark
    {
        public Landmark()
        {
            LandmarkPicture = new HashSet<LandmarkPicture>();
            LandmarkReview = new HashSet<LandmarkReview>();
            TicketXlandmark = new HashSet<TicketXlandmark>();
        }

        public int LandmarkId { get; set; }
        public string LandmarkName { get; set; }
        public string LandmarkDescription { get; set; }
        public bool? LandmarkTicket { get; set; }
        public int? LandmarkTypeId { get; set; }
        public int? LandmarkPeriodId { get; set; }
        public int? LandmarkAddressId { get; set; }
        public string LandmarkCode { get; set; }
        public int? CityId { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }

        public virtual DictionaryCity City { get; set; }
        public virtual DictionaryLandmarkPeriod LandmarkPeriod { get; set; }
        public virtual DictionaryLandmarkType LandmarkType { get; set; }
        public virtual ICollection<LandmarkPicture> LandmarkPicture { get; set; }
        public virtual ICollection<LandmarkReview> LandmarkReview { get; set; }
        public virtual ICollection<TicketXlandmark> TicketXlandmark { get; set; }
    }
}
