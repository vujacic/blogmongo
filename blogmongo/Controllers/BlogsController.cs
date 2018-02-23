using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using blogmongo.Models;
using blogmongo.Models.Entiteti;

namespace blogmongo.Controllers
{
    [Authorize]   
    public class BlogsController : Controller
    {
        Mongo mon;
        public BlogsController()
        {
            mon = new Mongo();
        }

        // GET: BlogoviOsobe
        public ActionResult Index()
        {
            return View();
        }

        // GET: BlogoviOsobe/Details/5
        public ActionResult Details(/*int id*/string id)
        {
            List<BlogPost> postovi = mon.vratiSveBlogoveAutora(/*mongoID*/id);
            return View(postovi);
            //ViewBag.ID = id;
            //return View();
        }
        
        //[HttpGet]
        //public ActionResult GetData(string id,int pageIndex,int pageSize)
        //{
        //    List<BlogPost> blogovi = mon.vratiNBlogovaAutora(id, pageIndex, pageSize);
        //    return Json(blogovi,JsonRequestBehavior.AllowGet);
        //}

        // GET: BlogoviOsobe/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BlogoviOsobe/Create
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

        // GET: BlogoviOsobe/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: BlogoviOsobe/Edit/5
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

        // GET: BlogoviOsobe/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: BlogoviOsobe/Delete/5
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
