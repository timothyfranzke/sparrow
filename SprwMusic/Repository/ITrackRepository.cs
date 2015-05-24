using SprwMusic.Models;
using SprwMusic.Models.CreateModels;
using SprwMusic.Models.ViewModels;

namespace SprwMusic.Repository
{
    public interface ITrackRepository
    {
        TrackPathModel GetTrackPath(int trackId);
        CreateViewModel CreateTrack(CreateTrackModel model);
        StatusModel DeleteTrack(int id);
        bool AddTrackPopularityLike(CreateTrackPopularModel model);
        bool AddTrackPopularityDislike(CreateTrackPopularModel model);
        bool AddTrackPopularityPlayThrough(CreateTrackPopularModel model);
    }
}