using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SprwMusic.Models.CreateModels
{
    public class CreateAlbumImageModel
    {
        public int ArtistId { get; set; }
        public int AlbumId { get; set; }
        public string Name { get; set; }
        public bool Primary { get; set; }
        public string Description { get; set; }
        public HttpPostedFileBase AlbumImage { get; set; }
        public string UserId { get; set; }
    }
}