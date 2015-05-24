using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using SprwMusic.Models.ViewModels;

namespace SprwMusic.Repository
{
    public interface IDiscoverRepository
    {
        PlaylistViewModel GetPlaylist(int page);
        EventViewModel GetEvents(string email);
    }
}
