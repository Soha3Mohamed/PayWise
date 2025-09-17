using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayWise.Application.DTOs
{
    public enum TransactionType
    {
        Deposit = 1, Withdrawal = 2, Transfer = 3, Payment = 4,
    }
    public enum TransactionStatus
    {
        Pending = 1, Success = 2, Failed = 3,
    }
    public class TransactionResponseDTO
    {
        public int Id { get; set; }
        public int FromWalletId { get; set; }
        public int ToWalletId { get; set; }
        public decimal Amount { get; set; }
        public TransactionType Type { get; set; }
        public TransactionStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
