using System.Collections.Generic;
using SprwMusic.Models;
using SprwMusic.Models.AuthModels;
using SprwMusic.Models.CreateModels;
using SprwMusic.Models.ViewModels;

namespace SprwMusic.BLL
{
    public interface IArtist
    {
        bool CreateartistImg(CreateImgModel model);
        CreateViewModel CreateArtist(CreateArtistModel model);
        CreateViewModel CreateEvent(CreateEventModel model);
        IEnumerable<ArtistListModel> GetArtists(string email);
        ArtistViewModel GetArtistById(int artistId);
        byte[] GetFile(int? artistId);
        bool CreateAssociation(CreateArtistAssociation model);
    }
}
