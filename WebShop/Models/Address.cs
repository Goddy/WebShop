namespace WebShop.Models
{
    /// <summary>
    /// Summary description for Address
    /// </summary>
    public class Address : AbstractEntity<int>
    {
        public string Street { get; set; }
        public int Number { get; set; }
        public int PostalCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}