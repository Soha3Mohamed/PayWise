using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PayWise.Application.DTOs;
using PayWise.Application.Interfaces;
using PayWise.Core;

namespace PayWise.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly ILogger<TransactionsController> _logger;
        private readonly ITransactionService _transactionService;

        public TransactionsController(ILogger<TransactionsController> logger, ITransactionService transactionService)
        {
            _logger = logger;
            this._transactionService = transactionService;
        }

        // GET: api/transactions
        [HttpGet]
        public async Task<ActionResult<ServiceResult<IEnumerable<TransactionResponseDTO>>>> GetAll()
        {
            var result = await _transactionService.GetAllAsync();
            if (!result.Success)
                return NotFound(result);

            return Ok(result);
        }

        // GET: api/transactions/{id}
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ServiceResult<TransactionResponseDTO>>> GetById(int id)
        {
            var result = await _transactionService.GetTransactionByIdAsync(id);
            if (!result.Success)
                return NotFound(result);

            return Ok(result);
        }

        // GET: api/transactions/wallet/{walletId}
        [HttpGet("wallet/{walletId:int}")]
        public async Task<ActionResult<ServiceResult<IEnumerable<TransactionResponseDTO>>>> GetByWalletId(int walletId)
        {
            var result = await _transactionService.GetTransactionsByWalletIdAsync(walletId);
            if (!result.Success)
                return NotFound(result);

            return Ok(result);
        }
    }
    }

