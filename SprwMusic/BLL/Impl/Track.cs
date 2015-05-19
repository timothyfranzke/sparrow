using System;
using System.Collections.Generic;
using SprwMusic.Models;
using SprwMusic.Models.CreateModels;
using SprwMusic.Models.ViewModels;
using SprwMusic.Repository.Impl;
using SprwMusic.Utils;

namespace SprwMusic.BLL.Impl
{
    public class Track: ITrack
    {
        private readonly TrackRepository _repository;
        public Track()
        {
            _repository = new TrackRepository();
        }

        public Track(TrackRepository trackRepo)
        {
            _repository = trackRepo;
        }

        public CreateViewModel CreateSingleTrack(CreateTrackModel model)
        {
            var status = new CreateViewModel();

            var trackStat = _repository.CreateTrack(model);
            if (trackStat.Status.Success)
            {
                var dirStat = CreateTrackDirectory(model, trackStat.Id);
                if (!dirStat.Success)
                {
                    _repository.DeleteTrack(trackStat.Id);
                    status.Status.Success = false;
                    status.Status.Messages = new List<string> {"unable to create file"};
                }
            }

            return status;
        }

        public CreateViewModel CreateMultipleTracks(IEnumerable<CreateTrackModel> models)
        {
            var messages = new List<string>();
            var status = new CreateViewModel();

            return status;
        }

        private StatusModel CreateTrackDirectory(CreateTrackModel model, int trackId)
        {
            var albums = String.Empty;
            if (model.AlbumId < 0)
            {
                albums = "/albums/singles";
            }
            else
            {
                albums = "/albums/" + model.AlbumId;
            }
            var dir = "/artists/" + model.ArtistId + albums + "/tracks/" + trackId + "/";
            
            var status = new StatusModel
            {
                Messages = new List<string>()
            };
            try
            {
                status.Success = FileUtil.CreateDirectory(dir);
                status.Success = FileUtil.CreateFile(dir, model.Track, trackId);
            }
            catch (Exception e)
            {
                status.Success = false;
            }

            return status;
        }
    }
}