using AutoMapper;
using Microsoft.Extensions.Logging;
using PayWise.Application.DTOs;
using PayWise.Application.Interfaces;
using PayWise.Core;
using PayWise.Core.Entities;
using PayWise.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace PayWise.Application.Implementation
{
    internal class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IWalletRepository _walletRepository;
        private readonly ILogger<TransactionService> _logger;
        private readonly IMapper _mapper;

        public TransactionService(
            ITransactionRepository transactionRepository,
            IWalletRepository walletRepository,
            ILogger<TransactionService> logger,
            IMapper mapper)
        {
            _transactionRepository = transactionRepository;
            _walletRepository = walletRepository;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<ServiceResult<IEnumerable<TransactionResponseDTO>>> GetTransactionsByWalletIdAsync(int walletId)
        {
            var transactions = await _transactionRepository.GetByWalletIdAsync(walletId);
            if (!transactions.Any())
                return ServiceResult<IEnumerable<TransactionResponseDTO>>.Fail("No transactions found");

            var dtoList = _mapper.Map<IEnumerable<TransactionResponseDTO>>(transactions);
            return ServiceResult<IEnumerable<TransactionResponseDTO>>.Ok(dtoList);
        }

        public async Task<ServiceResult<TransactionResponseDTO>> GetTransactionByIdAsync(int id)
        {
            var transaction = await _transactionRepository.GetByIdAsync(id);
            if (transaction == null)
                return ServiceResult<TransactionResponseDTO>.Fail("Transaction not found");

            return ServiceResult<TransactionResponseDTO>.Ok(_mapper.Map<TransactionResponseDTO>(transaction));
        }

        public async Task<ServiceResult<IEnumerable<TransactionResponseDTO>>> GetAllAsync()
        {

            var transactions = await _transactionRepository.GetAllAsync();
            if (!transactions.Any())
                return ServiceResult<IEnumerable<TransactionResponseDTO>>.Fail("No transactions found");

            var dtoList = _mapper.Map<IEnumerable<TransactionResponseDTO>>(transactions);
            return ServiceResult<IEnumerable<TransactionResponseDTO>>.Ok(dtoList);
        }

        public async Task<ServiceResult<TransactionResponseDTO>> LogTransactionAsync(Core.Entities.Transaction transaction)
        {
            await _transactionRepository.AddAsync(transaction);
            await _transactionRepository.SaveChangesAsync();
            var dto = _mapper.Map<TransactionResponseDTO>(transaction);
            return ServiceResult<TransactionResponseDTO>.Ok(dto);
        }
    }
}
