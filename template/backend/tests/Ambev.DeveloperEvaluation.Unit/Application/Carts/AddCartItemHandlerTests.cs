using Ambev.DeveloperEvaluation.Application.Carts.AddCartItem;
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
    public class AddCartItemHandlerTests
    {
        private readonly ICartRepository _cartRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<AddCartItemHandler> _logger;
        private readonly AddCartItemHandler _handler;

        public AddCartItemHandlerTests()
        {
            _cartRepository = Substitute.For<ICartRepository>();
            _productRepository = Substitute.For<IProductRepository>();
            _mapper = Substitute.For<IMapper>();
            _logger = Substitute.For<ILogger<AddCartItemHandler>>();
            _handler = new AddCartItemHandler(_cartRepository, _productRepository, _mapper, _logger);
        }

        [Fact(DisplayName = "Given valid request When adding cart item Then returns success response")]
        public async Task GivenValidRequest_WhenAddingCartItem_ThenReturnsSuccessResponse()
        {
            // Given
            var request = AddCartItemHandlerTestData.Get().Generate();
            var product = ProductTestData.GenerateValidProduct();
            var cart = CartTestData.GenerateValidCart();
            cart.Items.Clear();
            var cartItem = new CartItem(cart.Id, product.Id, product.Name, 1, product.UnitPrice);
            cart.Items.Add(cartItem);

            var addCartResult = new AddCartItemResult
            {
                Id = cart.Id,
                Branch = cart.Branch,
                CustomerId = cart.CustomerId,
                Items = cart.Items.ConvertAll(ci => new CartItemResult() { Id = ci.Id, ProductId = ci.ProductId, ProductName = ci.ProductName, Quantity = ci.Quantity, UnitPrice = ci.UnitPrice, Subtotal = ci.Subtotal }),
                Status = cart.Status
            };

            _mapper.Map<AddCartItemResult>(cart).Returns(addCartResult);
            _cartRepository.GetByIdAsync(request.CartId, Arg.Any<CancellationToken>()).Returns(cart);
            _productRepository.GetByIdAsync(request.ProductId, Arg.Any<CancellationToken>()).Returns(product);
            _cartRepository.UpdateAsync(cart, Arg.Any<CancellationToken>()).Returns(cart);

            // When
            var response = await _handler.Handle(request, CancellationToken.None);

            // Then
            _mapper.Received(1).Map<AddCartItemResult>(Arg.Is<Cart>(c =>
                c.Id == cart.Id &&
                c.Branch == cart.Branch &&
                c.CustomerId == cart.CustomerId &&
                c.Status == cart.Status &&
                c.Items == cart.Items
            ));

            await _cartRepository.Received(1).UpdateAsync(cart, Arg.Any<CancellationToken>());
            response.Should().NotBeNull();
            response.Should().BeEquivalentTo(addCartResult);
        }
    }
}
