using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SprwMusic.Models.ViewModels;

namespace SprwMusic.BLL
{
    public interface IDiscover
    {
        PlaylistViewModel GetPlaylist(int page);
        EventViewModel GetEvents(string email);
    }
}
