using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SprwMusic.Models.CreateModels
{
    public class CreateTrackModel
    {
        public string UserEmail { get; set; }
        public string Token { get; set; }
        public int AlbumId { get; set; }
        public int ArtistId { get; set; }
        public HttpPostedFileBase Track { get; set; }
        public string TrackName { get; set; }
        public string Description { get; set; }
        public DateTime? ReleaseDate { get; set; }

    }
}