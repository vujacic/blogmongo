using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using blogmongo.Models;
using blogmongo.Models.Entiteti;

namespace blogmongo.Controllers
{
    public class ProfileController : Controller
    {
        Mongo mon;
        public ProfileController()
        {
            mon = new Mongo();
        }
        // GET: Profile
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Details(/*int id*/string id)
        {
            User korisnik = mon.vratiUsera(id);
            return View(korisnik);
        }

        public ActionResult Edit(string id)
        {
            User korisnik = mon.vratiUsera(id);
            return View(korisnik);
        }

        [HttpPost]
        public ActionResult Edit(string id,FormCollection fc)
        {
            string ime = fc["ime"].ToString();
            string prezime = fc["prezime"].ToString();
            string opis = fc["opis"].ToString();
            mon.updateUsera(ime, prezime, opis, id);
            return RedirectToAction("Details", "Profile",new { id=id});
        }
    }
}