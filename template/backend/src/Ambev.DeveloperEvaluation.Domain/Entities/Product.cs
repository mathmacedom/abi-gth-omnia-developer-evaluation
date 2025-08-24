using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    /// <summary>
    /// Represents a product in the system, including customer, branch, items, and totals.
    /// Follows DDD principles with validation and encapsulated business rules.
    /// </summary>
    public class Product : BaseEntity
    {
        /// <summary>
        /// Name of the product.
        /// </summary>
        public string Name { get; private set; } = string.Empty;

        /// <summary>
        /// Price of the product.
        /// </summary>
        public decimal Price { get; private set; }

        /// <summary>
        /// Description of the product.
        /// </summary>
        public string Description { get; private set; } = string.Empty;

        /// <summary>
        /// Category of the product.
        /// </summary>
        public string Category { get; private set; } = string.Empty;
    }
}
