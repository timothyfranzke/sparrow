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
    public class TrackController : Controller
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
                CreateViewModel status = new CreateViewModel
                {
                    Status = new StatusModel
                    {
                        Success = true
                    }
                };
                try
                {
                    status = _track.CreateSingleTrack(model);
                }
                catch (Exception e)
                {
                    status.Status.Success = false;
                    messages.Add("Exception: " + e);
                }
                status.Status.Messages = messages;
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

        [HttpGet]
        public ActionResult GetAudioFile(int artistId, int albumId, int trackId)
        {
            var album = "";
            var track = trackId.ToString() + ".mp3";
            if (albumId == -1)
            {
                album = "singles";
            }
            else
            {
                album = albumId.ToString();
            }
            var dir = String.Format("/artists/{0}/albums/{1}/tracks/{2}/{3}", artistId.ToString(), album, trackId.ToString(), track);

            var fileLocation = HttpContext.Server.MapPath(dir);
            var bytes = new byte[0];

            using (var fs = new FileStream(fileLocation, FileMode.Open, FileAccess.Read))
            {
                var br = new BinaryReader(fs);
                long numBytes = new FileInfo(fileLocation).Length;
                bytes = br.ReadBytes((int)numBytes);
            }

            return File(bytes, "audio/mpeg", track);
        }

        private bool Verify(string token, string email, int artistId)
        {
            var success = _auth.VerifyToken(token, email);
            if (success)
            {
                success = _auth.VerifyArtist(email, artistId);
            }

            return success;
        }
    }
}
