using System.Web.Mvc;
using Newtonsoft.Json;
using SprwMusic.BLL;
using SprwMusic.BLL.Impl;

namespace SprwMusic.Controllers
{
    public class SearchController : Controller
    {
        private readonly ISearch _search;
        public SearchController()
        {
            _search = new Search();
        }
        public SearchController(Search search)
        {
            _search = search;
        }
       
        public string MultiSearch(string name)
        {
            var results = _search.SearchByName(name);
             
            return JsonConvert.SerializeObject(results);
        }
    }
}
