using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PayWise.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly ILogger<TransactionsController> _logger;

        public TransactionsController(ILogger<TransactionsController> logger)
        {
            _logger = logger;
        }

        // GET: api/transactions
        [HttpGet]
        public IActionResult GetAllTransactions()
        {
            _logger.LogInformation("Fake: retrieving all transactions");
            return Ok(new[]
            {
                new { Id = 1, FromWalletId = 101, ToWalletId = 202, Amount = 50.00m, Timestamp = DateTime.UtcNow },
                new { Id = 2, FromWalletId = 202, ToWalletId = 101, Amount = 75.00m, Timestamp = DateTime.UtcNow }
            });
        }

        // POST: api/transactions/transfer
        [HttpPost("transfer")]
        public IActionResult Transfer([FromQuery] int fromWalletId, [FromQuery] int toWalletId, [FromQuery] decimal amount)
        {
            _logger.LogInformation("Fake: transferring {Amount} from {FromWallet} to {ToWallet}", amount, fromWalletId, toWalletId);

            return Ok(new
            {
                Message = "Fake transfer successful",
                FromWalletId = fromWalletId,
                ToWalletId = toWalletId,
                Amount = amount,
                Timestamp = DateTime.UtcNow
            });
        }

        // POST: api/transactions/deposit
        [HttpPost("deposit")]
        public IActionResult Deposit([FromQuery] int walletId, [FromQuery] decimal amount)
        {
            _logger.LogInformation("Fake: depositing {Amount} into wallet {WalletId}", amount, walletId);

            return Ok(new
            {
                Message = "Fake deposit successful",
                WalletId = walletId,
                Amount = amount,
                Timestamp = DateTime.UtcNow
            });
        }

        // POST: api/transactions/withdraw
        [HttpPost("withdraw")]
        public IActionResult Withdraw([FromQuery] int walletId, [FromQuery] decimal amount)
        {
            _logger.LogInformation("Fake: withdrawing {Amount} from wallet {WalletId}", amount, walletId);

            return Ok(new
            {
                Message = "Fake withdrawal successful",
                WalletId = walletId,
                Amount = amount,
                Timestamp = DateTime.UtcNow
            });
        }
    }
}
