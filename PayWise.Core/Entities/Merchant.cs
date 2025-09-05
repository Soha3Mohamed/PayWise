using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayWise.Core.Entities
{   public class Merchant
    {
        public int Id { get; set; }
        public string BusinessName { get; set; }
        public string ApiKey { get; set; } = Guid.NewGuid().ToString(); // i don't know if that should be a string and not sure even of its benefit but we will see

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public int UserId { get; set; }
        public User User { get; set; } //as FK
    }
}
