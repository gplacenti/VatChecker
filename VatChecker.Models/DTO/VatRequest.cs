namespace VatChecker.Models.DTO;

public class VatRequest
{
    public string CountryCode { get; set; } = string.Empty;
    public string VatNumber { get; set; } = string.Empty;
}
