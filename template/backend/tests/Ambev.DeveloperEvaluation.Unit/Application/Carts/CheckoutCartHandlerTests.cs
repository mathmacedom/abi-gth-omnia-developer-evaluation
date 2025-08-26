using Ambev.DeveloperEvaluation.Application.Carts.CheckoutCart;
using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
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
    public class CheckoutCartHandlerTests
    {
        private readonly ICartRepository _cartRepository;
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CheckoutCartHandler> _logger;
        private readonly CheckoutCartHandler _handler;

        public CheckoutCartHandlerTests()
        {
            _cartRepository = Substitute.For<ICartRepository>();
            _saleRepository = Substitute.For<ISaleRepository>();
            _mapper = Substitute.For<IMapper>();
            _logger = Substitute.For<ILogger<CheckoutCartHandler>>();
            _handler = new CheckoutCartHandler(_cartRepository, _saleRepository, _mapper, _logger);
        }

        [Fact(DisplayName = "Given valid request When checking out cart Then returns success response")]
        public async Task GivenValidRequest_WhenCheckingOutCart_ThenReturnsSuccessResponse()
        {
            // Given
            var request = CheckoutCartHandlerTestData.Get().Generate();
            var cart = CartTestData.GenerateValidCart(request.Id);
            var sale = SaleTestData.GenerateValidSale(customerId: cart.CustomerId, branch: cart.Branch);
            sale.AddItems(cart.Items.ConvertAll(i => new SaleItem(i.ProductId, i.ProductName, i.Quantity, i.UnitPrice)));

            var getSaleResult = new GetSaleResult()
            {
                Id = sale.Id,
                CustomerId = sale.CustomerId,
                Branch = sale.Branch,
                TotalAmount = sale.TotalAmount,
                TotalDiscount = sale.TotalDiscount,
                IsCancelled = sale.IsCancelled,
                CreatedAt = sale.CreatedAt,
                UpdatedAt = sale.UpdatedAt,
                CancelledAt = sale.CancelledAt,
                Status = sale.Status,
                Items = sale.Items.ConvertAll(i => new GetSaleItemResult()
                {
                    ProductId = i.ProductId,
                    ProductName = i.ProductName,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice,
                    Discount = i.Discount,
                    Total = i.Total,
                    IsCancelled = i.IsCancelled
                })
            };

            _mapper.Map<Cart>(request).Returns(cart);
            _mapper.Map<GetSaleResult>(Arg.Any<Sale>()).Returns(getSaleResult);
            _cartRepository.GetByIdAsync(request.Id, Arg.Any<CancellationToken>()).Returns(cart);
            _saleRepository.CreateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>()).Returns(sale);
            _cartRepository.UpdateAsync(Arg.Any<Cart>(), Arg.Any<CancellationToken>()).Returns(cart);

            // When
            var response = await _handler.Handle(request, CancellationToken.None);

            // Then
            _mapper.Received(1).Map<GetSaleResult>(Arg.Is<Sale>(c =>
                c.Id == sale.Id &&
                c.CustomerId == sale.CustomerId &&
                c.Branch == sale.Branch &&
                c.TotalAmount == sale.TotalAmount &&
                c.TotalDiscount == sale.TotalDiscount &&
                c.IsCancelled == sale.IsCancelled &&
                c.CreatedAt == sale.CreatedAt &&
                c.UpdatedAt == sale.UpdatedAt &&
                c.CancelledAt == sale.CancelledAt &&
                c.Status == sale.Status
            ));

            await _saleRepository.Received(1).CreateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>());
            await _cartRepository.Received(1).UpdateAsync(Arg.Any<Cart>(), Arg.Any<CancellationToken>());
            response.Should().NotBeNull();
            response.Status.Should().Be(SaleStatus.Open);
            response.Items.Should().NotBeEmpty();
            response.Items.Should().HaveCount(cart.Items.Count);
            cart.Status.Should().Be(CartStatus.Converted);
            response.Should().BeEquivalentTo(getSaleResult);
        }
    }
}
