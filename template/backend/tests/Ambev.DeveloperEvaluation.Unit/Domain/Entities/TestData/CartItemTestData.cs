using Ambev.DeveloperEvaluation.Domain.Entities;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData
{
    public static class CartItemTestData
    {
        private static readonly Faker<CartItem> CartItemFaker = new Faker<CartItem>()
                .RuleFor(p => p.Id, _ => Guid.NewGuid())
                .RuleFor(p => p.CartId, _ => Guid.NewGuid())
                .RuleFor(p => p.ProductId, _ => Guid.NewGuid())
                .RuleFor(p => p.ProductName, f => f.Commerce.ProductName())
                .RuleFor(p => p.Quantity, f => f.Random.Int(1, 20))
                .RuleFor(p => p.UnitPrice, f => decimal.Parse(f.Commerce.Price(1, 2000)));

        public static CartItem GenerateValidCartItem()
        {
            var cartItem = CartItemFaker.Generate();
            cartItem.UpdateSubtotal();
            
            return cartItem;
        }

        public static List<CartItem> GenerateValidCartItems(int count)
        {
            var cartItemsFaker = CartItemFaker;
            cartItemsFaker.FinishWith((_, cartItem) => cartItem.UpdateSubtotal());

            return cartItemsFaker.Generate(count);
        }
    }
}
