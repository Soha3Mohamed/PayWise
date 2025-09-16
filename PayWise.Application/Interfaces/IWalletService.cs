using PayWise.Application.Common;
using PayWise.Application.DTOs;
using PayWise.Core;
using PayWise.Core.Entities;

namespace PayWise.Application.Interfaces
{
    public interface IWalletService
    {
        // Basic Wallet CRUD
        Task<ServiceResult<WalletResponseDTO>> CreateWalletAsync(int userId, AddWalletDTO addWalletDTO);
        Task<ServiceResult<WalletResponseDTO>> GetWalletByIdAsync(int id);
        Task<ServiceResult<WalletResponseDTO>> GetWalletByUserIdAsync(int userId);
        Task<ServiceResult<IEnumerable<WalletResponseDTO>>> GetAllWalletsAsync();
        Task<ServiceResult<bool>> DeleteWalletAsync(int id);

        // Business operations
        Task<ServiceResult<decimal>> GetBalanceAsync(int walletId);
        Task<ServiceResult<decimal>> DepositAsync(int walletId, decimal amount);
        Task<ServiceResult<decimal>> WithdrawAsync(int walletId, decimal amount);

        // Transfer money between wallets
        Task<ServiceResult<bool>> TransferAsync(int fromWalletId, int toWalletId, decimal amount);
    }
}
