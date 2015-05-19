using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sparrow_Music.Models.ViewModels;
using SprwMusic.Models.AuthModels;
using SprwMusic.Repository;
using SprwMusic.Repository.Impl;
using SprwMusic.Utils;

namespace SprwMusic.BLL.Impl
{
    public class Auth:IAuth
    {
        private readonly IArtistRepository _artistRepo;
        private readonly IUserRepository _userRepo;

        public Auth()
        {
            _artistRepo = new ArtistRepository();;
            _userRepo = new UserRepository();
        }
        public bool VerifyArtist(string email, int artistId)
        {
            var count = 0;
            var success = true;
            var artists = _artistRepo.GetArtists(email);
            foreach (var artist in artists)
            {
                if (artist.ArtistId == artistId)
                {
                    count++;
                }
            }
            if (count == 0)
            {
                success = false;
            }

            return success;
        }

        public AuthUserViewModel AuthenticateUser(AuthUserModel model)
        {
            var authModel = new AuthUserViewModel
            {
                Authenticated = false,
                Token = ""
            };
            var user = _userRepo.GetUser(model.Email);
            var saltedPassword = user.SALT + model.Password;
            var hashedPassword = AuthUtils.GenerateHash(saltedPassword);

            if (hashedPassword.Equals(user.PASSWORD))
            {
                authModel.Authenticated = true;
                authModel.Token = AuthUtils.GenerateToken(user);
            }

            return authModel;
        }

        public bool VerifyToken(string token, string email)
        {
            var user = _userRepo.GetUser(email);
            var success = token.Equals(AuthUtils.GenerateToken(user));

            return success;
        }
    }
}