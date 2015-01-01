using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebShop.Models
{
    public class Image : AbstractEntity<int>
    {
        public Image()
        {
        }

        public Image(String location, String description)
        {
            this.Location = location;
            this.Description = description;
        }

        [Required]
        public String Location { get; set; }
        public String Description { get; set; }
        public virtual Product Product { get; set; }
    }
}