using Microsoft.EntityFrameworkCore;
using PayWise.Application.Interfaces;
using PayWise.Core.Entities;
using PayWise.Core.Interfaces;
using PayWise.Infrastructure.Contexts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PayWise.Infrastructure.Repositories
{
    public class WalletRepository : IWalletRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ITransactionRepository _transactionRepo;

        public WalletRepository(ApplicationDbContext dbContext, ITransactionRepository transactionRepo)
        {
            _dbContext = dbContext;
            this._transactionRepo = transactionRepo;
        }
        //i am not saving changes separately in every method because in the service there is a scenario where i create user then create his wallet
          //and then i want to save changes so 
        public async Task<IEnumerable<Wallet>> GetAllWalletsAsync()
        {
            return await _dbContext.Wallets.AsNoTracking().ToListAsync();
        }

        public async Task<Wallet?> GetWalletByIdAsync(int id)
        {
            //i used FirstOrDefaultAsync because if there is no wallet it will return null instead of throwing an exception and made the return wallet? nullable
            //also i used AsNoTracking() because those are get methods , no need for tracking for the sake of performance
            return await _dbContext.Wallets.AsNoTracking().FirstOrDefaultAsync(w => w.Id == id);
        }

        public async Task<Wallet?> GetWalletByUserIdAsync(int userId)
        {
            return await _dbContext.Wallets
                .AsNoTracking()
                .FirstOrDefaultAsync(w => w.UserId == userId);
        }

        public async Task AddWalletAsync(Wallet wallet)
        {
            await _dbContext.Wallets.AddAsync(wallet);
        }

        public Task UpdateWalletAsync(Wallet wallet)
        {
            _dbContext.Wallets.Update(wallet);
            return Task.CompletedTask;
        }

        public async Task DeleteWalletAsync(int id)
        {
            var wallet = await _dbContext.Wallets.FirstOrDefaultAsync(w=>w.UserId == id);
            if (wallet != null)
            {
                _dbContext.Wallets.Remove(wallet);
            }
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> TransferAsync(int fromUserId, int toUserId, decimal amount)
        {
            await using var transaction = await _dbContext.Database.BeginTransactionAsync();

            try
            {
                var fromWallet = await _dbContext.Wallets.FirstOrDefaultAsync(w => w.UserId == fromUserId);
                var toWallet = await _dbContext.Wallets.FirstOrDefaultAsync(w => w.UserId == toUserId);

                if (fromWallet == null || toWallet == null || fromWallet.Balance < amount)
                    return false;

                fromWallet.Balance -= amount;
                toWallet.Balance += amount;


                await _dbContext.SaveChangesAsync();
                await transaction.CommitAsync();

                var transactionLog = new Transaction
                {
                    SourceWalletId = fromWallet.Id,
                    DestinationWalletId = toWallet.Id,
                    Amount = amount,
                    CreatedAt = DateTime.UtcNow,
                    Type = TransactionType.Deposit,
                    Status = TransactionStatus.Success,
                };

                await _transactionRepo.AddAsync(transactionLog);
                await _transactionRepo.SaveChangesAsync();
                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

    }
}
