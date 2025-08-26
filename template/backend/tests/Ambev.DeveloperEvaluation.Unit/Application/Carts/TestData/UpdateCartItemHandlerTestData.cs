using Ambev.DeveloperEvaluation.Application.Carts.UpdateCartItem;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.Carts.TestData
{
    public static class UpdateCartItemHandlerTestData
    {
        public static Faker<UpdateCartItemCommand> Get(Guid? cartId = null)
        {
            return new Faker<UpdateCartItemCommand>()
                .RuleFor(c => c.CartId, _ => cartId ?? Guid.NewGuid())
                .RuleFor(c => c.CartItemId, _ => Guid.NewGuid())
                .RuleFor(c => c.Quantity, f => f.Random.Int(1, 20));
        }
    }
}
