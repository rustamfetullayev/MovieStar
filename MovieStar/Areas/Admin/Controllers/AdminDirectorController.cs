using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using MovieStar.Models;
using System.Threading.Tasks;

namespace MovieStar.Areas.Admin.Controllers
{
    [AuthorizeAdmin]
    public class AdminDirectorController : Controller
    {
        private readonly MSEntities db = new MSEntities();

        // GET: Admin/AdminDirector
        public ActionResult Index()
        {
            return View(db.Directors.OrderBy(d=>d.Name));
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Director director)
        {
            if (ModelState.IsValid)
            {
                db.Directors.Add(director);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View();
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Director director = db.Directors.Find(id);

            if (director == null)
            {
                return HttpNotFound();
            }

            return View(director);
        }

        [HttpPost]
        public ActionResult Edit(Director director)
        {
            if (ModelState.IsValid)
            {
                db.Entry(director).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View();
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Director director = db.Directors.Find(id);

            if (director == null)
            {
                return HttpNotFound();
            }

            return View(director);
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirm(int id)
        {
            Director director = db.Directors.Find(id);

            Task removedirector = Task.Run(() =>
            {
                foreach (var item in db.Film_to_Director.Where(a => a.DirectorID == id))
                {
                    db.Film_to_Director.Remove(item);
                }

                db.SaveChanges();
            });

            await removedirector;

            db.Directors.Remove(director);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}