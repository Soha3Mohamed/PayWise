using PayWise.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PayWise.Core.Interfaces
{
    public interface IWalletRepository
    {
        Task<Wallet?> GetWalletByIdAsync(int id);
        Task<Wallet?> GetWalletByUserIdAsync(int userId);
        Task<IEnumerable<Wallet>> GetAllWalletsAsync();
        Task AddWalletAsync(Wallet wallet);
        Task UpdateWalletAsync(Wallet wallet);
        Task DeleteWalletAsync(int id);
        Task SaveChangesAsync();
        //Task<IDbContextTransaction> BeginTransactionAsync(); //will require installing ef core here, not supported in core
        public Task<bool> TransferAsync(int fromUserId, int toUserId, decimal amount);
    }


}

