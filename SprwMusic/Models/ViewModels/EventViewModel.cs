﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SprwMusic.Models.ViewModels
{
    public class EventViewModel
    {
        IEnumerable<EventModel> Events { get; set; }
    }
}