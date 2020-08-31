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

        public ActionResult Orders()
        {
            Response.AddHeader("Refresh", "8");
            return View(orderDB.Orders.ToList());
        }

        /// <summary>
        /// Adds a new order to the db using the given customer name and drink select
        /// </summary>
        /// <param name="customer_name"></param>
        /// <param name="drink"></param>
        /// <returns></returns>
        public ActionResult CreateOrder(string customer_name, string drink)
        {
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

        /// <summary>
        /// Get order by id and deletes that order if it was found in the database.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Gets order by id and changes the status of that order to Order.State.READY
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult ReadyOrder(int? id)
        {
            if (id != null)
            {
                var result = orderDB.Orders.Single(o => o.OrderNo == id);
                result.Status = Order.State.READY;
                orderDB.SaveChanges();
            }

            return RedirectToAction("Orders");
        }

        //This was used for adding cocktails to the menu.
        //public ActionResult Create()
        //{
        //    return View();
        //}

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
    }
}