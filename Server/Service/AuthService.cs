using Learning.Server.Repositories;
using Learning.Shared;
using Learning.Shared.DbModels;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Server.Service {
    public interface IAuthService {
        Task<sr<string>> Login(string email, string password);
    }

    public static class CreatePassword {
        public static void CreatePasswordHash(string password, out byte[] hash, out byte[] salt) {
            using (var hmac = new System.Security.Cryptography.HMACSHA512()) {
                salt = hmac.Key;
                hash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
    public class AuthService : IAuthService {
        readonly Microsoft.Extensions.Configuration.IConfiguration _configuration;
        readonly IUserRepo _userRepo;
        public AuthService(IUserRepo userRepo, Microsoft.Extensions.Configuration.IConfiguration configuration) {
            _userRepo = userRepo;
            _configuration = configuration;
        }

        public async Task<sr<string>> Login(string email, string password) {
            var response = sr<string>.Get();
            var user = await _userRepo.GetUser(email);
            if (user != null && VeriFyPasswordHash(password, user.PasswordHash, user.PasswordSalt)) {
                response.Success = true;
                response.Data = CreateToken(user);
            } else {
                response.Message = "Wrong password or wrong email";
            }
            return response;
        }



        private bool VeriFyPasswordHash(string password, byte[] hash, byte[] salt) {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(salt)) {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return ByteArrayCompare(hash, computedHash);
            }
        }

        private string CreateToken(User user) {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username ?? "null"),
                new Claim(ClaimTypes.Role,"role1"),
                new Claim(ClaimTypes.Role,"role2")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        static bool ByteArrayCompare(ReadOnlySpan<byte> a1, ReadOnlySpan<byte> a2) {
            return a1.SequenceEqual(a2);
        }
    }
}
