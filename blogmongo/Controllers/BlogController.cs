using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using blogmongo.Models.Entiteti;
using blogmongo.Models;

namespace blogmongo.Controllers
{
    public class BlogController : Controller
    {
        Mongo mon;
        public BlogController()
        {
            mon = new Mongo();
        }
        // GET: Blog
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Details(string id)
        {
            BlogPost bp = mon.vratiJedanBlog(id);
            ViewBag.Email = mon.vratiUsera(bp.Author.ToString()).Email;
            ViewBag.ID = bp.Author.ToString();
            return View(bp);
        }

        [HttpPost]
        public void Details(string blogID,string email)
        {
            mon.dodajUFavorite(blogID,email);
        }

        [HttpPost]
        public void AddFavorite(string blogID,string email)
        {
            mon.dodajUFavorite(blogID, email);
        }

        [HttpPost]
        public void Brisi(string blogID,string email,string userID)
        {
            mon.brisiBlog(blogID, email);
        }
    }
}