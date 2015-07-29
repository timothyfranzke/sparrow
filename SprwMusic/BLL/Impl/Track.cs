using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using SprwMusic.Models;
using SprwMusic.Models.CreateModels;
using SprwMusic.Models.ViewModels;
using SprwMusic.Repository;
using SprwMusic.Repository.Impl;
using SprwMusic.Utils;

namespace SprwMusic.BLL.Impl
{
    public class Track: ITrack
    {
        private readonly ITrackRepository _repository;
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

        public bool CreateTrackImg(CreateImgModel model)
        {
            return CreateTrackImgDirectory(model);
        }

        public bool ModifyTrackPopularity(CreateTrackPopularModel model)
        {
            var success = true;
            switch (model.Criteria)
            {
                case "playthrough":
                    success = _repository.AddTrackPopularityPlayThrough(model);
                    break;
                case "dislike":
                    success = _repository.AddTrackPopularityDislike(model);
                    break;
                case "like":
                    success = _repository.AddTrackPopularityLike(model);
                    break;
                default:
                    break;
            }
            return success;
        }

        public byte[] GetFile(string type, int? artistId, int? albumId, int? trackId)
        {
            byte[] bytes = new byte[] {};
            switch (type)
            {
                case "image":
                    bytes = FileUtil.GetImgFile(artistId, albumId, trackId);
                    break;
                case "audio":
                    bytes = FileUtil.GetAudioFile(artistId, albumId, trackId);
                    break;
            }
            return bytes;
        }

        private StatusModel CreateTrackDirectory(CreateTrackModel model, int trackId)
        {
            var albums = String.Empty;
            if (model.AlbumId == null)
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

        private bool CreateTrackImgDirectory(CreateImgModel model)
        {
            var success = true;
            var trackPath = _repository.GetTrackPath(model.TrackingId);
            var albums = String.Empty;
            if (trackPath.AlbumId == null)
            {
                albums = "/albums/singles";
            }
            else
            {
                albums = "/albums/" + trackPath.AlbumId;
            }
            var dir = "/artists/" + trackPath.ArtistId + albums + "/tracks/" + trackPath.TrackId + "/img/";

            var status = new StatusModel
            {
                Messages = new List<string>()
            };
            try
            {
                success = FileUtil.CreateDirectory(dir);
                success = FileUtil.CreateImgFile(dir, model.AlbumImage, model.TrackingId);
            }
            catch (Exception e)
            {
                success = false;
            }

            return success;
        }

    }
}