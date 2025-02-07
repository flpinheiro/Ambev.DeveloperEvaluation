﻿using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData;

public static class CreateProductHandlerTestData
{
    private static readonly Faker<CreateProductCommand> CommandFaker = new Faker<CreateProductCommand>()
        .RuleFor(p => p.Name, f => f.Commerce.Product())
        .RuleFor(p => p.Description, f => f.Commerce.ProductDescription())
        .RuleFor(p => p.Price, f => f.Random.Decimal(1, 1000))
        .RuleFor(p => p.Quantity, f => f.Random.Number(1, 100));

    private static readonly Faker<CreateProductResult> ResultFaker = new Faker<CreateProductResult>()
    .RuleFor(p => p.Id, faker => faker.Random.Guid())
    .RuleFor(p => p.Name, f => f.Commerce.Product())
    .RuleFor(p => p.Description, f => f.Commerce.ProductDescription())
    .RuleFor(p => p.Price, f => f.Random.Decimal(1, 1000))
    .RuleFor(p => p.Quantity, f => f.Random.Number(1, 100));

    public static CreateProductCommand CreateProductCommand() => CommandFaker.Generate();

    public static CreateProductResult CreateProductResult() => ResultFaker.Generate();

}
