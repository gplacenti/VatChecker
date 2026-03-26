using VatChecker.Models.Entities;

namespace VatChecker.Models.DTO;

public class VatResponse
{
    public bool Valid { get; set; }        // La partita IVA è valida secondo il WSDL
    public bool Saved { get; set; }        // Se il cliente è stato salvato nel DB
    public CustomerInfo? CustomerInfo { get; set; } // I dati del cliente salvato
}
