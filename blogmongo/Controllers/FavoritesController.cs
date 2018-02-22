using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using blogmongo.Models.Entiteti;
using blogmongo.Models;

namespace blogmongo.Controllers
{
    [Authorize]
    public class FavoritesController : Controller
    {
        Mongo mon;

        public FavoritesController()
        {
            mon = new Mongo();
        }

        // GET: Favorites
        public ActionResult Index()
        {
            return View();
        }

        // GET: Favorites/Details/5
        public ActionResult Details(/*int id*/string id)
        {
            //List<BlogPost> favoriti = mon.vratiFavoriteAutora(id);
            //return View(favoriti);
            ViewBag.ID = id;
            return View();
        }
        
        public ActionResult GetData(string id, int pageIndex, int pageSize)
        {
            List<BlogPost> blogovi = mon.vratiNFavoritaAutora(id, pageIndex, pageSize);
            return Json(blogovi,JsonRequestBehavior.AllowGet);
        }

        // GET: Favorites/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Favorites/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Favorites/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Favorites/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Favorites/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Favorites/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
