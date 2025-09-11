using PayWise.Application.DTOs;
using PayWise.Core.Entities;
using PayWise.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayWise.Application.Interfaces
{
    public interface IUserService
    {
        Task<ServiceResult<IEnumerable<User>>> GetAllUsersAsync();
        Task<ServiceResult<User?>> GetUserByIdAsync(int id);
        Task<ServiceResult<string>> AuthenticateAsync(string email, string password);
        Task<ServiceResult<User>> RegisterUserAsync(UserRegisterationDTO registerationDTO);

        Task<ServiceResult<User>> UpdateUserAsync(UserUpdateDTO updateDTO);
        Task<ServiceResult<bool>> DeleteUserAsync(int id);

    }
}
