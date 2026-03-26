using VatChecker.Models;
using VatChecker.Models.DTO;

namespace VatChecker.Contracts;

public interface ICustomerService
{
    Task<CustomerValidationResult> ValidateAndSaveCustomerAsync(string countryCode, string vatNumber);
    Task<VatQueryResponse> QueryCustomersAsync(VatQueryRequest request);
}
