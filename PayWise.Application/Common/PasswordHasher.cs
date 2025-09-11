using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayWise.Application.Common
{
    internal class PasswordHasher
    {
        private static PasswordHasher<string> _hasher = new();

        public static string Hash(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                return "Password cannot be null or empty";
            }
            return _hasher.HashPassword(null, password);
        }

        public static bool VerifyPassword(string hashedPassword, string inputPassword)
        {
            if (string.IsNullOrEmpty(hashedPassword) || string.IsNullOrEmpty(inputPassword))
            {
                return false;
            }
            var result = _hasher.VerifyHashedPassword(null, hashedPassword, inputPassword);
            return result == PasswordVerificationResult.Success;
        }
    }
}
