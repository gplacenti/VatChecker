using VatChecker.Models.Entities;

namespace VatChecker.Models.DTO;

public class VatQueryRequest
{
    public string? CountryCode { get; set; }
    public string? VatNumber { get; set; }

    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 50;
}

public class VatQueryResponse
{
    public int TotalCount { get; set; }
    public List<CustomerInfo> Customers { get; set; } = new();
}
