using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Bartender.Models
{
    public class Order
    {
        public enum State { PREPARING, READY }

        [Key]
        public int OrderNo { get; set; }
        public string Cocktail { get; set; }
        public string Customer { get; set; }
        public State Status { get; set; }

    }

    public class OrderContext : DbContext
    {
        public OrderContext() : base("BartenderDB")
        {

        }

        public DbSet<Order> Orders { get; set; }
    }
}