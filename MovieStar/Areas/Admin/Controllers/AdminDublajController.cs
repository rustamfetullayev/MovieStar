using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MovieStar.Models;
using System.Data.Entity;

namespace MovieStar.Areas.Admin.Controllers
{
    [AuthorizeAdmin]
    public class AdminDublajController : Controller
    {
        private readonly MSEntities db = new MSEntities();

        // GET: Admin/AdminDublaj
        public ActionResult Index()
        {
            return View(db.Dublajs.OrderBy(d=>d.Name).ToList());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Dublaj dublaj)
        {
            if (ModelState.IsValid)
            {
                if (db.Dublajs.FirstOrDefault(d => d.Name.ToLower() == dublaj.Name.ToLower()) == null)
                {
                    db.Dublajs.Add(dublaj);
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.DublajError = "This dublaj already exist.";
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

            Dublaj dublaj = db.Dublajs.Find(id);

            if(dublaj == null)
            {
                return HttpNotFound();
            }

            return View(dublaj);
        }

        [HttpPost]
        public ActionResult Edit(Dublaj dublaj)
        {
            if (ModelState.IsValid)
            {
                if (db.Dublajs.FirstOrDefault(d => d.Name.ToLower() == dublaj.Name.ToLower()) == null)
                {
                    db.Entry(dublaj).State = EntityState.Modified;
                    db.SaveChanges();
                }
                else
                {
                    ViewBag.DublajError = "This dublaj already exist.";
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

            Dublaj dublaj = db.Dublajs.Find(id);

            if (dublaj == null)
            {
                return HttpNotFound();
            }

            return View(dublaj);
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirm(int id)
        {
            Dublaj dublaj = db.Dublajs.Find(id);

            db.Dublajs.Remove(dublaj);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}