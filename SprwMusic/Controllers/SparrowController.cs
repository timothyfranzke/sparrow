using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;
using Sparrow_Music.Models.ViewModels;
using Newtonsoft.Json;
using SprwMusic.BLL;
using SprwMusic.BLL.Impl;
using SprwMusic.Models;
using SprwMusic.Models.AuthModels;
using SprwMusic.Models.CreateModels;
using SprwMusic.Models.ViewModels;

namespace SprwMusic.Controllers
{
    public class SparrowController : AsyncController
    {
        private readonly IUser _user;
        private readonly IAuth _auth;

        public SparrowController()
        {
            _user = new User();
            _auth = new Auth();
        }
        public SparrowController(User user)
        {
            _user = user;
        }
        // GET: Sparrow
        public ActionResult Index()
        {
            return View();
        }

        // POST: Sparrow/Create
        [HttpPost]
        public string CreateUser(CreateUserModel model)
        {
            AuthUserViewModel status;
            try
            {
                status = _user.CreateUser(model);
            }
            catch(Exception e)
            {
                status = new AuthUserViewModel
                {
                    Authenticated = false
                };
            }

            return JsonConvert.SerializeObject(status);
        }

        [HttpPost]
        public string AuthenticateUser(AuthUserModel model)
        {
            AuthUserViewModel authModel;
            try
            {
                authModel = _auth.AuthenticateUser(model);
            }
            catch(Exception e)
            {
                authModel = new AuthUserViewModel
                {
                    Authenticated = false,
                    Token = ""
                };
            }

            return JsonConvert.SerializeObject(authModel);
        }

        public ActionResult GetImageFile(int artistId, int albumId, int trackId)
        {
            var album = "";
            var track = trackId.ToString() + ".jpg";
            if (albumId == -1)
            {
                album = "singles";
            }
            else
            {
                album = albumId.ToString();
            }
            var dir = String.Format("/artists/{0}/albums/{1}/tracks/{2}/img/{3}", artistId.ToString(), album, trackId.ToString(), track);

            var fileLocation = HttpContext.Server.MapPath(dir);
            var bytes = new byte[0];

            using (var fs = new FileStream(fileLocation, FileMode.Open, FileAccess.Read))
            {
                var br = new BinaryReader(fs);
                long numBytes = new FileInfo(fileLocation).Length;
                bytes = br.ReadBytes((int)numBytes);
            }

            return File(bytes, "image/jpg", track);
        }
    }
}
