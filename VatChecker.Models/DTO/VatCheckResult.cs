namespace VatChecker.Models.DTO;

public class VatCheckResult
{
    public bool Valid { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
}
