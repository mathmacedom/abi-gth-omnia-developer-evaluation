using Ambev.DeveloperEvaluation.Application.Sales.CancelSale;
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
    public class CancelSaleHandlerTests
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IBus _bus;
        private readonly ILogger<CancelSaleHandler> _logger;
        private readonly CancelSaleHandler _handler;

        public CancelSaleHandlerTests()
        {
            _saleRepository = Substitute.For<ISaleRepository>();
            _bus = Substitute.For<IBus>();
            _logger = Substitute.For<ILogger<CancelSaleHandler>>();
            _handler = new CancelSaleHandler(_saleRepository, _bus, _logger);
        }

        [Fact(DisplayName = "Given valid request When cancelling sale Then returns success response")]
        public async Task GivenValidRequest_WhenCancellingSale_ThenReturnsSuccessResponse()
        {
            // Given
            var request = CancelSaleHandlerTestData.Get().Generate();
            var sale = SaleTestData.GenerateValidSale(request.Id);
            
            _saleRepository.GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(sale);
            _saleRepository.CancelAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);

            // When
            var response = await _handler.Handle(request, CancellationToken.None);

            // Then
            response.Should().NotBeNull();
            response.Success.Should().Be(true);
        }
    }
}
