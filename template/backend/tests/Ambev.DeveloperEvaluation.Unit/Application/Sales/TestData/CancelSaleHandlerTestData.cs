using Ambev.DeveloperEvaluation.Application.Sales.CancelSale;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales.TestData
{
    public static class CancelSaleHandlerTestData
    {
        public static Faker<CancelSaleCommand> Get()
        {
            return new Faker<CancelSaleCommand>()
                .RuleFor(c => c.Id, _ => Guid.NewGuid());
        }
    }
}
