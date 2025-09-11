using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using PayWise.Application.Common;
using PayWise.Application.DTOs;
using PayWise.Application.Interfaces;
using PayWise.Core;
using PayWise.Core.Entities;
using PayWise.Core.Interfaces;



namespace PayWise.Application.Implementation
{
    public class UserService : IUserService
    {
        private readonly ILogger<UserService> _logger;
        private readonly IUserRepository _userRepository;
        private readonly string _jwtKey = "secretKeyforjwtauthenticationforPayWise"; // should come from config

        public UserService(ILogger<UserService> logger, IUserRepository userRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
        }

        public async Task<ServiceResult<User?>> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user == null)
            {
                _logger.LogWarning("User with id {Id} not found", id);
                return ServiceResult<User?>.Fail($"User with this id: {id} is Not Found");
            }

            _logger.LogInformation("User with id {Id} retrieved successfully", id);
            return ServiceResult<User?>.Ok(user);
        }

        public async Task<ServiceResult<string>> AuthenticateAsync(string email, string password)
        {
            var user = await _userRepository.GetByEmailAsync(email);

            if (user == null || user.PasswordHash == PasswordHasher.Hash(password))
            {
                _logger.LogWarning("Authentication failed for email {Email}", email);
                return ServiceResult<string>.Fail("Authentication Failed, Credentials don't match");
            }

            _logger.LogInformation("Authentication succeeded for email {Email}", email);

            var token = GenerateToken(user);

            return ServiceResult<string>.Ok(token);
        }

        public async Task<ServiceResult<User>> RegisterUserAsync(UserRegisterationDTO registrationDTO)
        {
            // Check if user already exists
            var existingUser = await _userRepository.GetByEmailAsync(registrationDTO.Email);
            if (existingUser != null)
            {
                _logger.LogWarning("User registration failed: email {Email} already exists", registrationDTO.Email);
                return ServiceResult<User>.Fail("Email is already registered");
            }
            var user = new User
            {

                Email = registrationDTO.Email,
                PasswordHash = PasswordHasher.Hash(registrationDTO.Password), // In real applications, hash the password
                FirstName = registrationDTO.FirstName,
                LastName = registrationDTO.LastName,
                CreatedAt = DateTime.UtcNow,
                LastUpdatedAt = DateTime.UtcNow
            };
            // Save new user
            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();

            _logger.LogInformation("User {Email} registered successfully", user.Email);

            return ServiceResult<User>.Ok(user);
        }

        private string GenerateToken(User user)
        {
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email)
        };

            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtKey));
            var signingKey = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: signingKey
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<ServiceResult<IEnumerable<User>>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();
            if (users == null)
            {
                _logger.LogWarning("No users found in the system");
                return ServiceResult<IEnumerable<User>>.Fail("No users found");
            }
            _logger.LogInformation("Retrieved {Count} users", users.Count());
            return ServiceResult<IEnumerable<User>>.Ok(users);
        }

        public async Task<ServiceResult<User>> UpdateUserAsync(UserUpdateDTO updateDTO)
        {
            var existingUser = await _userRepository.GetByEmailAsync(updateDTO.Email);
            if (existingUser != null)
            {
                _logger.LogWarning("User update failed: email {Email} already exists", updateDTO.Email);
                return ServiceResult<User>.Fail("Email is already registered");
            }
            var user = new User
            {

                Email = updateDTO.Email,
                FirstName = updateDTO.FirstName,
                LastName = updateDTO.LastName,
                
                LastUpdatedAt = DateTime.UtcNow
            };
            // Save new user
            await _userRepository.UpdateAsync(user);
            await _userRepository.SaveChangesAsync();

            _logger.LogInformation("User {Email} updated successfully", user.Email);

            return ServiceResult<User>.Ok(user);
        }

        public async Task<ServiceResult<bool>> DeleteUserAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user == null)
            {
                _logger.LogWarning("User with id {Id} not found", id);
                return ServiceResult<bool>.Fail($"User with this id: {id} is Not Found");
            }
            await _userRepository.DeleteAsync(id);
            await _userRepository.SaveChangesAsync();
            _logger.LogInformation("User with id {Id} deleted successfully", id);
            return ServiceResult<bool>.Ok(true);
        }

        public Task<ServiceResult<string>> ChangePasswordAsync(int id)
        {
            throw new NotImplementedException();
        }
    }


}

