using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayWise.Application.DTOs
{
    public  class AddWalletDTO
    {
        public decimal Balance { get; set; }
        public string Currency { get; set; } = "EGP";
    }
}
