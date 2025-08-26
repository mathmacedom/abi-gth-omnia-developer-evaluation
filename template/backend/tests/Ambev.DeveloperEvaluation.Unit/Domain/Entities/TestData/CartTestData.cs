using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData
{
    public static class CartTestData
    {
        private static readonly Faker<Cart> CartFaker = new Faker<Cart>()
                .RuleFor(p => p.Id, _ => Guid.NewGuid())
                .RuleFor(p => p.Branch, f => f.Company.CompanyName())
                .RuleFor(p => p.CustomerId, _ => Guid.NewGuid())
                .RuleFor(p => p.Status, _ => CartStatus.Active)
                .RuleFor(p => p.Items, f => f.Make(3, () => CartItemTestData.GenerateValidCartItem()));

        public static Cart GenerateValidCart(Guid? cartId = null)
        {
            var cart = CartFaker.Generate();

            if (cartId.HasValue)             {
                cart = CartFaker.RuleFor(x => x.Id, _ => cartId.Value);
            }

            return CartFaker.Generate();
        }

        public static List<Cart> GenerateValidCarts(int count)
        {
            return CartFaker.Generate(count);
        }
    }
}
