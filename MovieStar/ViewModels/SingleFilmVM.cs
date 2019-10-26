using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MovieStar.Models;

namespace MovieStar.ViewModels
{
    public class SingleFilmVM
    {
        public Film Film { get; set; }
        public IEnumerable<Film_to_Genre> Genres_of_Film { get; set; }
        public IEnumerable<Film_to_Country> Country_of_Film { get; set; }
        public IEnumerable<Film_to_Director> Director_of_Film { get; set; }
        public IEnumerable<Film_to_Actor> Actor_of_Film { get; set; }
        public IEnumerable<Comment> Comments { get; set; }
        public IEnumerable<LikedFilm> LikedFilms { get; set; }
        public IEnumerable<DislikedFilm> DislikedFilms { get; set; }
    }
}