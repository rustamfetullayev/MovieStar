using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MovieStar.Models;

namespace MovieStar.ViewModels
{
    public class SingleNewsVM
    {
        public News News { get; set; }
        public IEnumerable<NewsComment> Comments { get; set; }
    }
}