using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebAppEF.Entities
{
    public class Base
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public DateTime CreateDate { get; set; } = DateTime.Now;
        public bool Active { get; set; } = true;
        public bool Deleted { get; set; } = false;
    }
}