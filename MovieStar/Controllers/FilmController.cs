using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using MovieStar.Models;
using MovieStar.ViewModels;

namespace MovieStar.Controllers
{
    public class FilmController : Controller
    {
        private readonly MSEntities db = new MSEntities();

        // GET: Film
        public ActionResult Index()
        {
            AllFilmsVM vm = new AllFilmsVM
            {
                Films = db.Films.OrderByDescending(f => f.UpdateDate).Take(3),
                Genres_of_Film = db.Film_to_Genre,
                Country_of_Film = db.Film_to_Country,
                Categories = db.Categories,
                Genres = db.Genres,
                Dublaj = db.Dublajs,
                SavedFilms=db.SavedFilms
            };
            return View(vm);
        }

        public ActionResult ScrollLoadFilms(int skip)
        {
            AllFilmsVM vm = new AllFilmsVM
            {
                Films = db.Films.OrderByDescending(f => f.UpdateDate).Skip(skip).Take(3),
                Genres_of_Film = db.Film_to_Genre,
                Country_of_Film = db.Film_to_Country,
                Categories = db.Categories,
                Genres = db.Genres,
                Dublaj = db.Dublajs,
                SavedFilms = db.SavedFilms
            };

            return PartialView("_PartialLoadFilms", vm);
        }

        public ActionResult Single(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Film film = db.Films.Find(id);

            if (film == null)
            {
                return HttpNotFound();
            }

            film.ViewCount++;
            db.SaveChanges();

            SingleFilmVM vm = new SingleFilmVM
            {
                Film = film,
                Genres_of_Film = db.Film_to_Genre.Where(g => g.FilmID == film.ID),
                Country_of_Film = db.Film_to_Country.Where(c => c.FilmID == film.ID),
                Director_of_Film = db.Film_to_Director.Where(d => d.FilmID == film.ID),
                Actor_of_Film=db.Film_to_Actor.Where(a=>a.FilmID==film.ID),
                Comments=db.Comments.Where(c=>c.FilmID==film.ID).OrderByDescending(a=>a.ComTime),
                LikedFilms=db.LikedFilms,
                DislikedFilms=db.DislikedFilms
            };
            
            return View(vm);
        }

        public ActionResult AddComment(string text, int filmid)
        {
            User currentUser = Session["user"] as User;
            Comment newcom = new Comment
            {
                UserID = currentUser.ID,
                FilmID = filmid,
                Content = text,
                ComTime = DateTime.Now,
            };

            db.Comments.Add(newcom);
            db.SaveChanges();
            newcom.User = currentUser;
            return PartialView("_PartialComment", newcom);
        }

        public ActionResult LiveSearch(string query)
        {
            AllFilmsVM vm = new AllFilmsVM
            {
                Films = db.Films.Where(f=>f.Name.ToLower().Contains(query.ToLower())).Take(5),
                Genres_of_Film = db.Film_to_Genre,
                Country_of_Film = db.Film_to_Country,
            };

            return PartialView("_PartialLiveSearchFilms",vm);
        }

        [HttpPost]
        public ActionResult Search(string text)
        {
            AllFilmsVM vm = new AllFilmsVM
            {
                Films = db.Films.Where(f => f.Name.ToLower().Contains(text.ToLower())).OrderByDescending(f => f.UpdateDate),
                Genres_of_Film = db.Film_to_Genre,
                Country_of_Film = db.Film_to_Country,
                Categories = db.Categories,
                Genres = db.Genres,
                Dublaj = db.Dublajs
            };

            return View(vm);
        }

        public ActionResult Genre(string genre)
        {
            ViewBag.Genre = genre;
            
            AllFilmsVM vm = new AllFilmsVM
            {
                Films = db.Film_to_Genre.Where(g=>g.Genre.Name==genre).Select(f=>f.Film).OrderByDescending(f => f.UpdateDate).Take(3),
                Genres_of_Film = db.Film_to_Genre,
                Country_of_Film = db.Film_to_Country,
                Categories = db.Categories,
                Genres = db.Genres,
                Dublaj = db.Dublajs
            };

            return View(vm);
        }

        public ActionResult ScrollLoadFilmsGenre(int skip,string genre)
        {
            AllFilmsVM vm = new AllFilmsVM
            {
                Films = db.Film_to_Genre.Where(g => g.Genre.Name == genre).Select(f => f.Film).OrderByDescending(f => f.UpdateDate).Skip(skip).Take(3),
                Genres_of_Film = db.Film_to_Genre,
                Country_of_Film = db.Film_to_Country,
                Categories = db.Categories,
                Genres = db.Genres,
                Dublaj = db.Dublajs
            };

            return PartialView("_PartialLoadFilms", vm);
        }

        public ActionResult Category(string category)
        {
            ViewBag.Category = category;

            AllFilmsVM vm = new AllFilmsVM
            {
                Films = db.Films.Where(f=>f.Category.Name==category).OrderByDescending(f => f.UpdateDate).Take(3),
                Genres_of_Film = db.Film_to_Genre,
                Country_of_Film = db.Film_to_Country,
                Categories = db.Categories,
                Genres = db.Genres,
                Dublaj = db.Dublajs
            };

            return View(vm);
        }

