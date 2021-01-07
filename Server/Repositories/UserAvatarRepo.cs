using Learning.Server.DbContext;
using Learning.Server.Service;
using Learning.Shared;
using Learning.Shared.DbModels;
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
            if (!await UsersExits(user.Email)) {
                return sr<int>.Get("User already exists");
            }

            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();

            return sr<int>.GetSuccess(user.Id);
        }
        public async Task<sr<bool>> UsersExists(User user) {
            return sr<bool>.Get(await UsersExits(user.Email));
        }

        private async Task<bool> UsersExits(string email) {
            return await _dbContext.Users.AnyAsync(x => x.Email.ToLower() == email.ToLower());
        }
        private async Task<User> GetUser(string email) {
            return await _dbContext.Users.FirstOrDefaultAsync<User>(x => x.Email.ToLower() == email.ToLower());
        }
    }
}
