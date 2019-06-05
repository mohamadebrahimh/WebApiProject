using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using EshopMVC.Models;

namespace EshopMVC.DAL
{
    public class EshopDbContext : DbContext
    {
        public EshopDbContext()
        {

        }

        #region DBSET

        public DbSet<Category> Categories { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Product> Products { get; set; }

        #endregion

    }
}