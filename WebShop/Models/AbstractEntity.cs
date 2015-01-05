using System;

namespace WebShop.Models
{
    /// <summary>
    /// Contains global entity properties.
    /// </summary>
    public abstract class AbstractEntity<T> : ICloneable
    {
        public virtual T Id { get; set; }
        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}