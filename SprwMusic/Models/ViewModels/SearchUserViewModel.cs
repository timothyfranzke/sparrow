using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SprwMusic.Models.ViewModels
{
    public class SearchUserViewModel
    {
        public IEnumerable<UserModel> Users { get; set; } 
    }
}