using Microsoft.AspNetCore.Mvc;
using VatChecker.Business;
using VatChecker.Contracts;
using VatChecker.Models;
using VatChecker.Models.DTO;

namespace VatChecker.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VatController : ControllerBase
{
    private readonly ICustomerService _customerService;

    public VatController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    /// <summary>
    /// Validate a VAT number and save it if valid
    /// </summary>
    [HttpPost("validate")]
    public async Task<ActionResult<CustomerValidationResult>> Validate([FromBody] VatRequest request)
    {
        try
        {
            var result = await _customerService.ValidateAndSaveCustomerAsync(request.CountryCode, request.VatNumber);

            if (!result.Valid || result.Customer == null)
                return BadRequest(new
                {
                    Message = $"Invalid VAT number {request.VatNumber} for country {request.CountryCode}"
                });

            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                Message = "An error occurred while validating the VAT number",
                Details = ex.Message
            });
        }
    }

    /// <summary>
    /// Query saved VAT numbers with optional filters
    /// </summary>
    [HttpPost("query")]
    public async Task<ActionResult<VatQueryResponse>> Query([FromBody] VatQueryRequest request)
    {
        try
        {
            var response = await _customerService.QueryCustomersAsync(request);
            return Ok(response);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                Message = "An error occurred while querying customers",
                Details = ex.Message
            });
        }
    }
}
