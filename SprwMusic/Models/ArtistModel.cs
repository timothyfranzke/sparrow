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
        public IEnumerable<AlbumModel> Albums { get; set; }

    }
}