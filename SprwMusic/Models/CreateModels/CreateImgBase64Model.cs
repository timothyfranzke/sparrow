using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SprwMusic.Models.CreateModels
{
    public class CreateImgBase64Model
    {
        public string UserEmail { get; set; }
        public string Token { get; set; }
        public int ArtistId { get; set; }
        public int TrackingId { get; set; }
        public string Name { get; set; }
        public bool Primary { get; set; }
        public string Description { get; set; }
        public string Image64 { get; set; }
    }
}