﻿using Ambev.DeveloperEvaluation.Application.Sale.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using Ambev.DeveloperEvaluation.Unit.Fixtures;
using AutoMapper;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application;

public class GetSaleHandlerTest
{
    private readonly GetSaleHandler _handler;

    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;

    public GetSaleHandlerTest()
    {
        _saleRepository = Substitute.For<ISaleRepository>();
        _mapper = MapperFixture.CreateMapper(new GetSaleProfile(), new CreateSaleProfile());

        _handler = new GetSaleHandler(_saleRepository, _mapper);
    }

    [Fact]
    public async Task Should_Return_Valid_Sale_When_SaleExist()
    {
        var sale = SaleTestData.GenerateValidSale();

        _saleRepository.GetByIdAsync(sale.Id).Returns(sale);

        var command =  new GetSaleCommand() 
        {
            Id = sale.Id
        };

        var result = await _handler.Handle(command, default);

        Assert.NotNull(result);
        Assert.Equal(sale.Id, result.Id);
    }
}
