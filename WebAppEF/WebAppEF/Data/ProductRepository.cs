using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAppEF.Entities;

namespace WebAppEF.Data
{
    public interface IProductRepository : IGenericRepository<Product>
    {

    }

    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository() : base(new DataModel())
        {

        }
    }
}