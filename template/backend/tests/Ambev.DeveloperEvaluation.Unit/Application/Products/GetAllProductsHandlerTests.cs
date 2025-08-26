using Ambev.DeveloperEvaluation.Application.Products.GetAllProducts;
using Ambev.DeveloperEvaluation.Application.Products.GetProduct;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Products
{
    public class GetAllProductsHandlerTests
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllProductsHandler> _logger;
        private readonly GetAllProductsHandler _handler;

        public GetAllProductsHandlerTests()
        {
            _productRepository = Substitute.For<IProductRepository>();
            _mapper = Substitute.For<IMapper>();
            _logger = Substitute.For<ILogger<GetAllProductsHandler>>();
            _handler = new GetAllProductsHandler(_productRepository, _mapper, _logger);
        }

        [Fact(DisplayName = "Given valid request When getting all products Then returns success response")]
        public async Task GivenValidRequest_WhenGettingAllProducts_ThenReturnsSuccessResponse()
        {
            // Given
            var products = ProductTestData.GenerateValidProducts(10);
            var request = new GetAllProductsCommand();

            _productRepository.GetAllAsync(Arg.Any<bool?>(), Arg.Any<string?>(), Arg.Any<CancellationToken>()).Returns(products);

            var productsResult = products.Select(p => new GetProductResult(p.Id, p.Name, p.UnitPrice, p.Description, p.Category, p.IsActive));
            var result = new GetAllProductsResult() { Products = [.. productsResult] };

            _mapper.Map<GetAllProductsResult>(products).Returns(result);

            // When
            var response = await _handler.Handle(request, CancellationToken.None);

            // Then
            response.Should().NotBeNull();
            response.Products.Should().NotBeNull();
            response.Products.Should().HaveCount(10);
            response.Should().BeEquivalentTo(result);
        }

        [Fact(DisplayName = "Given valid request When getting all products Then returns empty list")]
        public async Task GivenValidRequest_WhenGettingAllProducts_ThenReturnsEmptyList()
        {
            // Given
            var request = new GetAllProductsCommand();

            _productRepository.GetAllAsync(Arg.Any<bool?>(), Arg.Any<string?>(), Arg.Any<CancellationToken>()).Returns([]);

            // When
            var response = await _handler.Handle(request, CancellationToken.None);

            // Then
            response.Should().BeNull();
        }
    }
}
