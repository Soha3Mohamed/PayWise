using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayWise.Core.Entities
{
    public class Wallet
    {
        public int Id { get; set; }
        public decimal Balance { get; set; }
        public string Currency { get; set; } = "EGP"; 

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set;} = DateTime.UtcNow;

        public int UserId { get; set; }
        public User User { get; set; }

        public ICollection<Transaction> SourceTransactions { get; set; } = new HashSet<Transaction>();

        public ICollection<Transaction> DestinationTransactions { get; set; } = new HashSet<Transaction>();
    }
}
