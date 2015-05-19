using SprwMusic;
using SprwMusic.Models.CreateModels;
using SprwMusic.Models.ViewModels;

namespace SprwMusic.Repository
{
    public interface IUserRepository
    {
        SPRW_USER CreateUser(CreateUserModel model, int salt);
        SPRW_USER GetUser(string userId);
        
    }
}
