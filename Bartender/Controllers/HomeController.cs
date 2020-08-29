using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bartender.Models;

namespace Bartender.Controllers
{
    public class HomeController : Controller
    {
        private CocktailContext db = new CocktailContext();
        private OrderContext orderDB = new OrderContext();

        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult Menu()
        {
            return View(db.Cocktails.ToList());
        }


        [HttpPost]
        public ActionResult Menu(string customer_name, string drink)
        {
            if (customer_name != "" && drink != null)
            {
                orderDB.Orders.Add(new Order
                {
                    OrderNo = orderDB.Orders.OrderByDescending(o => o.OrderNo).First().OrderNo + 1,
                    Cocktail = drink,
                    Customer = customer_name,
                    Status = Order.State.PREPARING
                });
                orderDB.SaveChanges();
            }

            return View("Index");
        }

        public ActionResult Create(string name, string customer)
        {
            if(name != null && customer != null)
            {
                orderDB.Orders.Add(new Order
                {
                    OrderNo = orderDB.Orders.OrderByDescending(o => o.OrderNo).First().OrderNo + 1,
                    Cocktail = name,
                    Customer = customer,
                    Status = Order.State.PREPARING
                });
            }

            return View("Index");
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(Cocktail cocktail)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Cocktails.Add(cocktail);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(cocktail);
        //}

        public ActionResult Orders()
        {
            return View(orderDB.Orders.ToList());
        }

        public ActionResult Ready(int? id)
        {
            if(id != null)
            {
                var result = orderDB.Orders.Single(o => o.OrderNo == id);
                result.Status = Order.State.READY;
                orderDB.SaveChanges();
            }

            return View("Orders", orderDB.Orders.ToList());
        }
    }
}