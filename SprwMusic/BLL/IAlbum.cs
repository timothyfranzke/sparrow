using SprwMusic.Models.CreateModels;
using SprwMusic.Models.ViewModels;

namespace SprwMusic.BLL
{
    public interface IAlbum
    {
        CreateViewModel CreateAlbum(CreateAlbumModel model);
        bool CreateAlbumImg(CreateImgModel model);
        byte[] GetFile(int? artistId, int? albumId);
    }
}