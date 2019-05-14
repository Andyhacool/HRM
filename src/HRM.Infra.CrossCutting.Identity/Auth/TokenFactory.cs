using HRM.Infra.CrossCutting.Identity.Interfaces.Services;
using System;
using System.Security.Cryptography;

namespace HRM.Infra.CrossCutting.Identity.Auth
{
    public class TokenFactory : ITokenFactory
    {
        public string GenerateToken(int size=32)
        {
            var randomNumber = new byte[size];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
    }
}
