using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PayWise.Application.DTOs;
using PayWise.Application.Interfaces;

namespace PayWise.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class WalletController : ControllerBase
    {
        private readonly IWalletService _walletService;

        public WalletController(IWalletService walletService)
        {
            _walletService = walletService;
        }

        //[HttpPost("{userId}")]
        //public async Task<IActionResult> CreateWallet(int userId, [FromBody] AddWalletDTO dto)
        //{
        //    var result = await _walletService.CreateWalletAsync(userId, dto);

        //    if (!result.Success)
        //        return BadRequest(new { message = result.ErrorMessage });

        //    return Ok(result.Data);
        //}

        [HttpGet("{id}")]
        public async Task<IActionResult> GetWalletById(int id)
        {
            var result = await _walletService.GetWalletByIdAsync(id);

            if (!result.Success)
                return NotFound(new { message = result.ErrorMessage });

            return Ok(result.Data);
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetWalletByUserId(int userId)
        {
            var result = await _walletService.GetWalletByUserIdAsync(userId);

            if (!result.Success)
                return NotFound(new { message = result.ErrorMessage });

            return Ok(result.Data);
        }
        [HttpGet("balance/{userId:int}")]
        public async Task<IActionResult> GetBalance(int userId)
        {
            var result = await _walletService.GetBalanceAsync(userId);
            return result.Success ? Ok(result.Data) : NotFound(result.ErrorMessage);
        }

        [HttpDelete("{userId:int}")]
        public async Task<IActionResult> DeleteWallet(int userId)
        {
            var result = await _walletService.DeleteWalletAsync(userId);
            return result.Success ? Ok(result.Data) : NotFound(result.ErrorMessage);
        }

        [HttpPost("{walletId:int}/deposit")]
        public async Task<IActionResult> Deposit(int walletId, [FromQuery] decimal amount)
        {
            var result = await _walletService.DepositAsync(walletId, amount);
            return result.Success ? Ok(result.Data) : BadRequest(result.ErrorMessage);
        }

        [HttpPost("{walletId:int}/withdraw")]
        public async Task<IActionResult> Withdraw(int walletId, [FromQuery] decimal amount)
        {
            var result = await _walletService.WithdrawAsync(walletId, amount);
            return result.Success ? Ok(result.Data) : BadRequest(result.ErrorMessage);
        }

        [HttpPost("transfer")]
        public async Task<IActionResult> Transfer([FromQuery] int fromWalletId, [FromQuery] int toWalletId, [FromQuery] decimal amount)
        {
            var result = await _walletService.TransferAsync(fromWalletId, toWalletId, amount);
            return result.Success ? Ok(result.Data) : BadRequest(result.ErrorMessage);
        }
    }
}
