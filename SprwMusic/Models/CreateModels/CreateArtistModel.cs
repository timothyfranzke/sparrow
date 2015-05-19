using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SprwMusic.Models.CreateModels
{
    public class    CreateArtistModel
    {
        public string UserEmail { get; set; }
        public string Token { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}