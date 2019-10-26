using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MovieStar.Models;
using MovieStar.ViewModels;

namespace MovieStar.Controllers
{
    public class HomeController : Controller
    {
        private readonly MSEntities db = new MSEntities();

        public ActionResult Index()
        {
            IndexFilmVM vm = new IndexFilmVM
            {
                Top_IMDB_Film = db.Films.OrderByDescending(f => f.IMDB).First(),
                Last_Added_5_Film = db.Films.OrderByDescending(f=>f.UpdateDate).Take(5),
                Top_Rated_5_Film = db.Films.OrderByDescending(f=>f.IMDB).Take(5),
                Most_Popular_3_Film=db.Films.OrderByDescending(f=>f.ViewCount).Take(3),
                Genres_of_Film=db.Film_to_Genre,
                Country_of_Film=db.Film_to_Country
            };

            return View(vm);
        }
    }
}