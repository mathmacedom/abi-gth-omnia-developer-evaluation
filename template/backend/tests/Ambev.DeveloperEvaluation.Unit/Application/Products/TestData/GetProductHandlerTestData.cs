using Ambev.DeveloperEvaluation.Application.Products.GetProduct;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.Products.TestData
{
    public static class GetProductHandlerTestData
    {
        public static Faker<GetProductCommand> Get()
        {
            return new Faker<GetProductCommand>()
                .RuleFor(p => p.Id, _ => Guid.NewGuid());
        }
    }
}
