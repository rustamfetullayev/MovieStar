using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MovieStar.Models;

namespace MovieStar.ViewModels
{
    public class IndexFilmVM
    {
        public Film Top_IMDB_Film { get; set; }
        public IEnumerable<Film> Last_Added_5_Film { get; set; }
        public IEnumerable<Film> Top_Rated_5_Film { get; set; }
        public IEnumerable<Film> Most_Popular_3_Film { get; set; }
        public IEnumerable<Film_to_Genre> Genres_of_Film { get; set; }
        public IEnumerable<Film_to_Country> Country_of_Film { get; set; }
    }
}