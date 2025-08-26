using Ambev.DeveloperEvaluation.Application.Carts.GetCartByCustomerId;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.Carts.TestData
{
    public static class GetCartByCustomerIdHandlerTestData
    {
        public static Faker<GetCartByCustomerIdCommand> Get()
        {
            return new Faker<GetCartByCustomerIdCommand>()
                .RuleFor(c => c.CustomerId, _ => Guid.NewGuid());
        }
    }
}
