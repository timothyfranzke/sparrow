using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SprwMusic.Models.ViewModels
{
    public class ArtistListModel
    {
        public int ArtistId { get; set; }
        public string ArtistName { get; set; }
        public List<AlbumModel> Albums { get; set; }
        public List<EventModel> Events { get; set; } 
    }
}