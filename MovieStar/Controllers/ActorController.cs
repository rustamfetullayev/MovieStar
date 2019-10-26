using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MovieStar.Models;
using MovieStar.ViewModels;

namespace MovieStar.Controllers
{
    public class ActorController : Controller
    {
        private readonly MSEntities db = new MSEntities();

        // GET: Actor
        public ActionResult Index()
        {
            ActorIndexVM vm = new ActorIndexVM
            {
                Actors=db.Actors.OrderBy(a=>a.Name).Take(12),
                Actor_of_Film=db.Film_to_Actor
            };

            return View(vm);
        }

        public ActionResult ScrollLoadActors(int skip)
        {
            ActorIndexVM vm = new ActorIndexVM
            {
                Actors = db.Actors.OrderBy(a => a.Name).Skip(skip).Take(12),
                Actor_of_Film = db.Film_to_Actor
            };

            return PartialView("_PartialLoadActor", vm);
        }

        public ActionResult LiveSearchActor(string query)
        {
            ActorIndexVM vm = new ActorIndexVM
            {
                Actors = db.Actors.Where(a => a.Name.ToLower().Contains(query.ToLower())).Take(1),
                Actor_of_Film = db.Film_to_Actor
            };
            return PartialView("_PartialLiveSearchActors", vm);
        }

        [HttpPost]
        public ActionResult Search(string text)
        {
            AllFilmsVM vm = new AllFilmsVM
            {
                Films = db.Film_to_Actor.Where(a=>a.Actor.Name.ToLower().Contains(text.ToLower())).Select(f=>f.Film).Distinct(),
                Genres_of_Film = db.Film_to_Genre,
                Country_of_Film = db.Film_to_Country,
                Categories = db.Categories,
                Genres = db.Genres,
                Dublaj = db.Dublajs
            };

            return View(vm);
        }
    }
}