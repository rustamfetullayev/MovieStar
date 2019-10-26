using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MovieStar.Models;

namespace MovieStar.ViewModels
{
    public class UserPersonalVM
    {
        public User User { get; set; }
        public IEnumerable<Film> LikedFilms { get; set; }
        public IEnumerable<Film> SavedFilms { get; set; }
        public IEnumerable<Film_to_Genre> Genres_of_Film { get; set; }
        public IEnumerable<Film_to_Country> Country_of_Film { get; set; }
    }
}