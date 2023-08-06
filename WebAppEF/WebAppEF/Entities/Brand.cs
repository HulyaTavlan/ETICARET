using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppEF.Entities
{
    [Table("tbl_brand")]
    public class Brand : Base {
        public virtual ICollection<Product> Products { get; set; }
    }
}