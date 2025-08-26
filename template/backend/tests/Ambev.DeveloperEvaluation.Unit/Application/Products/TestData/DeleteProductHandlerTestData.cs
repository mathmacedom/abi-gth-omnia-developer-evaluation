using Ambev.DeveloperEvaluation.Application.Products.DeleteProduct;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.Products.TestData
{
    public static class DeleteProductHandlerTestData
    {
        public static Faker<DeleteProductCommand> Get(Guid? id)
        {
            return new Faker<DeleteProductCommand>()
                .RuleFor(p => p.Id, _ => id ?? Guid.NewGuid());
        }
    }
}
