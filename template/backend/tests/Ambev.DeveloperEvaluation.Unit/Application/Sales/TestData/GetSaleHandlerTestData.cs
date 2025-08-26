using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales.TestData
{
    public static class GetSaleHandlerTestData
    {
        public static Faker<GetSaleCommand> Get()
        {
            return new Faker<GetSaleCommand>()
                .RuleFor(c => c.Id, _ => Guid.NewGuid());
        }
    }
}
