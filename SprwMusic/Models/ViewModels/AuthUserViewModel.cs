using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sparrow_Music.Models.ViewModels
{
    public class AuthUserViewModel
    {
        public string Token { get; set; }
        public bool Authenticated { get; set; }
    }
}