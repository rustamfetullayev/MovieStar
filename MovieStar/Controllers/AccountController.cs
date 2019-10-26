using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MovieStar.Models;
using System.Web.Helpers;
using System.Data.Entity;
using MovieStar.Extensions;
using MovieStar.ViewModels;

namespace MovieStar.Controllers
{
    public class AccountController : Controller
    {
        private readonly MSEntities db = new MSEntities();

        // GET: Account
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(HttpPostedFileBase Image, string name, string username, string password, string confpass, string bio, string email)
        {
            if(string.IsNullOrEmpty(name.Trim()) || string.IsNullOrEmpty(email.Trim()) ||
                string.IsNullOrEmpty(username.Trim()))
            {
                ViewBag.RegisterError = "Please fill empty areas correctly.";
                return View();
            }

            if (password.Trim().Length < 6)
            {
                ViewBag.RegisterError = "Please select strong password.";
                return View();
            }

            User usr = db.Users.FirstOrDefault(u => u.Email == email);

            if (usr != null)
            {
                ViewBag.RegisterError = "Please select another email.";
                return View();
            }

            if (Image == null)
            {
                ViewBag.RegisterError = "Please select an image.";
                return View();
            }

            if (!Extension.CheckImageType(Image))
            {
                ViewBag.RegisterError = "Please select another image. Type of picture is not valid.";
                return View();
            }

            if (password != confpass)
            {
                ViewBag.RegisterError = "Please confirm your password correctly.";
                return View();
            }

            User newusr = new User
            {
                Image = Extension.SaveImage(Server.MapPath("~/Source/images"), Image),
                Name = name,
                Username = username,
                Email = email,
                Password = Crypto.HashPassword(password),
                Bio = bio
            };

            db.Users.Add(newusr);
            db.SaveChanges();
            Session["user"] = newusr;

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string email, string password)
        {
            User currentuser = db.Users.FirstOrDefault(u => u.Email == email);

            if (currentuser == null)
            {
                ViewBag.LoginError = "Email or password is wrong.";
                return View();
            }

            if (!Crypto.VerifyHashedPassword(currentuser.Password, password))
            {
                ViewBag.LoginError = "Email or password is wrong.";
                return View();
            }

            Session["user"] = currentuser;
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Logout()
        {
            Session["user"] = null;

            return RedirectToAction("Index", "Home");
        }

        [AuthorizeUser]
        public ActionResult Personal()
        {
            User currentuser = db.Users.Find(((User)Session["user"]).ID);

            UserPersonalVM vm = new UserPersonalVM
            {
                User = currentuser,
                LikedFilms = db.LikedFilms.Where(l => l.UserID == currentuser.ID).OrderByDescending(f=>f.LikeTime).Select(i => i.Film),
                SavedFilms = db.SavedFilms.Where(s => s.UserID == currentuser.ID).OrderByDescending(f => f.SaveTime).Select(a => a.Film),
                Country_of_Film=db.Film_to_Country,
                Genres_of_Film=db.Film_to_Genre
            };

            return View(vm);
        }

        [AuthorizeUser]
        public ActionResult Edit()
        {
            User currentuser = db.Users.Find(((User)Session["user"]).ID);

            return View(currentuser);
        }

        [HttpPost]
        [AuthorizeUser]
        public ActionResult Edit(User currentuser, HttpPostedFileBase NewImage, string name, string username, string email, string bio)
        {
            currentuser = db.Users.Find(((User)Session["user"]).ID);

            if (NewImage!=null)
            {
                if (Extension.CheckImageType(NewImage))
                {
                    Extension.DeleteImage(Server.MapPath("~/Source/images"), currentuser.Image);
                    currentuser.Image = Extension.SaveImage(Server.MapPath("~/Source/images"), NewImage);
                }
                else
                {
                    ViewBag.EditError = "Please select valid type of image.";
                    return View(currentuser);
                }
            }

            User sessuser = Session["user"] as User;
            sessuser.Username = username;
            currentuser.Name = name;
            currentuser.Username = username;
            currentuser.Bio = bio;
            currentuser.Email = email;
            db.SaveChanges();

            return (RedirectToAction("Personal", "Account"));
        }

        [HttpPost]
        [AuthorizeUser]
        public ActionResult EditPassword(User currentuser, string oldpass, string newpass, string confirmpass)
        {
            currentuser = db.Users.Find(((User)Session["user"]).ID);

            if (Crypto.VerifyHashedPassword(currentuser.Password, oldpass))
            {
                if (newpass == confirmpass)
                {
                    currentuser.Password = Crypto.HashPassword(newpass);
                    db.SaveChanges();
                    return (RedirectToAction("Personal", "Account"));
                }
                else
                {
                    ViewBag.EditError = "Please confirm your new password correctly.";
                    return (RedirectToAction("Edit", "Account",currentuser));
                }
            }
            else
            {
                ViewBag.EditError = "Please enter your previous password correctly.";
                return (RedirectToAction("Edit", "Account",currentuser));
            }
        }
    }
}