using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MovieStar.Models;
using System.Data.Entity;
using MovieStar.Extensions;
using System.Threading.Tasks;

namespace MovieStar.Areas.Admin.Controllers
{
    [AuthorizeAdmin]
    public class AdminActorController : Controller
    {
        private readonly MSEntities db = new MSEntities();

        // GET: Admin/AdminActor
        public ActionResult Index()
        {
            return View(db.Actors.OrderBy(a=>a.Name).ToList());
        }

        public ActionResult Search(string text)
        {
            return PartialView("_PartialActorSearch", db.Actors.Where(a => a.Name.Contains(text)).OrderBy(a => a.Name));
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create([Bind(Exclude = "Image")]Actor actor,HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {
                if (Extension.CheckImageType(Image))
                {
                    actor.Image = Extension.SaveImage(Server.MapPath("~/Source/images"), Image);

                    db.Actors.Add(actor);
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("Image", "Type of picture is not valid.");
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

            Actor actor = db.Actors.Find(id);

            if (actor == null)
            {
                return HttpNotFound();
            }

            return View(actor);
        }

        [HttpPost]
        public ActionResult Edit(Actor actor, HttpPostedFileBase NewImage)
        {
            if (ModelState.IsValid)
            {
                if (NewImage != null)
                {
                    if (Extension.CheckImageType(NewImage))
                    {
                        Extension.DeleteImage(Server.MapPath("~/Source/images"), actor.Image);

                        actor.Image = Extension.SaveImage(Server.MapPath("~/Source/images"), NewImage);
                    }
                    else
                    {
                        ModelState.AddModelError("NewImage", "Image type is not valid.");
                    }
                }
                
                db.Entry(actor).State = EntityState.Modified;
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

            Actor actor = db.Actors.Find(id);

            if (actor == null)
            {
                return HttpNotFound();
            }

            return View(actor);
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirm(int id)
        {
            Actor actor = db.Actors.Find(id);

            Task removeactor = Task.Run(() =>
            {
                foreach (var item in db.Film_to_Actor.Where(a=>a.ActorID==id))
                {
                    db.Film_to_Actor.Remove(item);
                }

                db.SaveChanges();
            });

            await removeactor;

            db.Actors.Remove(actor);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}