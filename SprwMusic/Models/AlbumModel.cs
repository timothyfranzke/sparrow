using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SprwMusic.Models
{
    public class AlbumModel
    {
        public int AlbumId { get; set; }
        public string AlbumName { get; set; }
        public IEnumerable<TrackModel> Tracks { get; set; } 
    }
}