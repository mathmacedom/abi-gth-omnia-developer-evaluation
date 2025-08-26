using Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.Products.TestData
{
    public static class UpdateProductHandlerTestData
    {
        public static Faker<UpdateProductCommand> Get(Guid id)
        {
            return new Faker<UpdateProductCommand>()
                .RuleFor(p => p.Id, _ => id)
                .RuleFor(p => p.Name, f => f.Commerce.ProductName())
                .RuleFor(p => p.UnitPrice, f => decimal.Parse(f.Commerce.Price(1, 2000)))
                .RuleFor(p => p.Description, f => f.Commerce.ProductDescription())
                .RuleFor(p => p.Category, f => f.Commerce.Categories(1)[0])
                .RuleFor(p => p.IsActive, true);
        }
    }
}
