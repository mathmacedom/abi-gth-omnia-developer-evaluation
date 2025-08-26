using Ambev.DeveloperEvaluation.Application.Sales.CancelSaleItem;
using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.Sales.TestData;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using FluentAssertions;
using MassTransit;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales
{
    public class CancelSaleItemHandlerTests
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IBus _bus;
        private readonly ILogger<CancelSaleItemHandler> _logger;
        private readonly CancelSaleItemHandler _handler;

        public CancelSaleItemHandlerTests()
        {
            _saleRepository = Substitute.For<ISaleRepository>();
            _bus = Substitute.For<IBus>();
            _logger = Substitute.For<ILogger<CancelSaleItemHandler>>();
            _handler = new CancelSaleItemHandler(_saleRepository, _bus, _logger);
        }

        [Fact(DisplayName = "Given valid request When cancelling sale item Then returns success response")]
        public async Task GivenValidRequest_WhenCancellingSaleItem_ThenReturnsSuccessResponse()
        {
            // Given
            var request = CancelSaleItemHandlerTestData.Get().Generate();
            var sale = SaleTestData.GenerateValidSale(request.SaleId);
            sale.AddItems(SaleItemTestData.GenerateValidSaleItems(3, sale.Id));
            sale.Items[0].Id = request.SaleItemId; // Ensure the sale has the item to be cancelled

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

            _saleRepository.GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(sale);
            _saleRepository.UpdateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>()).Returns(sale);

            // When
            var response = await _handler.Handle(request, CancellationToken.None);

            // Then
            response.Should().NotBeNull();
            response.Success.Should().Be(true);
            sale.Items[0].IsCancelled.Should().Be(true);
        }
    }
}
