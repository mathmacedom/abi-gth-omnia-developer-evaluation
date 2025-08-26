using Ambev.DeveloperEvaluation.Application.Carts.GetCartByCustomerId;
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
    public class GetCartByCustomerIdHandlerTests
    {
        private readonly ICartRepository _cartRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetCartByCustomerIdHandler> _logger;
        private readonly GetCartByCustomerIdHandler _handler;

        public GetCartByCustomerIdHandlerTests()
        {
            _cartRepository = Substitute.For<ICartRepository>();
            _mapper = Substitute.For<IMapper>();
            _logger = Substitute.For<ILogger<GetCartByCustomerIdHandler>>();
            _handler = new GetCartByCustomerIdHandler(_cartRepository, _mapper, _logger);
        }

        [Fact(DisplayName = "Given valid request When getting cart by customer id Then returns success response")]
        public async Task GivenValidRequest_WhenGettingCartByCustomerId_ThenReturnsSuccessResponse()
        {
            // Given
            var request = GetCartByCustomerIdHandlerTestData.Get().Generate();
            var cart = CartTestData.GenerateValidCart();
            var cartResult = new GetCartByCustomerIdResult()
            {
                Id = cart.Id,
                CustomerId = request.CustomerId,
                Branch = cart.Branch,
                Items = cart.Items.ConvertAll(ci => new CartItemResult() { Id = ci.Id, ProductId = ci.ProductId, ProductName = ci.ProductName, Quantity = ci.Quantity, UnitPrice = ci.UnitPrice, Subtotal = ci.Subtotal }),
                CreatedAt = cart.CreatedAt,
                Status = cart.Status
            };

            _cartRepository.GetByCustomerIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(cart);
            _mapper.Map<GetCartByCustomerIdResult>(cart).Returns(cartResult);

            // When
            var response = await _handler.Handle(request, CancellationToken.None);

            // Then
            _mapper.Received(1).Map<GetCartByCustomerIdResult>(Arg.Is<Cart>(
                c => c.Id == cart.Id &&
                     c.CustomerId == cart.CustomerId &&
                     c.Branch == cart.Branch &&
                     c.Status == cart.Status &&
                     c.CreatedAt == cart.CreatedAt &&
                     c.Items == cart.Items));

            response.Should().NotBeNull();
            response.CustomerId.Should().Be(request.CustomerId);
            response.Should().BeEquivalentTo(cartResult);
        }
    }
}
