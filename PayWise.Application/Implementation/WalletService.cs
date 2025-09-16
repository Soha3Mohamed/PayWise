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

namespace PayWise.Application.Implementation
{
    internal class WalletService : IWalletService
    {
        private readonly IWalletRepository _walletRepository;
        private readonly ILogger<WalletService> _logger;
        private readonly IMapper _mapper;

        public WalletService(IWalletRepository walletRepository , ILogger<WalletService> logger, IMapper mapper)
        {
            this._walletRepository = walletRepository;
            this._logger = logger;
            this._mapper = mapper;
        }

        public async Task<ServiceResult<IEnumerable<WalletResponseDTO>>> GetAllWalletsAsync()
        {
            var wallets = await _walletRepository.GetAllWalletsAsync();
            if (wallets == null || !wallets.Any())
            {
                _logger.LogWarning("No users found in the system");
                return ServiceResult<IEnumerable<WalletResponseDTO>>.Fail("No wallets found");
            }

            _logger.LogInformation("Retrieved {Count} users", wallets.Count());

            var dtoList = _mapper.Map<IEnumerable<WalletResponseDTO>>(wallets);
            return ServiceResult<IEnumerable<WalletResponseDTO>>.Ok(dtoList);
        }

        public async Task<ServiceResult<decimal>> GetBalanceAsync(int userId)
        {
            var wallet = await _walletRepository.GetWalletByUserIdAsync(userId);
            if (wallet == null)
            {
                _logger.LogWarning("Wallet with user id {Id} not found", userId);
                return ServiceResult<decimal>.Fail($"Wallet with user id {userId} not found");
            }
            _logger.LogInformation("Retrieved user wallet {userID} balance {balance}", userId, wallet.Balance);
            return ServiceResult<decimal>.Ok(wallet.Balance);
        }

        public async Task<ServiceResult<WalletResponseDTO>> GetWalletByIdAsync(int id)
        {
            var wallet = await _walletRepository.GetWalletByIdAsync(id);
            if (wallet == null)
            {
                _logger.LogWarning("No wallet was found with this id {id}", id);
                return ServiceResult<WalletResponseDTO>.Fail($"Wallet with id {id} not found");
            }

            _logger.LogInformation("Retrieved wallet with Id: {id}", wallet.Id);

            var dto = _mapper.Map<WalletResponseDTO>(wallet);
            return ServiceResult<WalletResponseDTO>.Ok(dto);
        }

        public async Task<ServiceResult<WalletResponseDTO>> GetWalletByUserIdAsync(int userId)
        {
            var wallet = await _walletRepository.GetWalletByUserIdAsync(userId);
            if (wallet == null)
            {
                _logger.LogWarning("No wallet was found with this user Id: {id}", userId);
                return ServiceResult<WalletResponseDTO>.Fail($"Wallet with user id: {userId} is not found");
            }

            _logger.LogInformation("Retrieved wallet with user Id: {id}", wallet.UserId);

            var dto = _mapper.Map<WalletResponseDTO>(wallet);
            return ServiceResult<WalletResponseDTO>.Ok(dto);
        }

        public async Task<ServiceResult<WalletResponseDTO>> CreateWalletAsync(int userId, AddWalletDTO addWalletDTO)
        {
           
            var existingWallet = await _walletRepository.GetWalletByUserIdAsync(userId);
            if (existingWallet != null)
            {
                _logger.LogWarning("User {UserId} already has a wallet", userId);
                return ServiceResult<WalletResponseDTO>.Fail($"User {userId} already has a wallet");
            }

            
            var wallet = new Wallet
            {
                UserId = userId,
                Balance = addWalletDTO.Balance,
                CreatedAt = DateTime.UtcNow
            };

            await _walletRepository.AddWalletAsync(wallet);
            await _walletRepository.SaveChangesAsync();

            var dto = _mapper.Map<WalletResponseDTO>(wallet);

            _logger.LogInformation("Wallet created successfully for User {UserId}", userId);

            return ServiceResult<WalletResponseDTO>.Ok(dto);
        }


        public async Task<ServiceResult<bool>> DeleteWalletAsync(int userId)
        {
            var wallet = await _walletRepository.GetWalletByUserIdAsync(userId);
            if (wallet == null)
            {
                _logger.LogWarning("No wallet was found with this user id {id}", userId);
                return ServiceResult<bool>.Fail($"Wallet with user id {userId} not found");
            }

            await _walletRepository.DeleteWalletAsync(userId);
            await _walletRepository.SaveChangesAsync();
            _logger.LogInformation("Wallet with user id {Id} deleted successfully", userId);
            return ServiceResult<bool>.Ok(true);
        }

        public async Task<ServiceResult<decimal>> DepositAsync(int walletId, decimal amount)
        {
            var wallet = await _walletRepository.GetWalletByIdAsync(walletId);
            if (wallet == null)
            {
                _logger.LogWarning("No wallet was found with this id {id}", walletId);
                return ServiceResult<decimal>.Fail($"Wallet with id {walletId} not found");
            }
            if(amount == 0)
            {
                return ServiceResult<decimal>.Fail($"The amount is 0, it must be positive number");
            }
            wallet.Balance += amount;
            await _walletRepository.UpdateWalletAsync(wallet);
            await _walletRepository.SaveChangesAsync();
            _logger.LogInformation("Wallet with id {Id}, Balance Updated successfully +{amount}", walletId, amount);
            return ServiceResult<decimal>.Ok(wallet.Balance);
        }


        public async Task<ServiceResult<bool>> TransferAsync(int fromWalletId, int toWalletId, decimal amount)
        {
            var success = await _walletRepository.TransferAsync(fromWalletId, toWalletId, amount);
            return success
                ? ServiceResult<bool>.Ok(true)
                : ServiceResult<bool>.Fail("Transfer failed");
        }


        public async Task<ServiceResult<decimal>> WithdrawAsync(int walletId, decimal amount)
        {
            var wallet = await _walletRepository.GetWalletByIdAsync(walletId);
            if (wallet == null)
            {
                _logger.LogWarning("No wallet was found with this id {id}", walletId);
                return ServiceResult<decimal>.Fail($"Wallet with id {walletId} not found");
            }
            if (amount == 0)
            {
                return ServiceResult<decimal>.Fail($"The amount id 0, it must be positive number");
            }
            if(wallet.Balance < amount)
            {
                return ServiceResult<decimal>.Fail($"The amount {amount} is bigger than your balance");
            }
            wallet.Balance -= amount;
            await _walletRepository.UpdateWalletAsync(wallet);
            await _walletRepository.SaveChangesAsync();
            _logger.LogInformation("Wallet with id {Id}, Balance Updated successfully -{amount}", walletId, amount);
            return ServiceResult<decimal>.Ok(wallet.Balance);
        }
    }
}
