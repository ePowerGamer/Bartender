using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bartender.Models;

namespace Bartender.Controllers
{
    public class HomeController : Controller
    {
        private CocktailContext db = new CocktailContext();

        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult Menu()
        {
            return View(db.Cocktails.ToList());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Cocktail cocktail)
        {
            if (ModelState.IsValid)
            {
                db.Cocktails.Add(cocktail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cocktail);
        }
    }
}