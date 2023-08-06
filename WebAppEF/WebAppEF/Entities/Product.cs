using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppEF.Entities
{
    [Table("tbl_product")]
    public class Product : Base
    {
        public decimal Price { get; set; }
        public bool Discounted { get; set; } = false;
        public decimal DiscountRatio { get; set; } = 0;
        public decimal TaxRatio { get; set; } = 0;
        public string Detail { get; set; }
        public string FeaturedImage { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        [ForeignKey("Brand")]
        public int BrandId { get; set; }
        public virtual Brand Brand { get; set; }

        public virtual ICollection<ProductImage> ProductImages { get; set; }
    }
}