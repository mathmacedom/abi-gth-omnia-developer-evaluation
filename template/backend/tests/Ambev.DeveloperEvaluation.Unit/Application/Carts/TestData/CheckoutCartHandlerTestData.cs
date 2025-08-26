using Ambev.DeveloperEvaluation.Application.Carts.CheckoutCart;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.Carts.TestData
{
    public static class CheckoutCartHandlerTestData
    {
        public static Faker<CheckoutCartCommand> Get(Guid? cartId = null)
        {
            return new Faker<CheckoutCartCommand>()
                .RuleFor(c => c.Id, _ => cartId ?? Guid.NewGuid());
        }
    }
}
