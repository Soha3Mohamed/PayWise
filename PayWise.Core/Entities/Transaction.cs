using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayWise.Core.Entities
{
    public enum TransactionType{
        Deposit = 1, Withdrawal = 2, Transfer = 3, Payment =4,
    }
    public enum TransactionStatus
    {
        Pending =1 , Success =2 , Failed=3 ,
    }

    public class Transaction
    {
        public int Id { get; set; }

        public TransactionType Type { get; set; }
        public decimal Amount { get; set; }

        public TransactionStatus Status { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Source + Destination
        public int? SourceWalletId { get; set; }
        public Wallet? SourceWallet { get; set; }

        public int? DestinationWalletId { get; set; }
        public Wallet? DestinationWallet { get; set; }
    }
}
