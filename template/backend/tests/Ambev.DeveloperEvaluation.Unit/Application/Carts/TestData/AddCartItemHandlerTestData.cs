using Ambev.DeveloperEvaluation.Application.Carts.AddCartItem;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.Carts.TestData
{
    public static class AddCartItemHandlerTestData
    {
        public static Faker<AddCartItemCommand> Get(Guid? cartId = null)
        {
            return new Faker<AddCartItemCommand>()
                .RuleFor(c => c.CartId, _ => cartId ?? Guid.NewGuid())
                .RuleFor(c => c.ProductId, _ => Guid.NewGuid())
                .RuleFor(c => c.Quantity, f => f.Random.Int(1, 10));
        }
    }
}
