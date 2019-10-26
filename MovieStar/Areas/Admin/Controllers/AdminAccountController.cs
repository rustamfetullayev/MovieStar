using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Helpers;
using MovieStar.Models;

namespace MovieStar.Areas.Admin.Controllers
{
    public class AdminAccountController : Controller
    {
        private readonly MSEntities db = new MSEntities();

        // GET: Admin/AdminAccount
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            MovieStar.Models.Admin admin = db.Admins.FirstOrDefault(a => a.Username==username);

            if (admin == null)
            {
                ViewBag.LoginError = "Username or password is wrong.";
                return View();
            }

            if (!Crypto.VerifyHashedPassword(admin.Password, password))
            {
                ViewBag.LoginError = "Username or password is wrong.";
                return View();
            }

            Session["admin"] = admin;
            return RedirectToAction("Index", "AdminHome");
        }

        public ActionResult Logout()
        {
            Session["admin"] = null;

            return RedirectToAction("Index", "Home", new { area = "" });
        }
    }
}