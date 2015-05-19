using Sparrow_Music.Models.ViewModels;
using SprwMusic.Models.AuthModels;
using SprwMusic.Models.CreateModels;
using SprwMusic.Models.ViewModels;

namespace SprwMusic.BLL
{
    public interface IUser
    {
        AuthUserViewModel CreateUser(CreateUserModel model);
    }
}
