using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

public class SaleRepository : ISaleRepository
{
    private readonly DefaultContext _context;

    public SaleRepository(DefaultContext context)
    {
        _context = context;
    }

    public async Task<Sale> CreateAsync(Sale sale, CancellationToken cancellationToken = default)
    {
        _context.Sales.Add(sale);
        await _context.SaveChangesAsync(cancellationToken);
        return sale;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var sale = await GetByIdAsync(id, cancellationToken);
        if (sale is null || sale.Status > Domain.Enums.SaleStatus.Active) return false;

        sale.Status = Domain.Enums.SaleStatus.Canceled;
        sale.ProductSales.ToList().ForEach(ps => 
            ps.Status = Domain.Enums.SaleStatus.Canceled);

        _context.Sales.Update(sale);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<Sale?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Sales
            .Include(ps=> ps.ProductSales)
            .ThenInclude(p => p.Product)
            .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
    }

    public Task<Sale> UpdateAsync(Sale sale, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}