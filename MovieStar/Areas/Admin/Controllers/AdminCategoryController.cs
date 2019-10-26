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
    public class AdminCategoryController : Controller
    {
        private readonly MSEntities db = new MSEntities();

        // GET: Admin/AdminCategory
        public ActionResult Index()
        {
            return View(db.Categories.OrderBy(c=>c.Name).ToList());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                if(db.Categories.FirstOrDefault(c=>c.Name.Trim().ToLower() == category.Name.Trim().ToLower()) == null)
                {
                    db.Categories.Add(category);
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.CategoryError = "This category already exist";
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

            Category category = db.Categories.Find(id);

            if (category == null)
            {
                return HttpNotFound();
            }

            return View(category);
        }

        [HttpPost]
        public ActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                if (db.Categories.FirstOrDefault(c => c.Name.Trim().ToLower() == category.Name.Trim().ToLower()) == null)
                {
                    db.Entry(category).State = EntityState.Modified;
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.CategoryError = "This category already exist";
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

            Category category = db.Categories.Find(id);

            if (category == null)
            {
                return HttpNotFound();
            }

            return View(category);
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirm(int id)
        {
            Category category = db.Categories.Find(id);

            db.Categories.Remove(category);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}