using Sparrow_Music.Models.ViewModels;
using SprwMusic.Models.AuthModels;
using SprwMusic.Models.CreateModels;
using SprwMusic.Models.ViewModels;
using SprwMusic.Repository;
using SprwMusic.Repository.Impl;
using SprwMusic.Utils;

namespace SprwMusic.BLL.Impl
{
    public class User: IUser
    {
        private readonly IUserRepository _repository;
        private readonly IArtistRepository _artistRepo;

        public User()
        {
            _repository = new UserRepository();
            _artistRepo = new ArtistRepository();
        }

        public User(UserRepository repo)
        {
            _repository = repo;
        }
        public AuthUserViewModel CreateUser(CreateUserModel model)
        {
            //var userInfo = _repository.GetUser(model.Email);
            var pwSalt = AuthUtils.GenerateSalt();
            var saltedPassword = pwSalt + model.Password;
            model.Password = AuthUtils.GenerateHash(saltedPassword);
            
            var user = _repository.CreateUser(model, pwSalt);
            var authUser = new AuthUserViewModel();
            if (user != null)
            {
                authUser.Authenticated = true;
                authUser.Token = AuthUtils.GenerateToken(user);
            }

            return authUser;
        }

    }
}