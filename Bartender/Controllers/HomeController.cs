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

        //Adds new order to the db
        //[HttpPost]
        public ActionResult CreateOrder(string customer_name, string drink, string dr)
        {
            Debug.WriteLine(customer_name);
            if (customer_name.Trim().Length > 1 && drink != null)
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

        public ActionResult Create()
        {
            return View();
        }

        //This is used for adding cocktails to the menu
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
            Response.AddHeader("Refresh", "8");
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

        public ActionResult DeleteOrder(int? id)
        {
            if (id != null)
            {
                try
                {
                    var result = orderDB.Orders.Single(o => o.OrderNo == id);
                    orderDB.Orders.Remove(result);
                    orderDB.SaveChanges();
                }
                catch
                {
                    //Entry doesn't exist
                }

            }

            return RedirectToAction("Orders");
        }
    }
}