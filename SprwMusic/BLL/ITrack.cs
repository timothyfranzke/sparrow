using SprwMusic.Models;
using SprwMusic.Models.CreateModels;
using SprwMusic.Models.ViewModels;

namespace SprwMusic.BLL
{
    public interface ITrack
    {
        CreateViewModel CreateSingleTrack(CreateTrackModel model);
        bool ModifyTrackPopularity(CreateTrackPopularModel model);
        bool CreateTrackImg(CreateImgModel model);
        byte[] GetFile(string type, int? artistId, int? albumId, int? trackId);
    }
}
