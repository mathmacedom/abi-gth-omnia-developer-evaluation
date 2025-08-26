using Ambev.DeveloperEvaluation.Application.Sales.CancelSaleItem;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales.TestData
{
    public static class CancelSaleItemHandlerTestData
    {
        public static Faker<CancelSaleItemCommand> Get()
        {
            return new Faker<CancelSaleItemCommand>()
                .RuleFor(c => c.SaleId, _ => Guid.NewGuid())
                .RuleFor(c => c.SaleItemId, _ => Guid.NewGuid());
        }
    }
}