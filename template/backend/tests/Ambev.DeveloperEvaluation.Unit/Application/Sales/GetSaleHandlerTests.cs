using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales
{
    public class GetSaleHandlerTests
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetSaleHandler> _logger;
        private readonly GetSaleHandler _handler;

        public GetSaleHandlerTests()
        {
            _saleRepository = Substitute.For<ISaleRepository>();
            _mapper = Substitute.For<IMapper>();
            _logger = Substitute.For<ILogger<GetSaleHandler>>();
            _handler = new GetSaleHandler(_saleRepository, _mapper, _logger);
        }

        [Fact(DisplayName = "Given valid request When getting sale Then returns success response")]
        public async Task GivenValidRequest_WhenGettingSale_ThenReturnsSuccessResponse()
        {
            // Given
            var id = Guid.NewGuid();
            var request = new GetSaleCommand(id);
            var sale = SaleTestData.GenerateValidSale(id);
            sale.AddItems(SaleItemTestData.GenerateValidSaleItems(3, sale.Id));

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
            _mapper.Map<GetSaleResult>(sale).Returns(getSaleResult);

            // When
            var response = await _handler.Handle(request, CancellationToken.None);

            // Then
            _mapper.Received(1).Map<GetSaleResult>(Arg.Is<Sale>(p =>
                p.Id == getSaleResult.Id &&
                p.CustomerId == getSaleResult.CustomerId &&
                p.Branch == getSaleResult.Branch &&
                p.TotalAmount == getSaleResult.TotalAmount &&
                p.TotalDiscount == getSaleResult.TotalDiscount &&
                p.IsCancelled == getSaleResult.IsCancelled &&
                p.CreatedAt == getSaleResult.CreatedAt &&
                p.UpdatedAt == getSaleResult.UpdatedAt &&
                p.CancelledAt == getSaleResult.CancelledAt &&
                p.Status == getSaleResult.Status &&
                p.Items.Count == getSaleResult.Items.Count
            ));

            response.Should().NotBeNull();
            response.Should().BeEquivalentTo(getSaleResult);
        }
    }
}
