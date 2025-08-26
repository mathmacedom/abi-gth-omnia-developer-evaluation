using Ambev.DeveloperEvaluation.Application.Carts.CreateCart;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.Carts.TestData
{
    public static class CreateCartHandlerTestData
    {
        public static Faker<CreateCartCommand> Get()
        {
            return new Faker<CreateCartCommand>()
                .RuleFor(c => c.CustomerId, _ => Guid.NewGuid())
                .RuleFor(c => c.Branch, f => f.Company.CompanyName());
        }
    }
}
