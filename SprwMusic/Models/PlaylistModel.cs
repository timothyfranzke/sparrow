using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SprwMusic.Models
{
    public class PlaylistModel
    {
        public int ArtistId { get; set; }
        public string ArtistName { get; set; }
        public List<PlaylistTrack> Tracks { get; set; }  

    }

    public class PlaylistTrack
    {
        public int? AlbumId { get; set; }
        public string AlbumName { get; set; }
        public int TrackId { get; set; }
        public string TrackUrl { get; set; }
        public string ImageUrl { get; set; }
        public string PrimGenre { get; set; }
        public int PopCount { get; set; }
    }
}