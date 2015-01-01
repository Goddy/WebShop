using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebShop.Models
{
    /// <summary>
    /// Product entity with a many-to-many rel to OrderProduct
    /// </summary>
    public class Product : AbstractEntity<int>
    {
        public Product()
        {
            Images = new List<Image>();
        }

        [Required]
        public String Name { get; set; }
        public String Description { get; set; }
        [Required]
        public String Category { get; set; }
        [Required]
        public double Price { get; set; }

        public virtual ICollection<Image> Images { get; set; }
    
        public virtual ICollection<OrderProduct> OrderProducts { get; set; }
    }
}