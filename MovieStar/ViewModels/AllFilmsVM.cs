    using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MovieStar.Models;

namespace MovieStar.ViewModels
{
    public class AllFilmsVM
    {
        public IEnumerable<Film> Films { get; set; }
        public IEnumerable<Film_to_Genre> Genres_of_Film { get; set; }
        public IEnumerable<Film_to_Country> Country_of_Film { get; set; }
        public IEnumerable<Film_to_Actor> Actor_of_Film { get; set; }
        public IEnumerable<Genre> Genres { get; set; }
        public IEnumerable<Dublaj> Dublaj { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<SavedFilm> SavedFilms { get; set; }
    }
}