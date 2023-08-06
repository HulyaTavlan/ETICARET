using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppEF.Entities
{
    [Table("tbl_product_image")]
    public class ProductImage : Base
    {
        public string Path { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}