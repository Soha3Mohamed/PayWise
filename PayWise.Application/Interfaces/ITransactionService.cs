using PayWise.Application.DTOs;
using PayWise.Core;
using PayWise.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayWise.Application.Interfaces
{
    public interface ITransactionService
    {
        Task<ServiceResult<TransactionResponseDTO>> GetTransactionByIdAsync(int id);
        Task<ServiceResult<IEnumerable<TransactionResponseDTO>>> GetTransactionsByWalletIdAsync(int walletId);
        Task<ServiceResult<IEnumerable<TransactionResponseDTO>>> GetAllAsync();
        Task<ServiceResult<TransactionResponseDTO>> LogTransactionAsync(Transaction transaction);
    }
}
