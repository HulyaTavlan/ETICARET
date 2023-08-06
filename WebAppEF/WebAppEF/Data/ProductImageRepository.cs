using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAppEF.Entities;

namespace WebAppEF.Data
{
    public interface IProductImageRepository : IGenericRepository<ProductImage>
    {

    }

    public class ProductImageRepository : GenericRepository<ProductImage>, IProductImageRepository
    {
        public ProductImageRepository() : base(new DataModel())
        {

        }
    }
}