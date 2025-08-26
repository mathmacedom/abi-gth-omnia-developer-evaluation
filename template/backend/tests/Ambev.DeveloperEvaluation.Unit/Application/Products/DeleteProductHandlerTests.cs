using Ambev.DeveloperEvaluation.Application.Products.DeleteProduct;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Products
{
    public class DeleteProductHandlerTests
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<DeleteProductHandler> _logger;
        private readonly DeleteProductHandler _handler;

        public DeleteProductHandlerTests()
        {
            _productRepository = Substitute.For<IProductRepository>();
            _logger = Substitute.For<ILogger<DeleteProductHandler>>();
            _handler = new DeleteProductHandler(_productRepository, _logger);
        }

        [Fact(DisplayName = "Given valid request When deleting product Then returns success response")]
        public async Task GivenValidRequest_WhenDeletingProduct_ThenReturnsSuccessResponse()
        {
            // Given
            var id = Guid.NewGuid();
            var request = new DeleteProductCommand(id);

            _productRepository.DeleteAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);

            // When
            var response = await _handler.Handle(request, CancellationToken.None);

            // Then
            response.Should().NotBeNull();
            response.Success.Should().Be(true);
        }
    }
}
