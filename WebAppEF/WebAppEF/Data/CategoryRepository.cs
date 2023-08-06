using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAppEF.Entities;

namespace WebAppEF.Data
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {

    }

    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository() : base(new DataModel())
        {

        }
    }
}