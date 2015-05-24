using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SprwMusic.Models.ViewModels;
using SprwMusic.Repository;
using SprwMusic.Repository.Impl;

namespace SprwMusic.BLL.Impl
{
    public class Discover: IDiscover
    {
        private readonly IDiscoverRepository _repository;
        public Discover()
        {
            _repository = new DiscoverRepository();
        }
        public PlaylistViewModel GetPlaylist(int page)
        {
            return _repository.GetPlaylist(page);
        }
    }
}