using SprwMusic.Models.ViewModels;

namespace SprwMusic.BLL
{
    public interface ISearch
    {
        SearchViewModel SearchByName(string name);
        SearchUserViewModel SearchUsers(string email);
    }
}