        public ActionResult ScrollLoadFilmsCategory(int skip, string category)
        {
            AllFilmsVM vm = new AllFilmsVM
            {
                Films = db.Films.Where(f => f.Category.Name == category).OrderByDescending(f => f.UpdateDate).Skip(skip).Take(3),
                Genres_of_Film = db.Film_to_Genre,
                Country_of_Film = db.Film_to_Country,
                Categories = db.Categories,
                Genres = db.Genres,
                Dublaj = db.Dublajs
            };

            return PartialView("_PartialLoadFilms", vm);
        }

        public ActionResult Dublaj(string dublaj)
        {
            ViewBag.Dublaj = dublaj;

            AllFilmsVM vm = new AllFilmsVM
            {
                Films = db.Films.Where(f=>f.Dublaj.Name==dublaj).OrderByDescending(f => f.UpdateDate).Take(3),
                Genres_of_Film = db.Film_to_Genre,
                Country_of_Film = db.Film_to_Country,
                Categories = db.Categories,
                Genres = db.Genres,
                Dublaj = db.Dublajs
            };

            return View(vm);
        }

        public ActionResult ScrollLoadFilmsDublaj(int skip, string dublaj)
        {
            AllFilmsVM vm = new AllFilmsVM
            {
                Films = db.Films.Where(f => f.Dublaj.Name == dublaj).OrderByDescending(f => f.UpdateDate).Skip(skip).Take(3),
                Genres_of_Film = db.Film_to_Genre,
                Country_of_Film = db.Film_to_Country,
                Categories = db.Categories,
                Genres = db.Genres,
                Dublaj = db.Dublajs
            };

            return PartialView("_PartialLoadFilms", vm);
        }

        public ActionResult Actor(string actor)
        {
            ViewBag.Actor = actor;

            AllFilmsVM vm = new AllFilmsVM
            {
                Films = db.Film_to_Actor.Where(a=>a.Actor.Name==actor).Select(f=>f.Film).OrderByDescending(f => f.UpdateDate).Take(3),
                Genres_of_Film = db.Film_to_Genre,
                Country_of_Film = db.Film_to_Country,
                Categories = db.Categories,
                Genres = db.Genres,
                Dublaj = db.Dublajs
            };

            return View(vm);
        }

        public ActionResult ScrollLoadFilmsActor(int skip, string actor)
        {
            AllFilmsVM vm = new AllFilmsVM
            {
                Films = db.Film_to_Actor.Where(g => g.Actor.Name == actor).Select(f => f.Film).OrderByDescending(f => f.UpdateDate).Skip(skip).Take(3),
                Genres_of_Film = db.Film_to_Genre,
                Country_of_Film = db.Film_to_Country,
                Categories = db.Categories,
                Genres = db.Genres,
                Dublaj = db.Dublajs
            };

            return PartialView("_PartialLoadFilms", vm);
        }

        public ActionResult Director(string director)
        {
            ViewBag.Director = director;

            AllFilmsVM vm = new AllFilmsVM
            {
                Films = db.Film_to_Director.Where(d=>d.Director.Name==director).Select(f=>f.Film).OrderByDescending(f => f.UpdateDate).Take(3),
                Genres_of_Film = db.Film_to_Genre,
                Country_of_Film = db.Film_to_Country,
                Categories = db.Categories,
                Genres = db.Genres,
                Dublaj = db.Dublajs
            };

            return View(vm);
        }

        public ActionResult ScrollLoadFilmsDirector(int skip, string director)
        {
            AllFilmsVM vm = new AllFilmsVM
            {
                Films = db.Film_to_Director.Where(g => g.Director.Name == director).Select(f => f.Film).OrderByDescending(f => f.UpdateDate).Skip(skip).Take(3),
                Genres_of_Film = db.Film_to_Genre,
                Country_of_Film = db.Film_to_Country,
                Categories = db.Categories,
                Genres = db.Genres,
                Dublaj = db.Dublajs
            };

            return PartialView("_PartialLoadFilms", vm);
        }

        public ActionResult Country(string country)
        {
            ViewBag.Country = country;

            AllFilmsVM vm = new AllFilmsVM
            {
                Films = db.Film_to_Country.Where(d=>d.Country.Name== country).Select(f=>f.Film).OrderByDescending(f => f.UpdateDate).Take(3),
                Genres_of_Film = db.Film_to_Genre,
                Country_of_Film = db.Film_to_Country,
                Categories = db.Categories,
                Genres = db.Genres,
                Dublaj = db.Dublajs
            };

            return View(vm);
        }

        public ActionResult ScrollLoadFilmsCountry(int skip, string country)
        {
            AllFilmsVM vm = new AllFilmsVM
            {
                Films = db.Film_to_Country.Where(g => g.Country.Name == country).Select(f => f.Film).OrderByDescending(f => f.UpdateDate).Skip(skip).Take(3),
                Genres_of_Film = db.Film_to_Genre,
                Country_of_Film = db.Film_to_Country,
                Categories = db.Categories,
                Genres = db.Genres,
                Dublaj = db.Dublajs
            };

            return PartialView("_PartialLoadFilms", vm);
        }

