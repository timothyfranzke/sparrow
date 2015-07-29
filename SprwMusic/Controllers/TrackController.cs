using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;
using Newtonsoft.Json;
using SprwMusic.BLL;
using SprwMusic.BLL.Impl;
using SprwMusic.Models;
using SprwMusic.Models.CreateModels;
using SprwMusic.Models.ViewModels;


namespace SprwMusic.Controllers
{
    public class TrackController : BaseController
    {
        private readonly ITrack _track;
        private readonly IAuth _auth;
        public TrackController()
        {
            _track = new Track();
            _auth = new Auth();
            
        }

        public TrackController(Track track)
        {
            _track = track;
        }

        // POST: Track/Create
        [HttpPost]
        public string Create(CreateTrackModel model)
        {
            if (Verify(model.Token, model.UserEmail, model.ArtistId))
            {
                var messages = new List<string>();
                var status = _track.CreateSingleTrack(model);
                return JsonConvert.SerializeObject(status);
            }
            else
            {
                return JsonConvert.SerializeObject(new StatusModel()
                {
                    Success = false,
                    Messages = new List<string>()
                    {
                        "unauthenticated"
                    }
                });
            }
        }

        [HttpPost]
        public string CreateImage(CreateImgModel model)
        {
            if (Verify(model.Token, model.UserEmail, model.ArtistId))
            {
                var success = _track.CreateTrackImg(model);
                return "{success : " + success + " }";
            }
            else
            {
                return JsonConvert.SerializeObject(new StatusModel()
                {
                    Success = false,
                    Messages = new List<string>()
                    {
                        "unauthenticated"
                    }
                });
            }
            
        }

        [HttpPost]
        public string TrackPopularity(CreateTrackPopularModel model)
        {
            var success = _track.ModifyTrackPopularity(model);
            return "{success : " + success + " }";
        }

        [HttpGet]
        public ActionResult GetAudioFile(int artistId, int? albumId, int trackId)
        {
            var bytes = _track.GetFile("audio", artistId, albumId, trackId);

            return File(bytes, "audio/mpeg", trackId.ToString());
        }

        public ActionResult GetImageFile(int artistId, int albumId, int trackId)
        {
            var bytes = _track.GetFile("image", artistId, albumId, trackId);

            return File(bytes, "image/jpg", trackId + ".jpg");
        }

    }
}
