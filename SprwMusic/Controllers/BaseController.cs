using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SprwMusic.BLL;
using SprwMusic.BLL.Impl;

namespace SprwMusic.Controllers
{
    public class BaseController : Controller
    {
        private readonly IAuth _auth;
        public BaseController()
        {
            _auth = new Auth();
        }
        // GET: Base
        protected bool Verify(string token, string email, int artistId)
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