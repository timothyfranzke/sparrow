﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SprwMusic.Models.ViewModels
{
    public class PlaylistViewModel
    {
        public IEnumerable<PlaylistModel> Playlist { get; set; } 
    }
}