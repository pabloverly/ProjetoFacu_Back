using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using ApiTools.Model;
using Microsoft.IdentityModel.Tokens;

namespace ApiTools.Services
{
    public static class TokenAutenticeService
    {
        public static string GenerateToken(string input)
        {
            var rand = new Random();

            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input + rand.Next(int.MinValue, int.MaxValue));
                byte[] hashBytes = sha256.ComputeHash(inputBytes);

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    builder.Append(hashBytes[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }
    }
}