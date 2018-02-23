using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using blogmongo.Models.Entiteti;
using blogmongo.Models;

namespace blogmongo.Controllers
{
    public class AddNewBlogController : Controller
    {
        Mongo mon;
        public AddNewBlogController()
        {
            mon = new Mongo();
        }

        // GET: AddNewBlog
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddBlog(string authorID)
        {
            ViewBag.ID = authorID;
            return View();
        }

        [HttpPost]
        public ActionResult AddBlog(FormCollection fc)
        {
            string title = fc["title"].ToString();
            string opis = fc["description"].ToString();
            string authorID = mon.vratiUseraPoEmailu(fc["email"].ToString()).Id.ToString();
            BlogNew bn = new BlogNew { title = title, description = opis,autorId=authorID};
            mon.kreirajBlog(bn);
            return RedirectToAction("Details", "Profile", new { id = authorID });
        }
    }
}