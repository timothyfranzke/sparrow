using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SprwMusic.Models
{
    public class StatusModel
    {
        public bool Success { get; set; }
        public IEnumerable<String> Messages { get; set; }
    }
}