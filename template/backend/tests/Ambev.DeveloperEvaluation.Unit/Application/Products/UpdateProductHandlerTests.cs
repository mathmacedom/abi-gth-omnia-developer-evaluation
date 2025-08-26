using Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.Products.TestData;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Products
{
    public class UpdateProductHandlerTests
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateProductHandler> _logger;
        private readonly UpdateProductHandler _handler;

        public UpdateProductHandlerTests()
        {
            _productRepository = Substitute.For<IProductRepository>();
            _mapper = Substitute.For<IMapper>();
            _logger = Substitute.For<ILogger<UpdateProductHandler>>();
            _handler = new UpdateProductHandler(_productRepository, _mapper, _logger);
        }

        [Fact(DisplayName = "Given valid request When updating product Then returns success response")]
        public async Task GivenValidRequest_WhenUpdatingProduct_ThenReturnsSuccessResponse()
        {
            // Given
            var id = Guid.NewGuid();
            var product = ProductTestData.GenerateValidProduct();
            var request = UpdateProductHandlerTestData.Get(product.Id).Generate();
            var updateProductResult = new UpdateProductResult
            {
                Id = request.Id,
                Name = request.Name,
                Description = request.Description,
                UnitPrice = request.UnitPrice,
                Category = request.Category,
                IsActive = request.IsActive
            };

            _productRepository.GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(product);
            _mapper.Map<UpdateProductResult>(product).Returns(updateProductResult);
            _productRepository.UpdateAsync(product).Returns(product);

            // When
            var response = await _handler.Handle(request, CancellationToken.None);

            // Then
            _mapper.Received(1).Map<UpdateProductResult>(Arg.Is<Product>(p =>
                p.Id == product.Id &&
                p.Name == product.Name &&
                p.UnitPrice == product.UnitPrice &&
                p.Description == product.Description &&
                p.Category == product.Category
            ));

            response.Should().NotBeNull();
            response.Should().BeEquivalentTo(updateProductResult);
        }
    }
}
