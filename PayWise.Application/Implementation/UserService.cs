using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using PayWise.Core.Entities;
using PayWise.Core.Interfaces;


namespace PayWise.Application.Implementation
{
    internal class UserService : IUserService
    {
        private readonly ILogger<UserService> _logger;
        private readonly IUserRepository _userRepository;

        public UserService(ILogger<UserService> logger, IUserRepository userRepository)
        {
            this._logger = logger;
            this._userRepository = userRepository;
        }
        public async Task RegisterUserAsync(User user)
        {
            var existing = await _userRepository.GetByEmailAsync(user.Email);
            if (existing != null)
                _logger.LogWarning("User with email {Email} already exists.", user.Email);  

            await _userRepository.AddAsync(user);
        }
    }
}
