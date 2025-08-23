using Ambev.DeveloperEvaluation.Application.Users.CreateUser;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

/// <summary>
/// Handler for processing <see cref="CreateSaleCommand"/> requests.
/// </summary>
public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, CreateSaleResult>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of <see cref="CreateSaleHandler"/>.
    /// </summary>
    /// <param name="saleRepository">The repository for managing sales.</param>
    /// <param name="mapper">The AutoMapper instance for mapping objects.</param>
    public CreateSaleHandler(ISaleRepository saleRepository, IMapper mapper)
    {
        _saleRepository = saleRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the <see cref="CreateSaleCommand"/> request.
    /// </summary>
    /// <param name="command">The create sale command containing the sale data.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The details of the created sale.</returns>
    public async Task<CreateSaleResult> Handle(CreateSaleCommand command, CancellationToken cancellationToken)
    {
        var validator = new CreateSaleCommandValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        // Map command to domain entity
        var sale = _mapper.Map<Sale>(command);

        // Persist sale
        var createdSale = await _saleRepository.CreateAsync(sale, cancellationToken);

        // Map result to response DTO
        return _mapper.Map<CreateSaleResult>(createdSale);
    }
}
