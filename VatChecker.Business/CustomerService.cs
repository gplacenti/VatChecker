using Microsoft.EntityFrameworkCore;
using VatChecker.Business.Services;
using VatChecker.Contracts;
using VatChecker.Models;
using VatChecker.Models.DTO;
using VatChecker.Models.Entities;

namespace VatChecker.Business;

public class CustomerService(
    IUnitOfWork unitOfWork, 
    VatWebServiceClient vatClient) : ICustomerService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly VatWebServiceClient _vatClient = vatClient;

    public async Task<CustomerValidationResult> ValidateAndSaveCustomerAsync(string countryCode, string vatNumber)
    {
        var result = new CustomerValidationResult();

        // 1️ Check in WSDL
        var response = await _vatClient.CheckVatAsync(countryCode, vatNumber);
        if (response == null || !response.Valid)
        {
            result.Valid = false;
            result.Saved = false;
            result.Customer = null;
            return result;
        }

        result.Valid = true;

        // 2️ Salvataggio in DB
        var customer = new CustomerInfo
        {
            CountryCode = countryCode,
            VatNumber = vatNumber,
            Name = response.Name,
            Address = response.Address
        };

        await _unitOfWork.Customers.AddAsync(customer);
        await _unitOfWork.SaveChangesAsync();

        result.Saved = true;
        result.Customer = customer;

        return result;
    }

    public async Task<VatQueryResponse> QueryCustomersAsync(VatQueryRequest request)
    {
        var query = _unitOfWork.Customers.Query();

        if (!string.IsNullOrEmpty(request.CountryCode))
            query = query.Where(c => c.CountryCode == request.CountryCode);

        if (!string.IsNullOrEmpty(request.VatNumber))
            query = query.Where(c => c.VatNumber.Contains(request.VatNumber));

        var totalCount = await query.CountAsync();

        int page = request.Page > 0 ? request.Page : 1;
        int pageSize = request.PageSize > 0 ? request.PageSize : 50;

        var items = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new VatQueryResponse
        {
            Customers = items,
            TotalCount = totalCount
        };
    }
}