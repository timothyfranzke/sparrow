using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SprwMusic.Models.CreateModels
{
    public class CreateAlbumModel
    {
        public string UserEmail { get; set; }
        public string Token { get; set; }
        public int ArtistId { get; set; }
        public string AlbumName { get; set; }
        public string Description { get; set; }
        public DateTime ReleaseDate { get; set; }

    }
}