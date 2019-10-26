using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MovieStar.Models;
using MovieStar.Extensions;
using System.Threading.Tasks;
using System.Data.Entity;

namespace MovieStar.Areas.Admin.Controllers
{
    [AuthorizeAdmin]
    public class AdminFilmController : Controller
    {
        private readonly MSEntities db = new MSEntities();

        // GET: Admin/AdminFilm
        public ActionResult Index()
        {
            return View(db.Films.OrderByDescending(f=>f.UpdateDate).ToList());
        }

        public ActionResult Search(string text)
        {
            return PartialView("_PartialFilmSearch", db.Films.Where(f => f.Name.Contains(text)).OrderByDescending(f=>f.UpdateDate));
        }

        public ActionResult Details(int? id)
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

            return View(film);
        }

        public ActionResult Create()
        {
            ViewBag.Genres = new SelectList(db.Genres, "ID", "Name");
            //ViewBag.Actors = new SelectList(db.Actors.OrderBy(a => a.Name), "ID", "Name");
            ViewBag.Actors = db.Actors.OrderBy(a => a.Name).ToList();
            ViewBag.Directors = new SelectList(db.Directors.OrderBy(d=>d.Name), "ID", "Name");
            ViewBag.Dublaj = new SelectList(db.Dublajs, "ID", "Name");
            ViewBag.Categories = new SelectList(db.Categories, "ID", "Name");
            ViewBag.Countries = new SelectList(db.Countries.OrderBy(c=>c.Name), "ID", "Name");

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create([Bind(Exclude = "Main_Img,Cover_Img,Iframe_Cover_Img,Video_Img")]Film film,
            HttpPostedFileBase Main_Img, HttpPostedFileBase Cover_Img, HttpPostedFileBase Iframe_Cover_Img, HttpPostedFileBase Video_Img,
            IEnumerable<int> Actors,IEnumerable<int> Directors,IEnumerable<int> Genres,IEnumerable<int> Countries)
        {
            if (ModelState.IsValid)
            {
                if (Extension.CheckImageType(Main_Img) && Extension.CheckImageType(Cover_Img) && Extension.CheckImageType(Iframe_Cover_Img)
                    && Extension.CheckImageType(Video_Img))
                {
                    film.Main_Img = Extension.SaveImage(Server.MapPath("~/Source/images"), Main_Img);
                    film.Cover_Img = Extension.SaveImage(Server.MapPath("~/Source/images"), Cover_Img);
                    film.Iframe_Cover_Img = Extension.SaveImage(Server.MapPath("~/Source/images"), Iframe_Cover_Img);
                    film.Video_Img = Extension.SaveImage(Server.MapPath("~/Source/images"), Video_Img);

                    film.UpdateDate = DateTime.Now;
                    film.ViewCount = 0;

                    Film newfilm = null;

                    Task addnewfilm = Task.Run(() =>
                    {
                        newfilm = db.Films.Add(film);
                        db.SaveChanges();
                    });

                    await addnewfilm;

                    foreach (var genID in Genres)
                    {
                        db.Film_to_Genre.Add(new Film_to_Genre
                        {
                            FilmID = newfilm.ID,
                            GenreID = genID
                        });
                        db.SaveChanges();
                    }

                    foreach (var actID in Actors)
                    {
                        db.Film_to_Actor.Add(new Film_to_Actor
                        {
                            FilmID = newfilm.ID,
                            ActorID = actID
                        });
                        db.SaveChanges();
                    }

                    foreach (var dirID in Directors)
                    {
                        db.Film_to_Director.Add(new Film_to_Director
                        {
                            FilmID = newfilm.ID,
                            DirectorID = dirID
                        });
                        db.SaveChanges();
                    }

                    foreach (var coID in Countries)
                    {
                        db.Film_to_Country.Add(new Film_to_Country
                        {
                            FilmID = newfilm.ID,
                            CountryID = coID
                        });
                        db.SaveChanges();
                    }

                    return RedirectToAction("Index");
                }

                ModelState.AddModelError("Main_Img", "Image type is not valid.");
                ModelState.AddModelError("Cover_Img", "Image type is not valid.");
                ModelState.AddModelError("Iframe_Cover_Img", "Image type is not valid.");
                ModelState.AddModelError("Video_Img", "Image type is not valid.");
            }

            ViewBag.Genres = new SelectList(db.Genres, "ID", "Name");
            //ViewBag.Actors = new SelectList(db.Actors.OrderBy(a => a.Name), "ID", "Name");
            ViewBag.Actors = db.Actors.OrderBy(a=>a.Name).ToList();
            ViewBag.Directors = new SelectList(db.Directors.OrderBy(d => d.Name), "ID", "Name");
            ViewBag.Dublaj = new SelectList(db.Dublajs, "ID", "Name");
            ViewBag.Categories = new SelectList(db.Categories, "ID", "Name");
            ViewBag.Countries = new SelectList(db.Countries.OrderBy(c=>c.Name), "ID", "Name");

            return View();
        }


        public ActionResult Delete(int? id)
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

            return View(film);
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirm(int id)
        {
            Film film = db.Films.Find(id);


            Task removefilm = Task.Run(() =>
            {
                foreach (var item in db.Film_to_Actor.Where(a => a.FilmID == id))
                {
                    db.Film_to_Actor.Remove(item);
                }

                foreach (var item in db.Film_to_Country.Where(c=>c.FilmID == id))
                {
                    db.Film_to_Country.Remove(item);
                }

                foreach (var item in db.Film_to_Genre.Where(g => g.FilmID == id))
                {
                    db.Film_to_Genre.Remove(item);
                }

                foreach (var item in db.Film_to_Director.Where(d => d.FilmID == id))
                {
                    db.Film_to_Director.Remove(item);
                }

                foreach (var item in db.SavedFilms.Where(d => d.FilmID == id))
                {
                    db.SavedFilms.Remove(item);
                }

                foreach (var item in db.LikedFilms.Where(d => d.FilmID == id))
                {
                    db.LikedFilms.Remove(item);
                }

                foreach (var item in db.DislikedFilms.Where(d => d.FilmID == id))
                {
                    db.DislikedFilms.Remove(item);
                }

                foreach (var item in db.Comments.Where(c=>c.FilmID == id))
                {
                    db.Comments.Remove(item);
                }

                db.SaveChanges();
            });

            await removefilm;

            db.Films.Remove(film);
            db.SaveChanges();

            return RedirectToAction("Index");
        }


        public ActionResult Edit(int? id)
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

            ViewBag.Actors = db.Actors.OrderBy(a => a.Name).ToList();
            ViewBag.Directors = db.Directors.OrderBy(a => a.Name).ToList();
            ViewBag.Genres = db.Genres.ToList();
            ViewBag.Categories = db.Categories.ToList();
            ViewBag.Dublajs = db.Dublajs.ToList();
            ViewBag.Countries = db.Countries.OrderBy(c => c.Name).ToList();

            return View(film);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(Film film, int viewCount, HttpPostedFileBase New_Main_Img, HttpPostedFileBase New_Cover_Img, 
            HttpPostedFileBase New_Iframe_Cover_Img, HttpPostedFileBase New_Video_Img,
            IEnumerable<int> Actors, IEnumerable<int> Directors, IEnumerable<int> Genres, IEnumerable<int> Countries)
        {
            if (ModelState.IsValid)
            {
                if (New_Main_Img != null)
                {
                    if (Extension.CheckImageType(New_Main_Img))
                    {
                        Extension.DeleteImage(Server.MapPath("~/Source/images"), film.Main_Img);
                        film.Main_Img = Extension.SaveImage(Server.MapPath("~/Source/images"), New_Main_Img);
                    }
                    else
                    {
                        ModelState.AddModelError("New_Main_Img", "Image type is not valid.");
                    }
                }

                if (New_Cover_Img != null)
                {
                    if (Extension.CheckImageType(New_Cover_Img))
                    {
                        Extension.DeleteImage(Server.MapPath("~/Source/images"), film.Cover_Img);
                        film.Cover_Img = Extension.SaveImage(Server.MapPath("~/Source/images"), New_Cover_Img);
                    }
                    else
                    {
                        ModelState.AddModelError("New_Cover_Img", "Image type is not valid.");
                    }
                }

                if (New_Iframe_Cover_Img != null)
                {
                    if (Extension.CheckImageType(New_Iframe_Cover_Img))
                    {
                        Extension.DeleteImage(Server.MapPath("~/Source/images"), film.Iframe_Cover_Img);
                        film.Iframe_Cover_Img = Extension.SaveImage(Server.MapPath("~/Source/images"), New_Iframe_Cover_Img);
                    }
                    else
                    {
                        ModelState.AddModelError("New_Iframe_Cover_Img", "Image type is not valid.");
                    }
                }

                if (New_Video_Img != null)
                {
                    if (Extension.CheckImageType(New_Video_Img))
                    {
                        Extension.DeleteImage(Server.MapPath("~/Source/images"), film.Video_Img);
                        film.Video_Img = Extension.SaveImage(Server.MapPath("~/Source/images"), New_Video_Img);
                    }
                    else
                    {
                        ModelState.AddModelError("New_Video_Img", "Image type is not valid.");
                    }
                }

                Task removefilm = Task.Run(() =>
                {
                    foreach (var item in db.Film_to_Actor.Where(a => a.FilmID == film.ID))
                    {
                        db.Film_to_Actor.Remove(item);
                    }

                    foreach (var item in db.Film_to_Country.Where(c => c.FilmID == film.ID))
                    {
                        db.Film_to_Country.Remove(item);
                    }

                    foreach (var item in db.Film_to_Genre.Where(g => g.FilmID == film.ID))
                    {
                        db.Film_to_Genre.Remove(item);
                    }

                    foreach (var item in db.Film_to_Director.Where(d => d.FilmID == film.ID))
                    {
                        db.Film_to_Director.Remove(item);
                    }

                    db.SaveChanges();
                });

                await removefilm;

                foreach (var genID in Genres)
                {
                    db.Film_to_Genre.Add(new Film_to_Genre
                    {
                        FilmID = film.ID,
                        GenreID = genID
                    });
                    db.SaveChanges();
                }

                foreach (var actID in Actors)
                {
                    db.Film_to_Actor.Add(new Film_to_Actor
                    {
                        FilmID = film.ID,
                        ActorID = actID
                    });
                    db.SaveChanges();
                }

                foreach (var dirID in Directors)
                {
                    db.Film_to_Director.Add(new Film_to_Director
                    {
                        FilmID = film.ID,
                        DirectorID = dirID
                    });
                    db.SaveChanges();
                }

                foreach (var coID in Countries)
                {
                    db.Film_to_Country.Add(new Film_to_Country
                    {
                        FilmID = film.ID,
                        CountryID = coID
                    });
                    db.SaveChanges();
                }

                film.UpdateDate = DateTime.Now;
                film.ViewCount = viewCount;
                db.Entry(film).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.Actors = db.Actors.OrderBy(a => a.Name).ToList();
            ViewBag.Directors = db.Directors.OrderBy(a => a.Name).ToList();
            ViewBag.Genres = db.Genres.ToList();
            ViewBag.Categories = db.Categories.ToList();
            ViewBag.Dublajs = db.Dublajs.ToList();
            ViewBag.Countries = db.Countries.OrderBy(c => c.Name).ToList();

            return View(film);
        }
    }
}