using System.Collections.Generic;
using SprwMusic.Models;
using SprwMusic.Models.AuthModels;
using SprwMusic.Models.CreateModels;
using SprwMusic.Models.ViewModels;

namespace SprwMusic.BLL
{
    public interface IArtist
    {
        CreateViewModel CreateArtist(CreateArtistModel model);
        IEnumerable<ArtistListModel> GetArtists(string email);
        ArtistViewModel GetArtistById(int artistId);
    }
}
