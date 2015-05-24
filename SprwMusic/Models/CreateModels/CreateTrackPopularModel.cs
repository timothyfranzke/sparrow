using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SprwMusic.Models.CreateModels
{
    public class CreateTrackPopularModel
    {
        public string Criteria { get; set; }
        public int UserId { get; set; }
        public int TrackId { get; set; } 
    }
}