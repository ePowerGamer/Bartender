using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Bartender.Models
{
    public class Cocktail
    {
        [Key]
        public string Name { get; set; }

        [DisplayFormat(DataFormatString ="{0:C}")]
        public decimal Price { get; set; }

        [DisplayFormat(NullDisplayText ="No description available")]
        public string Description { get; set; }
    }

    public class CocktailContext : DbContext
    {
        public CocktailContext() : base("BartenderDB")
        {

        }

        public DbSet<Cocktail> Cocktails { get; set; }
    }
}