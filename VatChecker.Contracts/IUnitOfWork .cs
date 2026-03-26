using VatChecker.Models.Entities;

namespace VatChecker.Contracts;

public interface IUnitOfWork : IDisposable
{
    IRepository<CustomerInfo> Customers { get; }
    Task<int> SaveChangesAsync();
}
