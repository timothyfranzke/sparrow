﻿using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Newtonsoft.Json;
using SprwMusic.BLL;
using SprwMusic.BLL.Impl;
using SprwMusic.Models;
using SprwMusic.Models.AuthModels;
using SprwMusic.Models.CreateModels;
using SprwMusic.Models.ViewModels;

namespace SprwMusic.Controllers
{
    public class ArtistController : BaseController
    {
        private readonly IArtist _artist;
        private readonly IAuth _auth;
        public ArtistController()
        {
            _artist = new Artist();
            _auth = new Auth();
        }

        public ArtistController(Artist artist)
        {
            _artist = artist;
        }

        public string GetArtist(int artistId)
        {
            var artist = _artist.GetArtistById(artistId);

            return JsonConvert.SerializeObject(artist);
        }

        // GET: Artist/Details/5
        public string GetArtists(string email, string token)
        {
            if (_auth.VerifyToken(token, email))
            {
                var artists = _artist.GetArtists(email);
                return JsonConvert.SerializeObject(artists);
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

        public string GetGenres()
        {
            try
            {
                return JsonConvert.SerializeObject(_artist.GetGenres());
            }
            catch (Exception)
            {
                return "error";
            }
        }
        //POST: Artist/CreateImage
        [HttpPost]
        public string AddGenre(int artistId, int genreId)
        {
            try
            {
                return JsonConvert.SerializeObject(_artist.AddGenre(artistId, genreId));
            }
            catch (Exception)
            {
                return "error";
            }
        }
        [HttpPost]
        public string RemoveGenre(int artistId, int genreId)
        {
            try
            {
                return JsonConvert.SerializeObject(_artist.RemoveGenre(artistId, genreId));
            }
            catch (Exception)
            {
                return "error";
            }
        }
        // POST: Artist/CreateArtist
        [HttpPost]
        public string CreateArtist(CreateArtistModel model)
        {
            if (_auth.VerifyToken(model.Token, model.UserEmail))
            {
                CreateViewModel status;
                try
                {
                    status = _artist.CreateArtist(model);
                }
                catch (Exception e)
                {
                    status = new CreateViewModel()
                    {
                        Status = new StatusModel
                        {
                            Success = false,
                            Messages = new List<string>(){
                            "Exception: " + e.Message
                        }
                        }
                    };
                }

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

        //POST: Artist/CreateImage
        [HttpPost]
        public String CreateImage(CreateImgBase64Model model)
        {
            model.Image64 = model.Image64.Substring(model.Image64.IndexOf(",") + 1);
            var bytes = Convert.FromBase64String(model.Image64);
            if (Verify(model.Token, model.UserEmail, model.ArtistId))
            {
                var success = _artist.CreateartistImg(model);
                //return "{success : " + success + " }";
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
            return "";
        }

        public string CreateAssociation(CreateArtistAssociation model)
        {
            if (Verify(model.Token, model.UserEmail, model.ArtistId))
            {
                var success = _artist.CreateAssociation(model);
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

        public string CreateEvent(CreateEventModel model)
        {
            if (Verify(model.Token, model.UserEmail, model.ArtistId))
            {
                var success = _artist.CreateEvent(model);
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
    }
}
