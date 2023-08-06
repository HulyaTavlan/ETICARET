using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAppEF.Entities;

namespace WebAppEF.Data
{
    public interface IBrandRepository : IGenericRepository<Brand>
    {

    }

    public class BrandRepository : GenericRepository<Brand>, IBrandRepository
    {
        public BrandRepository() : base(new DataModel())
        {

        }
    }
}