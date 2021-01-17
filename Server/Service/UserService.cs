using Learning.Server.Repositories;
using Learning.Shared.DbModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using static Learning.Shared.Models.Enums;

namespace Learning.Server.Service {
    public interface IUserService {
        Task<User> GetUser();
        Task<User> GetUser(ClaimsPrincipal user);
        int GetUserId();
        int GetUserId(ClaimsPrincipal user);
        UserRole GetAccessLevel(User user);
    }

    public class UserService : IUserService {
        readonly IHttpContextAccessor _httpContext;
        readonly IUserRepo _userRepo;
        public UserService(IHttpContextAccessor httpContext, IUserRepo userRepo) {
            _httpContext = httpContext;
            _userRepo = userRepo;
        }
        public async Task<User> GetUser(ClaimsPrincipal user) {
            var sr = await _userRepo.Get(GetUserId(user));
            if (sr.Success) {
                return sr.Data;
            } else {
                return null;
            }
        }
        public async Task<User> GetUser() {
            var sr = await _userRepo.Get(GetUserId());
            if (sr.Success) {
                return sr.Data;
            } else {
                return null;
            }
        }
        public int GetUserId(ClaimsPrincipal user) => int.Parse(user.FindFirstValue(ClaimTypes.NameIdentifier));
        public int GetUserId() => int.Parse(_httpContext.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

        public UserRole GetAccessLevel(User user) {
            // :)
            return (int)user.UserRole switch
            {
                0 => UserRole.Default,
                1 => UserRole.Admin,
                2 => UserRole.ContentCreator,
                3 => UserRole.Basic,
                _ => UserRole.Basic
            };
        }
        
    }
}
