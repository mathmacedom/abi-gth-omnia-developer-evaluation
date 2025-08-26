using Ambev.DeveloperEvaluation.Application.Carts.UpdateCartItem;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.Carts.TestData;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Carts
{
    public class UpdateCartItemHandlerTests
    {
        private readonly ICartRepository _cartRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateCartItemHandler> _logger;
        private readonly UpdateCartItemHandler _handler;

        public UpdateCartItemHandlerTests()
        {
            _cartRepository = Substitute.For<ICartRepository>();
            _productRepository = Substitute.For<IProductRepository>();
            _mapper = Substitute.For<IMapper>();
            _logger = Substitute.For<ILogger<UpdateCartItemHandler>>();
            _handler = new UpdateCartItemHandler(_cartRepository, _productRepository, _mapper, _logger);
        }

        [Fact(DisplayName = "Given valid request When updating cart item Then returns success response")]
        public async Task GivenValidRequest_WhenUpdatingCartItem_ThenReturnsSuccessResponse()
        {
            // Given
            var cart = CartTestData.GenerateValidCart();
            var request = UpdateCartItemHandlerTestData.Get().Generate();
            cart.Id = request.CartId; // Ensure the cart has the correct ID
            cart.Items[0].Id = request.CartItemId; // Ensure the cart has the item to be updated
            request.Quantity = cart.Items[0].Quantity > 1 ? cart.Items[0].Quantity - 1 : cart.Items[0].Quantity + 1;
            var updateCartResult = new UpdateCartItemResult
            {
                Id = cart.Id,
                Branch = cart.Branch,
                CustomerId = cart.CustomerId,
                Status = cart.Status
            };

            _mapper.Map<UpdateCartItemResult>(cart).Returns(updateCartResult);
            _cartRepository.GetByIdAsync(request.CartId, Arg.Any<CancellationToken>()).Returns(cart);
            _cartRepository.UpdateAsync(cart, Arg.Any<CancellationToken>()).Returns(cart);

            // When
            var response = await _handler.Handle(request, CancellationToken.None);

            // Then
            _mapper.Received(1).Map<UpdateCartItemResult>(Arg.Is<Cart>(c =>
                c.Id == cart.Id &&
                c.Branch == cart.Branch &&
                c.CustomerId == cart.CustomerId &&
                c.Status == cart.Status
            ));

            await _cartRepository.Received(1).UpdateAsync(cart, Arg.Any<CancellationToken>());
            response.Should().NotBeNull();
            response.Should().BeEquivalentTo(updateCartResult);
        }
    }
}
