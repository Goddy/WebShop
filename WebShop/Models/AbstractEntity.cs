namespace WebShop.Models
{
    /// <summary>
    /// Contains global entity properties.
    /// </summary>
    public abstract class AbstractEntity<T>
    {
        public virtual T Id { get; set; }
    }
}