using Ambev.DeveloperEvaluation.Application.Products.GetAllProducts;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.Products.TestData
{
    public static class GetAllProductsHandlerTestData
    {
        public static Faker<GetAllProductsCommand> Get()
        {
            return new Faker<GetAllProductsCommand>();
        }
    }
}
