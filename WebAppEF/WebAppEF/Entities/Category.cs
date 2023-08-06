using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppEF.Entities
{
    [Table("tbl_category")]
    public class Category : Base {
        public virtual ICollection<Product> Products { get; set; }
    }
}