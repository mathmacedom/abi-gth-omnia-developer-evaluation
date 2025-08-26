using Ambev.DeveloperEvaluation.Application.Carts.DeleteCartItem;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.Carts.TestData
{
    public static class DeleteCartItemHandlerTestData
    {
        public static Faker<DeleteCartItemCommand> Get(Guid? cartId = null, Guid? cartItemId = null)
        {
            return new Faker<DeleteCartItemCommand>()
                .RuleFor(c => c.CartId, _ => cartId ?? Guid.NewGuid())
                .RuleFor(c => c.CartItemId, _ => cartItemId ?? Guid.NewGuid());
        }
    }
}
