using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
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
    public class CreateProductHandlerTests
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateProductHandler> _logger;
        private readonly CreateProductHandler _handler;

        public CreateProductHandlerTests()
        {
            _productRepository = Substitute.For<IProductRepository>();
            _mapper = Substitute.For<IMapper>();
            _logger = Substitute.For<ILogger<CreateProductHandler>>();
            _handler = new CreateProductHandler(_productRepository, _mapper, _logger);
        }

        [Fact(DisplayName = "Given valid request When creating product Then returns success response")]
        public async Task GivenValidRequest_WhenCreatingProduct_ThenReturnsSuccessResponse()
        {
            // Given
            var id = Guid.NewGuid();
            var request = CreateProductHandlerTestData.Get().Generate();
            var product = new Product(
                request.Name,
                request.UnitPrice,
                request.Description,
                request.Category
                )
            {
                Id = id
            };
            var createProductResult = new CreateProductResult
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                UnitPrice = product.UnitPrice,
                Category = product.Category,
                IsActive = product.IsActive
            };

            _mapper.Map<Product>(request).Returns(product);
            _mapper.Map<CreateProductResult>(product).Returns(createProductResult);
            _productRepository.CreateAsync(product, Arg.Any<CancellationToken>()).Returns(product);

            // When
            var response = await _handler.Handle(request, CancellationToken.None);

            // Then
            _mapper.Received(1).Map<Product>(Arg.Is<CreateProductCommand>(p =>
                p.Name == product.Name &&
                p.UnitPrice == product.UnitPrice &&
                p.Description == product.Description &&
                p.Category == product.Category
            ));

            _mapper.Received(1).Map<CreateProductResult>(Arg.Is<Product>(p =>
                p.Id == product.Id &&
                p.Name == product.Name &&
                p.UnitPrice == product.UnitPrice &&
                p.Description == product.Description &&
                p.Category == product.Category
            ));

            response.Should().NotBeNull();
            response.Should().BeEquivalentTo(createProductResult);
            await _productRepository.Received(1).CreateAsync(product, Arg.Any<CancellationToken>());
        }
    }
}
