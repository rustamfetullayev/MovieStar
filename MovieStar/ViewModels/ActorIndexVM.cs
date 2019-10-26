using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MovieStar.Models;

namespace MovieStar.ViewModels
{
    public class ActorIndexVM
    {
        public IEnumerable<Actor> Actors { get; set; }
        public IEnumerable<Film_to_Actor> Actor_of_Film { get; set; }
    }
}