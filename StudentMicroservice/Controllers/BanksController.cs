using Microsoft.AspNetCore.Mvc;
using StudentMicroservice.Bank;

namespace StudentMicroservice.Controllers;

[Route("api/banks")]
[ApiController]
public class BanksController : ControllerBase
{
    private readonly IBankService _bankService;
    public BanksController(IBankService bankService)
    {
        _bankService = bankService;
    }
    [HttpGet("GetBanks")]
    [ProducesResponseType(typeof(BankInfo), 200)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> GetBanksAsync()
    {
        try
        {
            var banksEnvelope = await _bankService.GetAllBanksAsync();
            return Ok(banksEnvelope);
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Server error");
        }
    }
}
