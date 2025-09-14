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
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserResponseDTO>(); //user to UserRegisterationDTO for get 
            CreateMap<UserUpdateDTO, User>();  //user to UserUpdateDTO for update
            CreateMap<UserRegisterationDTO, User>(); //UserRegisterationDTO to user for post
        }
    }
}
