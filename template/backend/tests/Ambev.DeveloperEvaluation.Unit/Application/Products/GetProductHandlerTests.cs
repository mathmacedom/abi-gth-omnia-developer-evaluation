using Ambev.DeveloperEvaluation.Application.Products.GetProduct;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.Products.TestData;
using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Products
{
    public class GetProductHandlerTests
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetProductHandler> _logger;
        private readonly GetProductHandler _handler;

        public GetProductHandlerTests()
        {
            _productRepository = Substitute.For<IProductRepository>();
            _mapper = Substitute.For<IMapper>();
            _logger = Substitute.For<ILogger<GetProductHandler>>();
            _handler = new GetProductHandler(_productRepository, _mapper, _logger);
        }

        [Fact(DisplayName = "Given valid request When getting product Then returns success response")]
        public async Task GivenValidRequest_WhenGettingProduct_ThenReturnsSuccessResponse()
        {
            // Given
            var id = Guid.NewGuid();
            var request = new GetProductCommand(id);
            var createProductCommand = CreateProductHandlerTestData.Get().Generate();

            var product = new Product(
                createProductCommand.Name,
                createProductCommand.UnitPrice,
                createProductCommand.Description,
                createProductCommand.Category
                )
            {
                Id = id
            };

            var result = new GetProductResult(
                product.Id,
                product.Name,
                product.UnitPrice,
                product.Description,
                product.Category,
                true);

            _productRepository.GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(product);
            _mapper.Map<GetProductResult>(product).Returns(result);

            // When
            var response = await _handler.Handle(request, CancellationToken.None);

            // Then
            _mapper.Received(1).Map<GetProductResult>(Arg.Is<Product>(p =>
                p.Id == result.Id &&
                p.Name == result.Name &&
                p.UnitPrice == result.UnitPrice &&
                p.Description == result.Description &&
                p.Category == result.Category &&
                p.IsActive == result.IsActive
            ));

            response.Should().NotBeNull();
            response.Should().BeEquivalentTo(result);
        }
    }
}
