using VatChecker.Models.DTO;
using VatChecker.WSDL;

namespace VatChecker.Business.Services;

public class VatWebServiceClient(checkVatPortTypeClient client)
{
    private readonly checkVatPortTypeClient _client = client;

    public async Task<VatCheckResult?> CheckVatAsync(string countryCode, string vatNumber)
    {
        if (string.IsNullOrWhiteSpace(countryCode) 
            || string.IsNullOrWhiteSpace(vatNumber))
            return null;

        try
        {
            var request = new checkVatRequest
            {
                countryCode = countryCode,
                vatNumber = vatNumber
            };

            var response = await _client.checkVatAsync(request);

            if (response == null || !response.valid)
                return new VatCheckResult { Valid = false };

            return new VatCheckResult
            {
                Valid = true,
                Name = response.name ?? string.Empty,
                Address = response.address ?? string.Empty
            };
        }
        catch
        {
            return null;
        }
    }
}