using VatChecker.Models.Entities;

namespace VatChecker.Models;

public class CustomerValidationResult
{
    /// <summary>
    /// True if the VAT number is valid
    /// </summary>
    public bool Valid { get; set; }

    /// <summary>
    /// True if the customer was successfully saved in the database
    /// </summary>
    public bool Saved { get; set; }

    /// <summary>
    /// Customer info saved in the database, null if validation failed
    /// </summary>
    public CustomerInfo? Customer { get; set; }
}
