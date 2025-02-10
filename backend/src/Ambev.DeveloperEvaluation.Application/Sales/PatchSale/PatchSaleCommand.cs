using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.PatchSale;

public class PatchSaleCommand :IRequest<bool>
{
    public Guid Id { get; set; }
}

public class PatchSaleCommandHandler : IRequestHandler<PatchSaleCommand, bool>
{
    private readonly ISaleRepository _saleRepository;

    public PatchSaleCommandHandler(ISaleRepository saleRepository)
    {
        _saleRepository = saleRepository;
    }

    public async Task<bool> Handle(PatchSaleCommand request, CancellationToken cancellationToken)
    {
        var sale = await _saleRepository.GetByIdAsync(request.Id, cancellationToken);

        if (sale == null || sale.Status == SaleStatus.Finished) return false;

        foreach (var ps in sale.ProductSales) 
        {
            if (sale.Status == ps.Status) ps.Status++;
        }

        sale.Status++;

        await _saleRepository.UpdateAsync(sale, cancellationToken);

        return true;
    }
}
