using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MovieStar.Models;
using MovieStar.ViewModels;

namespace MovieStar.Controllers
{
    public class NewsController : Controller
    {
        private readonly MSEntities db = new MSEntities();

        // GET: News
        public ActionResult Index()
        {
            return View(db.News.OrderByDescending(n => n.PostDate).Take(3).ToList());
        }

        public ActionResult ScrollLoadNews(int skip)
        {
            return PartialView("_PartialLoadNews", db.News.OrderByDescending(n => n.PostDate).Skip(skip).Take(3).ToList());
        }

        public ActionResult Single(int? id)
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

            SingleNewsVM vm = new SingleNewsVM
            {
                News = news,
                Comments = db.NewsComments.Where(c => c.NewsID == news.ID).OrderByDescending(c=>c.ComTime)
            };

            return View(vm);
        }

        public ActionResult AddComment(string text, int newsid)
        {
            User currentUser = Session["user"] as User;
            NewsComment newcom = new NewsComment
            {
                UserID = currentUser.ID,
                NewsID = newsid,
                Content = text,
                ComTime = DateTime.Now,
            };

            db.NewsComments.Add(newcom);
            db.SaveChanges();
            newcom.User = currentUser;
            return PartialView("_PartialNewsComment", newcom);
        }
    }
}