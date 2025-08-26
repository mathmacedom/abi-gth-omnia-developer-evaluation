using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData
{
    public static class SaleTestData
    {
        private static readonly Faker<Sale> SaleFaker = new Faker<Sale>()
                .RuleFor(p => p.Id, _ => Guid.NewGuid())
                .RuleFor(p => p.SaleNumber, _ => Guid.NewGuid().ToString("N")[..8].ToUpper())
                .RuleFor(p => p.SaleDate, _ => DateTime.Now)
                .RuleFor(p => p.Status, _ => SaleStatus.Open)
                .RuleFor(p => p.CustomerId, _ => Guid.NewGuid())
                .RuleFor(p => p.CustomerName, f => f.Person.FirstName)
                .RuleFor(p => p.Branch, f => f.Company.CompanyName());

        public static Sale GenerateValidSale(Guid? id = null, Guid? customerId = null, string? branch = null)
        {
            var sale = SaleFaker.Generate();

            if (id.HasValue)
            {
                sale = SaleFaker.RuleFor(x => x.Id, _ => id.Value);
            }

            if (customerId.HasValue)
            {
                sale = SaleFaker.RuleFor(x => x.CustomerId, _ => customerId.Value);
            }

            if (!string.IsNullOrEmpty(branch))
            {
                sale = SaleFaker.RuleFor(x => x.Branch, _ => branch);
            }

            return sale;
        }

        public static List<Sale> GenerateValidSales(int count)
        {
            return SaleFaker.Generate(count);
        }
    }
}
