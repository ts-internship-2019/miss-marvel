using System;
using System.Collections.Generic;

namespace iWasHere.Domain.Models
{
    public partial class LandmarkPicture
    {
        public int PictureId { get; set; }
        public string PictureName { get; set; }
        public int? LandmarkId { get; set; }

        public virtual Landmark Landmark { get; set; }

        public LandmarkPicture(int pictureId, string pictureName, int? landmarkId)
        {
            PictureId = pictureId;
            PictureName = pictureName;
            LandmarkId = landmarkId;
        }

        public LandmarkPicture()
        {
        }

        public LandmarkPicture(int pictureId, string pictureName)
        {
            PictureId = pictureId;
            PictureName = pictureName;
        }
    }

}
