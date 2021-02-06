using Learning.Server.DbContext;
using Learning.Server.Repositories.Base;
using Learning.Server.Service;
using Learning.Shared;
using Learning.Shared.DbModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learning.Server.Repositories {
    public interface IUserAvatarRepo {
        Task<sr<int>> SaveInContext(UserAvatar userAvatar);
        Task<sr<UserAvatar>> GetInContext(int id);
        Task<sr<UserAvatar>> Get(int id);
        Task<sr<IList<UserAvatar>>> GetAllInContext();
        Task<sr<IList<UserAvatar>>> GetAllForUser_NoBlob(User user);
        Task<sr<UserAvatar>> SetActiveInContext(int id);
        Task<sr<UserAvatar>> GetActiveInContext();
    }

    public class UserAvatarRepo : RepoBase2<UserAvatar>, IUserAvatarRepo, IRepoBase3<UserAvatar> {
        private readonly IUserService _userService;

        public UserAvatarRepo(AppDbContext dbContext, IUserService userService) : base(dbContext) {
            _userService = userService;
        }
        public override Task<sr<int>> Save(object obj) {
            throw new NotImplementedException();
        }

        public override Task<sr<UserAvatar>> SaveReturnEntity(object obj) {
            throw new NotImplementedException();
        }

        public override Task<sr<int>> Save(UserAvatar userAvatar) {
            return SaveInContext(userAvatar);
        }

        public async Task<sr<int>> SaveInContext(UserAvatar userAvatar) {
            //TODO: how could an "admin" user add userAvatars for another user...
            //      this just assigned current logged in user to the userAvatar
            userAvatar.UserId = _userService.GetUserId();
            if (userAvatar.Id != default(int)) {
                //update
                var dbUserAvatar = await _dbContext.UserAvatars.Include(x => x.Blob).FirstOrDefaultAsync(x => x.Id == userAvatar.Id && x.UserId == userAvatar.UserId);
                if (dbUserAvatar == null) {
                    //TODO: trying to access 
                } else {
                    dbUserAvatar.Name = userAvatar.Name;
                    dbUserAvatar.Description = userAvatar.Description;
                    if (dbUserAvatar.Blob == null) {
                        dbUserAvatar.Blob = new Blob();
                    }
                    dbUserAvatar.Blob.Data = userAvatar.CoverImage;
                  
                }
            } else {
                await _dbContext.UserAvatars.AddAsync(userAvatar);
            }

           
            await _dbContext.SaveChangesAsync();

            return sr<int>.GetSuccess(userAvatar.Id);
        }
        public async Task<sr<UserAvatar>> SetActiveInContext(int id) {
            var userId = _userService.GetUserId();
            var data = await _dbContext.UserAvatars.Where(x => x.UserId == userId).ToListAsync();
            UserAvatar active = null;
            foreach (var avatar in data) {
                if (avatar.Id == id) {
                    avatar.IsActive = true;
                    active = avatar;
                } else {
                    avatar.IsActive = false;
                }
            }
            await _dbContext.SaveChangesAsync();
            return sr<UserAvatar>.GetSuccess(active);
        }
        public async Task<sr<UserAvatar>> GetActiveInContext() {
            var userId = _userService.GetUserId();
            var data = await _dbContext.UserAvatars.Include(x => x.Blob).SingleOrDefaultAsync(x => x.UserId == userId && x.IsActive);
            return sr<UserAvatar>.GetSuccess(data);
        }
        public async Task<sr<UserAvatar>> GetInContext(int id) {
            var userId = _userService.GetUserId();
            var data = await _dbContext.UserAvatars.Include(x => x.Blob).SingleOrDefaultAsync(x => x.UserId == userId && x.Id == id);

            return sr<UserAvatar>.GetSuccess(data);
        }
        public async Task<sr<IList<UserAvatar>>> GetAllInContext() {
            var userId = _userService.GetUserId();
            var data = await _dbContext.UserAvatars.Include(x => x.Blob).Where(x => x.UserId == userId).ToListAsync();
            return sr<IList<UserAvatar>>.GetSuccess(data);
        }
        public async Task<sr<IList<UserAvatar>>> GetAllForUser_NoBlob(User user) {
            var data = await _dbContext.UserAvatars.Where(x => x.UserId == user.Id).ToListAsync();
            return sr<IList<UserAvatar>>.GetSuccess(data);
        }
        new public async Task<sr<UserAvatar>> Get(int id) {
            var data = await  Get(DbSet.Include(x => x.Blob).SingleOrDefaultAsync(x => x.Id == id));

            return sr<UserAvatar>.GetSuccess(data.Data);
        }


    }
}
