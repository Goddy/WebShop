namespace WebShop.Models
{
    /// <summary>
    /// Summary description for IEntity
    /// </summary>
    public interface IEntity<T>
    {
        T Id { get; set; }
    }
}