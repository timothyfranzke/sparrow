using System.Collections.Generic;
using SprwMusic.Models;
using SprwMusic.Models.CreateModels;
using SprwMusic.Models.ViewModels;

namespace SprwMusic.Repository
{
    public interface IArtistRepository
    {
        CreateViewModel CreateArtist(CreateArtistModel artist);
        StatusModel CreateArtistAssociation(string email, int artistId);
        IEnumerable<ArtistListModel> GetArtists(string email);
        ArtistViewModel GetArtistById(int artistId);

    }
}
