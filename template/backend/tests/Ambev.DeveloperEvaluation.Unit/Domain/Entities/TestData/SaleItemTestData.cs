using Ambev.DeveloperEvaluation.Domain.Entities;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData
{
    public static class SaleItemTestData
    {
        private static readonly Faker<SaleItem> SaleItemFaker = new Faker<SaleItem>()
                .RuleFor(p => p.Id, _ => Guid.NewGuid())
                .RuleFor(p => p.SaleId, _ => Guid.NewGuid())
                .RuleFor(p => p.ProductId, _ => Guid.NewGuid())
                .RuleFor(p => p.ProductName, f => f.Commerce.ProductName())
                .RuleFor(p => p.Quantity, f => f.Random.Int(1, 20))
                .RuleFor(p => p.UnitPrice, f => decimal.Parse(f.Commerce.Price(1, 2000)));

        public static SaleItem GenerateValidSaleItem(Guid? saleId = null, Guid? productId = null, string? productName = null)
        {
            var sale = SaleItemFaker.Generate();
            sale.UpdateTotal();

            if (saleId.HasValue)
            {
                sale = SaleItemFaker.RuleFor(x => x.SaleId, _ => saleId.Value);
            }

            if (productId.HasValue)
            {
                sale = SaleItemFaker.RuleFor(x => x.ProductId, _ => productId.Value);
            }

            if (!string.IsNullOrEmpty(productName))
            {
                sale = SaleItemFaker.RuleFor(x => x.ProductName, _ => productName);
            }

            return sale;
        }

        public static List<SaleItem> GenerateValidSaleItems(int count, Guid? saleId = null)
        {
            var salesFaker = SaleItemFaker;

            if (saleId.HasValue)
            {
                salesFaker.RuleFor(s => s.SaleId, _ => saleId.Value);
            }

            salesFaker.FinishWith((_, saleItem) => saleItem.UpdateTotal());

            return salesFaker.Generate(count);
        }
    }
}
