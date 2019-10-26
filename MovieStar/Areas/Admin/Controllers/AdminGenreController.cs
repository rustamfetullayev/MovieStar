using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MovieStar.Models;
using System.Data.Entity;
using System.Threading.Tasks;

namespace MovieStar.Areas.Admin.Controllers
{
    [AuthorizeAdmin]
    public class AdminGenreController : Controller
    {
        private readonly MSEntities db = new MSEntities();

        // GET: Admin/AdminGenre
        public ActionResult Index()
        {
            return View(db.Genres.OrderBy(c => c.Name).ToList());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Genre genre)
        {
            if (ModelState.IsValid)
            {
                if (db.Genres.FirstOrDefault(c => c.Name.ToLower() == genre.Name.ToLower()) == null)
                {
                    db.Genres.Add(genre);
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.GenreError = "This genre already exist";
                    return View();
                }
            }

            return View();
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Genre genre = db.Genres.Find(id);

            if (genre == null)
            {
                return HttpNotFound();
            }

            return View(genre);
        }

        [HttpPost]
        public ActionResult Edit(Genre genre)
        {
            if (ModelState.IsValid)
            {
                if (db.Genres.FirstOrDefault(c => c.Name.ToLower() == genre.Name.ToLower()) == null)
                {
                    db.Entry(genre).State = EntityState.Modified;
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.GenreError = "This genre already exist";
                    return View();
                }
            }

            return View();
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Genre genre = db.Genres.Find(id);

            if (genre == null)
            {
                return HttpNotFound();
            }

            return View(genre);
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirm(int id)
        {
            Genre genre = db.Genres.Find(id);

            Task removegenre = Task.Run(() =>
            {
                foreach (var item in db.Film_to_Genre.Where(a => a.GenreID == id))
                {
                    db.Film_to_Genre.Remove(item);
                }

                db.SaveChanges();
            });

            await removegenre;

            db.Genres.Remove(genre);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}