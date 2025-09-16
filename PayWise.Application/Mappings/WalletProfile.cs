using AutoMapper;
using PayWise.Application.DTOs;
using PayWise.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayWise.Application.Mappings
{
    public class WalletProfile : Profile
    {
        public WalletProfile()
        {
            CreateMap<Wallet, WalletResponseDTO>(); //user to UserRegisterationDTO for get 
            CreateMap<UpdateWalletDTO, Wallet>();  //user to UserUpdateDTO for update
            CreateMap<AddWalletDTO, Wallet>(); //UserRegisterationDTO to user for post
        }
    }
}
