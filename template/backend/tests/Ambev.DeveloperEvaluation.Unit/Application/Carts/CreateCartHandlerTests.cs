using Ambev.DeveloperEvaluation.Application.Carts.CreateCart;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.Carts.TestData;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using AutoMapper;
using Castle.Core.Resource;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Carts
{
    public class CreateCartHandlerTests
    {
        private readonly ICartRepository _cartRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateCartHandler> _logger;
        private readonly CreateCartHandler _handler;

        public CreateCartHandlerTests()
        {
            _cartRepository = Substitute.For<ICartRepository>();
            _mapper = Substitute.For<IMapper>();
            _logger = Substitute.For<ILogger<CreateCartHandler>>();
            _handler = new CreateCartHandler(_cartRepository, _mapper, _logger);
        }

        [Fact(DisplayName = "Given valid request When creating cart Then returns success response")]
        public async Task GivenValidRequest_WhenCreatingCart_ThenReturnsSuccessResponse()
        {
            // Given
            var request = CreateCartHandlerTestData.Get().Generate();
            var cart = CartTestData.GenerateValidCart();
            var createCartResult = new CreateCartResult
            {
                Id = cart.Id,
                Branch = cart.Branch,
                CustomerId = cart.CustomerId,
                Status = cart.Status
            };

            _mapper.Map<Cart>(request).Returns(cart);
            _mapper.Map<CreateCartResult>(cart).Returns(createCartResult);
            _cartRepository.CreateAsync(cart, Arg.Any<CancellationToken>()).Returns(cart);

            // When
            var response = await _handler.Handle(request, CancellationToken.None);

            // Then
            _mapper.Received(1).Map<Cart>(Arg.Is<CreateCartCommand>(c =>
                c.Branch == request.Branch &&
                c.CustomerId == request.CustomerId
            ));

            _mapper.Received(1).Map<CreateCartResult>(Arg.Is<Cart>(c =>
                c.Id == cart.Id &&
                c.Branch == cart.Branch &&
                c.CustomerId == cart.CustomerId &&
                c.Status == cart.Status
            ));

            await _cartRepository.Received(1).CreateAsync(cart, Arg.Any<CancellationToken>());
            response.Should().NotBeNull();
            response.Should().BeEquivalentTo(createCartResult);
        }
    }
}
