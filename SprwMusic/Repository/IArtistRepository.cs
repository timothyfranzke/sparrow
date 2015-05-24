using System.Collections.Generic;
using SprwMusic.Models;
using SprwMusic.Models.CreateModels;
using SprwMusic.Models.ViewModels;

namespace SprwMusic.Repository
{
    public interface IArtistRepository
    {
        CreateViewModel CreateArtist(CreateArtistModel artist);
        CreateViewModel CreateEvent(CreateEventModel model);
        bool CreateArtistAssociation(string email, int artistId);
        bool CreateArtistAssociation(int artistId, int userId);
        IEnumerable<ArtistListModel> GetArtists(string email);
        ArtistViewModel GetArtistById(int artistId);

    }
}
