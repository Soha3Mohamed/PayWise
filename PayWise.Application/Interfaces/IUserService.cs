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
        Task<ServiceResult<IEnumerable<UserResponseDTO>>> GetAllUsersAsync();
        Task<ServiceResult<UserResponseDTO>> GetUserByIdAsync(int id);
        Task<ServiceResult<string>> AuthenticateAsync(string email, string password);
        Task<ServiceResult<UserResponseDTO>> RegisterUserAsync(UserRegisterationDTO registerationDTO);

        Task<ServiceResult<UserResponseDTO>> UpdateUserAsync(int userId, UserUpdateDTO updateDTO);
        Task<ServiceResult<bool>> DeleteUserAsync(int id);

        public Task<ServiceResult<string>> ChangePasswordAsync(int id, string newPassword);

    }
}
