using Ambev.DeveloperEvaluation.Application.Carts.AddCartItem;
using Ambev.DeveloperEvaluation.Application.Carts.CreateCart;
using Ambev.DeveloperEvaluation.Application.Carts.DeleteCartItem;
using Ambev.DeveloperEvaluation.Application.Carts.GetCart;
using Ambev.DeveloperEvaluation.Application.Carts.GetCartByCustomerId;
using Ambev.DeveloperEvaluation.Application.Carts.UpdateCartItem;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts.AddCartItem;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts.CreateCart;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts.DeleteCartItem;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts.GetCart;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts.GetCartByCustomerId;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts.UpdateCartItem;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts;

/// <summary>
/// Controller for managing carts operations
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class CartsController : BaseController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly ILogger<CartsController> _logger;

    /// <summary>
    /// Initializes a new instance of CartsController
    /// </summary>
    /// <param name="mediator">The mediator instance</param>
    /// <param name="mapper">The AutoMapper instance</param>
    /// <param name="logger">The logger instance</param>
    public CartsController(IMediator mediator, IMapper mapper, ILogger<CartsController> logger)
    {
        _mediator = mediator;
        _mapper = mapper;
        _logger=logger;
    }

    [HttpGet]
    public string Teste() => "OK";

    /// <summary>
    /// Retrieves a cart by its ID
    /// </summary>
    /// <param name="id">The unique identifier of the cart</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The cart details if found</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponseWithData<GetCartResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCart([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling {GetCartRequest}", nameof(GetCartRequest));

        var request = new GetCartRequest { Id = id };
        var validator = new GetCartRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<GetCartCommand>(request.Id);
        var response = await _mediator.Send(command, cancellationToken);

        _logger.LogInformation("Cart retrieved successfully with ID: {Id}", id);

        _logger.LogInformation("Handled {GetCartRequest} successfully", nameof(GetCartRequest));
        return Ok(new ApiResponseWithData<GetCartResponse>
        {
            Success = true,
            Message = "Cart retrieved successfully",
            Data = _mapper.Map<GetCartResponse>(response)
        });
    }

    /// <summary>
    /// Retrieves a cart by its customer ID
    /// </summary>
    /// <param name="id">The unique identifier of the cart</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The cart details if found</returns>
    //[HttpGet("/customer/{id}")]
    //[ProducesResponseType(typeof(ApiResponseWithData<GetCartByCustomerIdResponse>), StatusCodes.Status200OK)]
    //[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    //[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    //public async Task<IActionResult> GetCartByCustomerId([FromRoute] Guid id, CancellationToken cancellationToken)
    //{
    //    _logger.LogInformation("Handling {GetCartByCustomerIdRequest}", nameof(GetCartByCustomerIdRequest));

    //    var request = new GetCartByCustomerIdRequest { CustomerId = id };
    //    var validator = new GetCartByCustomerIdRequestValidator();
    //    var validationResult = await validator.ValidateAsync(request, cancellationToken);

    //    if (!validationResult.IsValid)
    //        return BadRequest(validationResult.Errors);

    //    var command = _mapper.Map<GetCartByCustomerIdCommand>(request.CustomerId);
    //    var response = await _mediator.Send(command, cancellationToken);

    //    _logger.LogInformation("Cart retrieved successfully with customer ID: {Id}", id);

    //    _logger.LogInformation("Handled {GetCartByCustomerIdRequest} successfully", nameof(GetCartByCustomerIdRequest));
    //    return Ok(new ApiResponseWithData<GetCartByCustomerIdResponse>
    //    {
    //        Success = true,
    //        Message = "Cart retrieved successfully",
    //        Data = _mapper.Map<GetCartByCustomerIdResponse>(response)
    //    });
    //}

    /// <summary>
    /// Creates a new cart
    /// </summary>
    /// <param name="request">The cart creation request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created cart details</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponseWithData<CreateCartResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateCart([FromBody] CreateCartRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling {CreateCartRequest}", nameof(CreateCartRequest));

        var validator = new CreateCartRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<CreateCartCommand>(request);
        var response = await _mediator.Send(command, cancellationToken);

        _logger.LogInformation("Cart created successfully");

        _logger.LogInformation("Handled {CreateCartRequest} successfully", nameof(CreateCartRequest));
        return Created(string.Empty, new ApiResponseWithData<CreateCartResponse>
        {
            Success = true,
            Message = "Cart created successfully",
            Data = _mapper.Map<CreateCartResponse>(response)
        });
    }

    /// <summary>
    /// Add an cart item
    /// </summary>
    /// <param name="id">The cart identifier</param>
    /// <param name="request">The add request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The updated cart details</returns>
    [HttpPut("{id}/items")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddCartItem([FromRoute] Guid id, [FromBody] AddCartItemRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling {AddCartItemRequest}", nameof(AddCartItemRequest));
        request.CartId = id;

        var validator = new AddCartItemRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<AddCartItemCommand>(request);
        var response = await _mediator.Send(command, cancellationToken);

        _logger.LogInformation("Cart item added successfully...");

        _logger.LogInformation("Handled {AddCartItemRequest} successfully", nameof(AddCartItemRequest));
        return Ok(new ApiResponseWithData<AddCartItemResponse>
        {
            Success = true,
            Message = "Cart item added successfully",
            Data = _mapper.Map<AddCartItemResponse>(response)
        });
    }

    /// <summary>
    /// Updates an existing cart item
    /// </summary>
    /// <param name="id">The cart identifier</param>
    /// <param name="itemId">The cart item identifier</param>
    /// <param name="request">The update request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The updated cart details</returns>
    [HttpPut("{id}/items/{itemId}")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateCartItem([FromRoute] Guid id, [FromRoute] Guid itemId, [FromBody] UpdateCartItemRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling {UpdateCartItemRequest}", nameof(UpdateCartItemRequest));
        request.CartId = id;
        request.CartItemId = itemId;

        var validator = new UpdateCartItemRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<UpdateCartItemCommand>(request);
        var response = await _mediator.Send(command, cancellationToken);

        _logger.LogInformation("Cart updated successfully with ID: {Id}", id);

        _logger.LogInformation("Handled {UpdateCartItemRequest} successfully", nameof(UpdateCartItemRequest));
        return Ok(new ApiResponseWithData<UpdateCartItemResponse>
        {
            Success = true,
            Message = "Cart item updated successfully",
            Data = _mapper.Map<UpdateCartItemResponse>(response)
        });
    }

    /// <summary>
    /// Delete a cart item by its ID
    /// </summary>
    /// <param name="id">The unique identifier of the cart to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Success response if the cart item was deleted</returns>
    [HttpDelete("{id}/items/{itemId}")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteCartItem([FromRoute] Guid id, [FromRoute] Guid itemId, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling {DeleteCartItemRequest}", nameof(DeleteCartItemRequest));

        var request = new DeleteCartItemRequest { CartId = id, CartItemId = itemId };
        var validator = new DeleteCartItemRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<DeleteCartItemCommand>(request);
        await _mediator.Send(command, cancellationToken);

        _logger.LogInformation("Cart item deleted successfully with ID: {Id}", id);

        _logger.LogInformation("Handled {DeleteCartItemRequest} successfully", nameof(DeleteCartItemRequest));
        return Ok(new ApiResponse
        {
            Success = true,
            Message = "Cart item deleted successfully"
        });
    }
}
