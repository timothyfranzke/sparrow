using SprwMusic.Models.ViewModels;

namespace SprwMusic.BLL
{
    public interface ISearch
    {
        SearchViewModel SearchByName(string name);
    }
}
