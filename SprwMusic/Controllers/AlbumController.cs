using System.Collections.Generic;
using System.Web.Mvc;
using Newtonsoft.Json;
using SprwMusic.BLL;
using SprwMusic.BLL.Impl;
using SprwMusic.Models;
using SprwMusic.Models.CreateModels;
using SprwMusic.Models.ViewModels;

namespace SprwMusic.Controllers
{
    public class AlbumController : BaseController
    {
        private readonly IAlbum _album;
        private readonly IAuth _auth;
        public AlbumController()
        {
            _album = new Album();
            _auth = new Auth();
        }

        public string GetAlbums(int artistId)
        {
            
            return "";
        }

        // POST: Album/Create
        [HttpPost]
        public string CreateAlbum( CreateAlbumModel model)
        {
            CreateViewModel status;
            if (Verify(model.Token, model.UserEmail, model.ArtistId))
            {
                status = _album.CreateAlbum(model);
            }
            else
            {
                status = new CreateViewModel()
                {
                    Status = new StatusModel
                    {
                        Success = false,
                        Messages = new List<string>
                        {
                            "Unauthenticated"
                        }
                    }
                };
            }

            return JsonConvert.SerializeObject(status);
        }

        [HttpPost]
        public string CreateImage( CreateImgModel model )
        {
            var status = new StatusModel();
            if (Verify(model.Token, model.UserEmail, model.ArtistId))
            {
                status.Success = _album.CreateAlbumImg(model);
            }
            else
            {
                status.Success = false;
                status.Messages = new List<string>
                {
                    "Unauthenticated"
                };
            }

            return JsonConvert.SerializeObject(status);
        }
   }
}
