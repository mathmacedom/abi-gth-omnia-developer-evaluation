using Ambev.DeveloperEvaluation.Application.Carts.GetCart;
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
    public class GetCartHandlerTests
    {
        private readonly ICartRepository _cartRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetCartHandler> _logger;
        private readonly GetCartHandler _handler;

        public GetCartHandlerTests()
        {
            _cartRepository = Substitute.For<ICartRepository>();
            _mapper = Substitute.For<IMapper>();
            _logger = Substitute.For<ILogger<GetCartHandler>>();
            _handler = new GetCartHandler(_cartRepository, _mapper, _logger);
        }

        [Fact(DisplayName = "Given valid request When getting cart Then returns success response")]
        public async Task GivenValidRequest_WhenGettingCart_ThenReturnsSuccessResponse()
        {
            // Given
            var id = Guid.NewGuid();
            var request = GetCartHandlerTestData.Get().Generate();
            var cart = CartTestData.GenerateValidCart();
            var cartResult = new GetCartResult()
            {
                Id = cart.Id,
                CustomerId = cart.CustomerId,
                Branch = cart.Branch,
                Items = cart.Items.ConvertAll(ci => new CartItemResult() { Id = ci.Id, ProductId = ci.ProductId, ProductName = ci.ProductName, Quantity = ci.Quantity, UnitPrice = ci.UnitPrice, Subtotal = ci.Subtotal }),
                CreatedAt = cart.CreatedAt,
                Status = cart.Status
            };

            _cartRepository.GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(cart);
            _mapper.Map<GetCartResult>(cart).Returns(cartResult);

            // When
            var response = await _handler.Handle(request, CancellationToken.None);

            // Then
            _mapper.Received(1).Map<GetCartResult>(Arg.Is<Cart>(
                c => c.Id == cart.Id &&
                     c.CustomerId == cart.CustomerId &&
                     c.Branch == cart.Branch &&
                     c.Status == cart.Status &&
                     c.CreatedAt == cart.CreatedAt &&
                     c.Items == cart.Items));

            await _cartRepository.Received(1).GetByIdAsync(request.Id, Arg.Any<CancellationToken>());
            
            response.Should().NotBeNull();
            response.Should().BeEquivalentTo(cartResult);
        }
    }
}
