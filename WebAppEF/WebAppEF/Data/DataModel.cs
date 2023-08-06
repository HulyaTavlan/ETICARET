using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebAppEF.Entities;

namespace WebAppEF.Data
{
    public class DataModel : DbContext
    {
        private static string connectionString = @"Server=BASCE;Database=eCommerceData;Trusted_Connection=True;";

        public DataModel() : base(connectionString) { }

        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Brand> Brands { get; set; }
        public virtual DbSet<ProductImage> ProductImages { get; set; }
    }
}