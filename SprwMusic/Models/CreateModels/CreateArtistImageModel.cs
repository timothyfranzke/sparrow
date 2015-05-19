using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SprwMusic.Models.CreateModels
{
    public class CreateArtistImageModel
    {
        public string UserEmail { get; set; }
        public string Token { get; set; }
        public HttpPostedFileBase Image { get; set; }
    }
}