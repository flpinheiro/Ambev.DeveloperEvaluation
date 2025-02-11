using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using AutoMapper;
using MediatR;
using Rebus.Bus;
using SaleEntity = Ambev.DeveloperEvaluation.Domain.Entities.Sale;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

public class CreateSaleCommandHandler : IRequestHandler<CreateSaleCommand, CreateSaleResult>
{
    private readonly IMapper _mapper;
    private readonly IProductRepository _productRepository;
    private readonly ISaleRepository _saleRepository;
    private readonly IBus _bus;

    public CreateSaleCommandHandler(IProductRepository productRepository, ISaleRepository saleRepository, IMapper mapper, IBus bus)
    {
        _productRepository = productRepository;
        _saleRepository = saleRepository;
        _mapper = mapper;
        _bus = bus;
    }

    public async Task<CreateSaleResult> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
    {
        var products = await _productRepository.GetManyById(request.Products.Select(p => p.ProductId));

        var productSales = new List<ProductSale>();
        foreach (var product in products)
        {
            if (product != null)
            {
                var productSale = new ProductSale
                {
                    ProductId = product.Id,
                    Product = product,
                    Quantity = request.Products.First(p => p.ProductId == product.Id).Quantity
                };
                productSales.Add(productSale);
            }
        }

        var sale = new SaleEntity
        {
            Id = Guid.NewGuid(),
            ProductSales = productSales
        };
        sale.CalculateTotalValue();
        foreach (var productSale in productSales)
        {
            productSale.SaleId = sale.Id;
            productSale.Sale = sale;
        }

        await _saleRepository.CreateAsync(sale, cancellationToken);

        var result = _mapper.Map<CreateSaleResult>(sale);

        await _bus.Publish(new CreateSaleEvent() { Id = sale.Id });

        return result;
    }
}