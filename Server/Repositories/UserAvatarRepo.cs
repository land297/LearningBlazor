using Learning.Server.DbContext;
using Learning.Server.Service;
using Learning.Shared;
using Learning.Shared.DbModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learning.Server.Repositories {
    public class UserAvatarRepo : RepoBase {
        private readonly IUserService _userService;

        public UserAvatarRepo(AppDbContext dbContext, IUserService userService) : base(dbContext) {
            _userService = userService;
        }
        public async Task<sr<int>> AddUserAvatar(UserAvatar userAvatar) {
            //TODO: how could an "admin" user add userAvatars for another user...
            //      this just assigned current logged in user to the userAvatar
            userAvatar.UserId = _userService.GetUserId();
           

            await _dbContext.UserAvatars.AddAsync(userAvatar);
            await _dbContext.SaveChangesAsync();

            return sr<int>.GetSuccess(userAvatar.Id);
        }
        private async Task<List<UserAvatar>> GetUserAvatarsInContext() {
            var id = _userService.GetUserId();
            return await _dbContext.UserAvatars.Where(x => x.UserId == id).ToListAsync();
        }
    }
}
