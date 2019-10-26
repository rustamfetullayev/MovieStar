using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MovieStar.Models;

namespace MovieStar.Areas.Admin.Controllers
{
    [AuthorizeAdmin]
    public class AdminHomeController : Controller
    {
        private readonly MSEntities db = new MSEntities();

        // GET: Admin/AdminHome
        public ActionResult Index()
        {
            return View(db.Users.OrderBy(u=>u.Name).ToList());
        }
    }
}