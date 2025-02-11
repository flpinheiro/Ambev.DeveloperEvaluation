using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Dtos;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

public class SaleRepository : ISaleRepository
{
    private readonly DefaultContext _context;
    private readonly IUserService _userService;
    public SaleRepository(DefaultContext context, IUserService userService)
    {
        _context = context;
        _userService = userService;
    }

    public async Task<Sale?> CreateAsync(Sale sale, CancellationToken cancellationToken = default)
    {
        if (sale == null) return null;
        if (_userService.UserId != Guid.Empty) sale.UserId = _userService.UserId;

        _context.Sales.Add(sale);

        await _context.SaveChangesAsync(cancellationToken);
        return sale;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var sale = await GetByIdAsync(id, cancellationToken);
        if (sale is null || sale.Status > SaleStatus.Active || sale.Status == SaleStatus.Canceled) return false;

        sale.Status = SaleStatus.Canceled;
        sale.ProductSales.ToList().ForEach(ps => ps.Status = SaleStatus.Canceled);

        await UpdateAsync(sale, cancellationToken);
        return true;
    }

    public async Task<PaginatedList<Sale>> GetAsync(GetPaginatedSaleDto request, CancellationToken cancellationToken = default)
    {
        return await _context.Sales
            .Where(s => s.UserId == _userService.UserId)
            .ToPaginatedListAsync(request.PageNumber, request.PageSize);
    }

    public async Task<Sale?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Sales
            .Include(ps => ps.ProductSales)
            .ThenInclude(p => p.Product)
            .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
    }

    public async Task<Sale?> UpdateAsync(Sale sale, CancellationToken cancellationToken = default)
    {
        var date = DateTime.UtcNow;
        sale.UpdatedAt = date;
        sale.ProductSales.ToList().ForEach(p => p.UpdatedAt = date);

        _context.Entry(sale).State = EntityState.Modified;

        _context.Sales.Update(sale);

        await _context.SaveChangesAsync(cancellationToken);

        return sale;
    }
}