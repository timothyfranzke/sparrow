using SprwMusic.Models;
using SprwMusic.Models.CreateModels;
using SprwMusic.Models.ViewModels;

namespace SprwMusic.Repository
{
    public interface ITrackRepository
    {
        CreateViewModel CreateTrack(CreateTrackModel model);
        StatusModel DeleteTrack(int id);
    }
}