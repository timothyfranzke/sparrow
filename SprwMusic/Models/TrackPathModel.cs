using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SprwMusic.Models
{
    public class TrackPathModel
    {
        public int ArtistId { get; set; }
        public int? AlbumId { get; set; }
        public int TrackId { get; set; }
    }
}