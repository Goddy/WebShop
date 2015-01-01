using System;

namespace WebShop.Models
{
    /// <summary>
    /// Summary description for Address
    /// </summary>
    public class Address : AbstractEntity<int>
    {
        public String Street { get; set; }
        public String Number { get; set; }
        public String PostalCode { get; set; }
        public String City { get; set; }
        public String Country { get; set; }
    }
}