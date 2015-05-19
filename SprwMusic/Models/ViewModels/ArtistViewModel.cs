using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sparrow_Music.Models;
using SprwMusic;


namespace SprwMusic.Models.ViewModels
{
    public class ArtistViewModel
    {
        public StatusModel Status { get; set; }
        public ArtistModel Artist { get; set; }
    }
}