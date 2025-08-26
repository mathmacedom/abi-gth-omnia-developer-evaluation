using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    /// <summary>
    /// Represents a product in the system, including name, price, description, and category.
    /// Follows DDD principles with validation and encapsulated business rules.
    /// </summary>
    public class Product : BaseEntity
    {
        /// <summary>
        /// Name of the product.
        /// </summary>
        public string Name { get; private set; } = string.Empty;

        /// <summary>
        /// Unit price of the product.
        /// </summary>
        public decimal UnitPrice { get; private set; }

        /// <summary>
        /// Description of the product.
        /// </summary>
        public string Description { get; private set; } = string.Empty;

        /// <summary>
        /// Category of the product.
        /// </summary>
        public string Category { get; private set; } = string.Empty;

        /// <summary>
        /// Indicates whether the product is active.
        /// </summary>
        public bool IsActive { get; private set; } = true;

        public Product()
        {
            
        }

        /// <summary>
        /// Create a new product instance
        /// </summary>
        /// <param name="name">Name of the product</param>
        /// <param name="unitPrice">Unit price of the product</param>
        /// <param name="description">Description of the product</param>
        /// <param name="category">Category name of the product</param>
        public Product(string name, decimal unitPrice, string description, string category)
        {
            Name = name;
            UnitPrice = unitPrice;
            Description = description;
            Category = category;
        }

        public void Unactivate()
        {
            IsActive = false;
        }
    }
}
