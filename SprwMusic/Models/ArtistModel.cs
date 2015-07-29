using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SprwMusic.Models
{
    public class ArtistModel
    {
        public int ArtistId { get; set; }
        public string AristName { get; set; }
        public string Description { get; set; }
        public List<AlbumModel> Albums { get; set; }
        public List<EventModel> Events { get; set; } 

    }
}