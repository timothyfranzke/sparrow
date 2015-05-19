using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sparrow_Music.Models.ViewModels;
using SprwMusic.Models.AuthModels;

namespace SprwMusic.BLL
{
    public interface IAuth
    {
        bool VerifyToken(string token, string email);
        bool VerifyArtist(string email, int artistId);
        AuthUserViewModel AuthenticateUser(AuthUserModel model);
    }
}