        public ActionResult IMDB()
        {
            AllFilmsVM vm = new AllFilmsVM
            {
                Films = db.Films.OrderByDescending(f => f.IMDB).Take(3),
                Genres_of_Film = db.Film_to_Genre,
                Country_of_Film = db.Film_to_Country,
                Categories = db.Categories,
                Genres = db.Genres,
                Dublaj = db.Dublajs
            };

            return View(vm);

        }

        public ActionResult ScrollLoadFilmsIMDB(int skip)
        {
            AllFilmsVM vm = new AllFilmsVM
            {
                Films = db.Films.OrderByDescending(f => f.IMDB).Skip(skip).Take(3),
                Genres_of_Film = db.Film_to_Genre,
                Country_of_Film = db.Film_to_Country,
                Categories = db.Categories,
                Genres = db.Genres,
                Dublaj = db.Dublajs
            };

            return PartialView("_PartialLoadFilms", vm);
        }

        public ActionResult Year()
        {
            AllFilmsVM vm = new AllFilmsVM
            {
                Films = db.Films.OrderByDescending(f => f.Year).Take(3),
                Genres_of_Film = db.Film_to_Genre,
                Country_of_Film = db.Film_to_Country,
                Categories = db.Categories,
                Genres = db.Genres,
                Dublaj = db.Dublajs
            };

            return View(vm);

        }

        public ActionResult ScrollLoadFilmsYear(int skip)
        {
            AllFilmsVM vm = new AllFilmsVM
            {
                Films = db.Films.OrderByDescending(f => f.Year).Skip(skip).Take(3),
                Genres_of_Film = db.Film_to_Genre,
                Country_of_Film = db.Film_to_Country,
                Categories = db.Categories,
                Genres = db.Genres,
                Dublaj = db.Dublajs
            };

            return PartialView("_PartialLoadFilms", vm);
        }
        
        public int SaveFilm(int filmid)
        {
            User currentuser = Session["user"] as User;
            SavedFilm saved = db.SavedFilms.FirstOrDefault(s => s.FilmID == filmid && s.UserID == currentuser.ID);

            if (saved == null)
            {
                SavedFilm sv = new SavedFilm
                {
                    UserID = currentuser.ID,
                    FilmID = filmid,
                    SaveTime = DateTime.Now
                };
                db.SavedFilms.Add(sv);
                db.SaveChanges();

                return 1;
            }
            else
            {
                db.SavedFilms.Remove(saved);
                db.SaveChanges();

                return 0;
            }
        }

        public int LikeFilm(int filmid)
        {
            User currentuser = Session["user"] as User;

            LikedFilm liked = db.LikedFilms.FirstOrDefault(l => l.FilmID == filmid && l.UserID == currentuser.ID);

            if (liked == null)
            {
                DislikedFilm disliked = db.DislikedFilms.FirstOrDefault(l => l.FilmID == filmid && l.UserID == currentuser.ID);

                if (disliked != null)
                {
                    LikedFilm ldf = new LikedFilm
                    {
                        FilmID = filmid,
                        UserID = currentuser.ID,
                        LikeTime = DateTime.Now
                    };

                    db.LikedFilms.Add(ldf);
                    db.DislikedFilms.Remove(disliked);
                    db.SaveChanges();

                    return 2;
                }


                LikedFilm lf = new LikedFilm
                {
                    FilmID = filmid,
                    UserID = currentuser.ID,
                    LikeTime = DateTime.Now
                };

                db.LikedFilms.Add(lf);
                db.SaveChanges();

                return 1;
            }
            else
            {
                db.LikedFilms.Remove(liked);
                db.SaveChanges();

                return 0;
            }
        }

        public int DislikeFilm(int filmid)
        {
            User currentuser = Session["user"] as User;

            DislikedFilm disliked = db.DislikedFilms.FirstOrDefault(l => l.FilmID == filmid && l.UserID == currentuser.ID);

            if (disliked == null)
            {
                LikedFilm liked = db.LikedFilms.FirstOrDefault(l => l.FilmID == filmid && l.UserID == currentuser.ID);

                if (liked != null)
                {
                    DislikedFilm ldf = new DislikedFilm
                    {
                        FilmID = filmid,
                        UserID = currentuser.ID,
                        DisLikeTime = DateTime.Now
                    };

                    db.DislikedFilms.Add(ldf);
                    db.LikedFilms.Remove(liked);
                    db.SaveChanges();

                    return 2;
                }


                DislikedFilm df = new DislikedFilm
                {
                    FilmID = filmid,
                    UserID = currentuser.ID,
                    DisLikeTime = DateTime.Now
                };

                db.DislikedFilms.Add(df);
                db.SaveChanges();

                return 1;
            }
            else
            {
                db.DislikedFilms.Remove(disliked);
                db.SaveChanges();

                return 0;
            }
        }

    }
}