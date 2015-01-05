using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebShop.Models
{
    public class Image : AbstractEntity<int>
    {
        [Key, ForeignKey("Product")]
        public int ProductId { get; set; }
        [Required]
        public String Location { get; set; }
        public String Description { get; set; }
        public virtual Product Product { get; set; }

        public Image()
        {
        }

        public Image(String location, String description)
        {
            Location = location;
            Description = description;

        }
    }
}