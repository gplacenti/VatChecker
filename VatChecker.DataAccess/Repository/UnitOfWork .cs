using VatChecker.Contracts;
using VatChecker.Models.Entities;

namespace VatChecker.DataAccess.Repository;

public class UnitOfWork : IUnitOfWork
{
    private readonly VatCheckerDbContext _context;
    private IRepository<CustomerInfo>? _customers;

    public UnitOfWork(VatCheckerDbContext context)
    {
        _context = context;
    }

    public IRepository<CustomerInfo> Customers => _customers ??= new Repository<CustomerInfo>(_context);

    public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();

    public void Dispose() => _context.Dispose();
}
