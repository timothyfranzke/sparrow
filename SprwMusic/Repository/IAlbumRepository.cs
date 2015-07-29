using SprwMusic.Models;
using SprwMusic.Models.CreateModels;
using SprwMusic.Models.ViewModels;

namespace SprwMusic.Repository
{
    public interface IAlbumRepository
    {
        int GetArtistId(int albumId);
        AlbumViewModel GetAlbum(int artistId);
        CreateViewModel CreateAlbum(CreateAlbumModel model);
        CreateViewModel CreateAlbumImage(CreateAlbumImageModel model);
        StatusModel DeleteAlbum(int id);
        StatusModel DeleteAlbumImage(int id);
    }
}
