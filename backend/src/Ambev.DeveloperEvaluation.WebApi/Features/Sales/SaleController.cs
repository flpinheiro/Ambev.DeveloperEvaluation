﻿using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;
using Ambev.DeveloperEvaluation.Application.Sales.GetPaginatedSales;
using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Ambev.DeveloperEvaluation.Application.Sales.PatchProductSale;
using Ambev.DeveloperEvaluation.Application.Sales.PatchSale;
using Ambev.DeveloperEvaluation.Application.Sales.RemoveProductSale;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.DeleteSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetPaginatedSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.PatchProductSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.PatchSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.RemoveProductSale;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class SaleController : BaseController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public SaleController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(typeof(ApiResponseWithData<PaginatedList<GetPaginatedSaleResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetSale([FromQuery] GetPaginatedSaleRequest request, CancellationToken cancellationToken)
    {
        var validator = new PaginatedRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<GetPaginatedSalesCommand>(request);

        var result = await _mediator.Send(command, cancellationToken);

        var response = _mapper.Map<PaginatedList<GetPaginatedSaleResponse>>(result);

        return Ok(
            new ApiResponseWithData<PaginatedList<GetPaginatedSaleResponse>>()
            {
                Success = true,
                Message = "Sale retrieved successfully",
                Data = response
            });
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponseWithData<GetSaleResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetSale(Guid id, CancellationToken cancellationToken)
    {
        var request = new GetSaleRequest { Id = id };
        var validadator = new GetSaleRequestValidator();

        var validationResult = await validadator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<GetSaleCommand>(request);
        var response = await _mediator.Send(command, cancellationToken);

        return Ok(new ApiResponseWithData<GetSaleResponse>
        {
            Success = true,
            Message = "Sale retrieved successfully",
            Data = _mapper.Map<GetSaleResponse>(response)
        });
    }

    [HttpPost]
    [ProducesResponseType(typeof(ApiResponseWithData<CreateSaleResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateSale(CreateSaleRequest request, CancellationToken cancellationToken)
    {
        var validationResult = request.Validate();

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<CreateSaleCommand>(request);
        var response = await _mediator.Send(command, cancellationToken);

        return Ok(new ApiResponseWithData<CreateSaleResponse>
        {
            Success = true,
            Message = "Sale created successfully",
            Data = _mapper.Map<CreateSaleResponse>(response)
        });
    }
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteSale(Guid id, CancellationToken cancellationToken)
    {
        var request = new DeleteSaleRequest { Id = id };
        var validadator = new DeleteSaleRequestValidator();

        var validationResult = await validadator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<DeleteSaleCommand>(request);
        var response = await _mediator.Send(command, cancellationToken);

        if (!response) return BadRequest("Unable to Cancel Sale");

        return Ok(new ApiResponse
        {
            Success = true,
            Message = "Sale Calceled successfully",
        });
    }

    [HttpDelete("{id}/Product")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RemoveProductSale(Guid id, RemoveProductSaleRequest request,  CancellationToken cancellationToken) 
    {
        request.Id = id;

        var validationResult = request.Validate();

        if (!validationResult.IsValid) return BadRequest(validationResult.Errors);

        var command = _mapper.Map<RemoveProductSaleCommand>(request);

        var result = await  _mediator.Send(command, cancellationToken);

        if (!result) return BadRequest(new ApiResponse() { Success = false, Message = " unable to cancel product item form sale" });

        return Ok(new ApiResponse() { Success = true, Message = "Product Item canceled with success" });
    }

    [HttpPatch("{id}")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PatchSale(Guid id, CancellationToken cancellationToken)
    {
        var request = new PatchSaleRequest { Id = id };
        var validatorResult = request.Validate();
        if (!validatorResult.IsValid)
            return BadRequest(validatorResult.Errors);

        var command = _mapper.Map<PatchSaleCommand>(request);

        var result = await _mediator.Send(command, cancellationToken);

        if (!result) return BadRequest(new ApiResponse { Message = "Unable to update sale", Success = false });

        return Ok(new ApiResponse { Success = true, Message = "Sale updated successfully" });
    }

    [HttpPatch("{id}/Product")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PatchProductSale(Guid id, PatchProductSaleRequest request, CancellationToken cancellationToken) 
    {
        request.Id = id;
        var validationResult = request.Validate();

        if(!validationResult.IsValid) return BadRequest(validationResult.Errors);

        var command = _mapper.Map<PatchProductSaleCommand>(request);

        var result = await _mediator.Send(command, cancellationToken);

        if(!result) return BadRequest(new ApiResponse() { Success = false, Message = "Unable to update sale" });

        return Ok(new ApiResponse() { Success = true, Message = "Sale sucefully updated"});
    }
}