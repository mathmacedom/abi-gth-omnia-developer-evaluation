using Ambev.DeveloperEvaluation.Application.Carts.DeleteCartItem;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.Carts.TestData;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Carts
{
    public class DeleteCartItemHandlerTests
    {
        private readonly ICartRepository _cartRepository;
        private readonly ILogger<DeleteCartItemHandler> _logger;
        private readonly DeleteCartItemHandler _handler;

        public DeleteCartItemHandlerTests()
        {
            _cartRepository = Substitute.For<ICartRepository>();
            _logger = Substitute.For<ILogger<DeleteCartItemHandler>>();
            _handler = new DeleteCartItemHandler(_cartRepository, _logger);
        }

        [Fact(DisplayName = "Given valid request When deleting cart item Then returns success response")]
        public async Task GivenValidRequest_WhenDeletingCartItem_ThenReturnsSuccessResponse()
        {
            // Given
            var cart = CartTestData.GenerateValidCart();
            var request = DeleteCartItemHandlerTestData.Get(cart.Id, cart.Items[0].Id).Generate();

            _cartRepository.GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(cart);
            _cartRepository.DeleteAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);

            // When
            var response = await _handler.Handle(request, CancellationToken.None);

            // Then
            response.Should().NotBeNull();
            response.Success.Should().Be(true);
        }
    }
}
