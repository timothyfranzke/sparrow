using SprwMusic.Models.CreateModels;
using SprwMusic.Models.ViewModels;

namespace SprwMusic.BLL
{
    public interface IAlbum
    {
        CreateViewModel CreateAlbum(CreateAlbumModel model);
    }
}