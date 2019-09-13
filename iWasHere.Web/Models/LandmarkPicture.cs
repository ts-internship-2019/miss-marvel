using System;
using System.Collections.Generic;

namespace iWasHere.Web.Models
{
    public partial class LandmarkPicture
    {
        public int PictureId { get; set; }
        public string PictureName { get; set; }
        public int? LandmarkId { get; set; }

        public virtual Landmark Landmark { get; set; }
    }
}
