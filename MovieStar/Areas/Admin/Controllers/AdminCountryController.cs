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
    public class AdminCountryController : Controller
    {
        private readonly MSEntities db = new MSEntities();

        // GET: Admin/AdminCountry
        public ActionResult Index()
        {
            return View(db.Countries.OrderBy(c => c.Name).ToList());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Country country)
        {
            if (ModelState.IsValid)
            {
                if (db.Countries.FirstOrDefault(c => c.Name.ToLower() == country.Name.ToLower()) == null)
                {
                    db.Countries.Add(country);
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.CountryError = "This country already exist";
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

            Country country = db.Countries.Find(id);

            if (country == null)
            {
                return HttpNotFound();
            }

            return View(country);
        }

        [HttpPost]
        public ActionResult Edit(Country country)
        {
            if (ModelState.IsValid)
            {
                if (db.Countries.FirstOrDefault(c => c.Name.ToLower() == country.Name.ToLower()) == null)
                {
                    db.Entry(country).State = EntityState.Modified;
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.CountryError = "This country already exist";
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

            Country country = db.Countries.Find(id);

            if (country == null)
            {
                return HttpNotFound();
            }

            return View(country);
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirm(int id)
        {
            Country country = db.Countries.Find(id);

            Task removecountry = Task.Run(() =>
            {
                foreach (var item in db.Film_to_Country.Where(a => a.CountryID == id))
                {
                    db.Film_to_Country.Remove(item);
                }

                db.SaveChanges();
            });

            await removecountry;

            db.Countries.Remove(country);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}