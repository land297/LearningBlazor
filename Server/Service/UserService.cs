using Learning.Server.Repositories;
using Learning.Shared.DbModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Learning.Server.Service {
    public class UserService {
        readonly IHttpContextAccessor _httpContext;
        readonly IUserRepo _userRepo;
        public UserService(IHttpContextAccessor httpContext, IUserRepo userRepo) {
            _httpContext = httpContext;
            _userRepo = userRepo;
        }

        public int GetUserId(ClaimsPrincipal user) => int.Parse(user.FindFirstValue(ClaimTypes.NameIdentifier));
        public async Task<User> GetUser(ClaimsPrincipal user) {
            return await _userRepo.GetUser(GetUserId(user));
        }
        public int GetUserId() => int.Parse(_httpContext.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
        public async Task<User> GetUser() {
            return await _userRepo.GetUser(GetUserId());
        }
    }
}
