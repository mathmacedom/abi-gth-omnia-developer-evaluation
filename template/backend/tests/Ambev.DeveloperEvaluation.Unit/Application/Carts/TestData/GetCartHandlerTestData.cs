using Ambev.DeveloperEvaluation.Application.Carts.GetCart;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.Carts.TestData
{
    public static class GetCartHandlerTestData
    {
        public static Faker<GetCartCommand> Get()
        {
            return new Faker<GetCartCommand>()
                .RuleFor(c => c.Id, _ => Guid.NewGuid());
        }
    }
}
