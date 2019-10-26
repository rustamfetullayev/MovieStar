using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MovieStar.Extensions;
using System.Web.Mvc;
using MovieStar.Models;
using System.Threading.Tasks;
using System.Data.Entity;

namespace MovieStar.Areas.Admin.Controllers
{
    [AuthorizeAdmin]
    public class AdminNewsController : Controller
    {
        private readonly MSEntities db = new MSEntities();

        // GET: Admin/AdminNews
        public ActionResult Index()
        {
            return View(db.News.OrderByDescending(n=>n.PostDate).ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            News news = db.News.Find(id);

            if (news == null)
            {
                return HttpNotFound();
            }

            return View(news);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create([Bind(Exclude = "Image_1,Image_2,Image_3,Image_4,Image_5")]News news,HttpPostedFileBase Image_1, HttpPostedFileBase Image_2, HttpPostedFileBase Image_3, 
            HttpPostedFileBase Image_4, HttpPostedFileBase Image_5)
        {
            if (ModelState.IsValid)
            {
                if(Extension.CheckImageType(Image_1) && Extension.CheckImageType(Image_2) && Extension.CheckImageType(Image_3)
                    && Extension.CheckImageType(Image_4) && Extension.CheckImageType(Image_5))
                {
                    news.Image_1 = Extension.SaveImage(Server.MapPath("~/Source/images"), Image_1);
                    news.Image_2 = Extension.SaveImage(Server.MapPath("~/Source/images"), Image_2);
                    news.Image_3 = Extension.SaveImage(Server.MapPath("~/Source/images"), Image_3);
                    news.Image_4 = Extension.SaveImage(Server.MapPath("~/Source/images"), Image_4);
                    news.Image_5 = Extension.SaveImage(Server.MapPath("~/Source/images"), Image_5);

                    db.News.Add(news);
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("Image_1", "Image type is not valid.");
                    ModelState.AddModelError("Image_2", "Image type is not valid.");
                    ModelState.AddModelError("Image_3", "Image type is not valid.");
                    ModelState.AddModelError("Image_4", "Image type is not valid.");
                    ModelState.AddModelError("Image_5", "Image type is not valid.");
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

            News news = db.News.Find(id);

            if (news == null)
            {
                return HttpNotFound();
            }

            return View(news);
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirm(int id)
        {
            News news = db.News.Find(id);

            Task deletenews = Task.Run(() =>
            {
                foreach (var item in db.NewsComments.Where(c=>c.NewsID==news.ID))
                {
                    db.NewsComments.Remove(item);
                }
            });

            await deletenews;

            db.News.Remove(news);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            News news = db.News.Find(id);

            if (news == null)
            {
                return HttpNotFound();
            }

            return View(news);
        }

        [HttpPost]
        public ActionResult Edit(News news, HttpPostedFileBase NewImage_1, HttpPostedFileBase NewImage_2, HttpPostedFileBase NewImage_3,
            HttpPostedFileBase NewImage_4, HttpPostedFileBase NewImage_5)
        {
            if (ModelState.IsValid)
            {
                if (NewImage_1 != null)
                {
                    if (Extension.CheckImageType(NewImage_1))
                    {
                        Extension.DeleteImage(Server.MapPath("~/Source/images"), news.Image_1);

                        news.Image_1 = Extension.SaveImage(Server.MapPath("~/Source/images"), NewImage_1);
                    }
                    else
                    {
                        ModelState.AddModelError("NewImage_1", "Type of image is not valid.");
                    }
                }

                if (NewImage_2 != null)
                {
                    if (Extension.CheckImageType(NewImage_2))
                    {
                        Extension.DeleteImage(Server.MapPath("~/Source/images"), news.Image_2);

                        news.Image_2 = Extension.SaveImage(Server.MapPath("~/Source/images"), NewImage_2);
                    }
                    else
                    {
                        ModelState.AddModelError("NewImage_2", "Type of image is not valid.");
                    }
                }

                if (NewImage_3 != null)
                {
                    if (Extension.CheckImageType(NewImage_3))
                    {
                        Extension.DeleteImage(Server.MapPath("~/Source/images"), news.Image_3);

                        news.Image_3 = Extension.SaveImage(Server.MapPath("~/Source/images"), NewImage_3);
                    }
                    else
                    {
                        ModelState.AddModelError("NewImage_3", "Type of image is not valid.");
                    }
                }

                if (NewImage_4 != null)
                {
                    if (Extension.CheckImageType(NewImage_4))
                    {
                        Extension.DeleteImage(Server.MapPath("~/Source/images"), news.Image_4);

                        news.Image_4 = Extension.SaveImage(Server.MapPath("~/Source/images"), NewImage_4);
                    }
                    else
                    {
                        ModelState.AddModelError("NewImage_4", "Type of image is not valid.");
                    }
                }

                if (NewImage_5 != null)
                {
                    if (Extension.CheckImageType(NewImage_5))
                    {
                        Extension.DeleteImage(Server.MapPath("~/Source/images"), news.Image_5);

                        news.Image_5 = Extension.SaveImage(Server.MapPath("~/Source/images"), NewImage_5);
                    }
                    else
                    {
                        ModelState.AddModelError("NewImage_5", "Type of image is not valid.");
                    }
                }

                db.Entry(news).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Edit",new { id=news.ID });
            }

            return View();
        }
    }
}