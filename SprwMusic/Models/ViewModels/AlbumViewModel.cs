using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SprwMusic.Models.ViewModels
{
    public class AlbumViewModel
    {
        public StatusModel Status { get; set; }
        public IEnumerable<SPRW_ALBUM> Albums { get; set; } 
    }
}