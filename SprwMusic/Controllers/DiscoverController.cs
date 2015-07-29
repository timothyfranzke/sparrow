using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using SprwMusic.BLL;
using SprwMusic.BLL.Impl;

namespace SprwMusic.Controllers
{
    public class DiscoverController : Controller
    {
        private readonly IDiscover _discover;

        public DiscoverController()
        {
            _discover = new Discover();
        }

        public string GetPlaylist( int page )
        {
            var playList = _discover.GetPlaylist(page);
            return JsonConvert.SerializeObject(playList);
        }

        public string GetEvents(string email)
        {
            var events = _discover.GetEvents(email);
            return JsonConvert.SerializeObject(events);
        }
    }
}