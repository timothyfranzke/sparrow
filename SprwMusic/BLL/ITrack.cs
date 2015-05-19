using SprwMusic.Models.CreateModels;
using SprwMusic.Models.ViewModels;

namespace SprwMusic.BLL
{
    public interface ITrack
    {
        CreateViewModel CreateSingleTrack(CreateTrackModel model);
    }
}
