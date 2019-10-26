using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MovieStar.Models;

namespace MovieStar.ViewModels
{
    public class DirectorIndexVM
    {
        public IEnumerable<Director> Directors { get; set; }
        public IEnumerable<Film_to_Director> Director_of_Film { get; set; }
    }
}