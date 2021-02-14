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
        Task<Tuple<int, string>> SaveInContext(UserAvatar userAvatar);
        Task<UserAvatar> GetInContext(int id);
        Task<UserAvatar> Get(int id);
        Task<List<UserAvatar>> GetAllInContext();
        Task<List<UserAvatar>> GetAllForUser_NoBlob(User user);
        Task<UserAvatar> SetActiveInContext(int id);
        Task<UserAvatar> Delete(int id);
        Task<UserAvatar> GetActiveInContext();
    }

    public class UserAvatarRepo : RepoBase2<UserAvatar>, IUserAvatarRepo {
        private readonly IUserService _userService;

        public UserAvatarRepo(AppDbContext dbContext, IUserService userService) : base(dbContext) {
            _userService = userService;
        }
        public override Task<Tuple<int, string>> SaveDtoAndGetId(object obj) {
            throw new NotImplementedException();
        }

        public override Task<Tuple<UserAvatar, string>> SaveDtoAndGetEntity(object obj) {
            throw new NotImplementedException();
        }

        public override Task<Tuple<int, string>> SaveAndGetId(UserAvatar userAvatar) {
            return SaveInContext(userAvatar);
        }

        public async Task<Tuple<int, string>> SaveInContext(UserAvatar userAvatar) {
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

            return Tuple.Create(userAvatar.Id,string.Empty);
        }
        public async Task<UserAvatar> Delete(int id) {
            //TODO: how could an "admin" user add userAvatars for another user...
            //      this just assigned current logged in user to the userAvatar
                   //update
            var dbUserAvatar = await _dbContext.UserAvatars.Include(x => x.Blob).Include(x => x.User).FirstOrDefaultAsync(x => x.Id == id && x.UserId == _userService.GetUserId());
            var completedProgarms = await _dbContext.CompletedSlideDeckPrograms.Where(x => x.UserAvatarId == dbUserAvatar.Id).ToListAsync();
            var userAccessProgarms = await _dbContext.UserAccessSlideDeckPrograms.Where(x => x.UserAvatarId == dbUserAvatar.Id).ToListAsync();
            if (dbUserAvatar == null) {
                //TODO: trying to access 
            } else {
                foreach (var item in userAccessProgarms) {
                    _dbContext.UserAccessSlideDeckPrograms.Remove(item);
                }
                foreach (var item in completedProgarms) {
                    _dbContext.CompletedSlideDeckPrograms.Remove(item);
                }
                await _dbContext.SaveChangesAsync();

                _dbContext.UserAvatars.Remove(dbUserAvatar);
                await _dbContext.SaveChangesAsync();

               
            }
            return dbUserAvatar;

        }

        public async Task<UserAvatar> SetActiveInContext(int id) {
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
            return active;
        }
        public Task<UserAvatar> GetActiveInContext() {
            var userId = _userService.GetUserId();
            return _dbContext.UserAvatars.Include(x => x.Blob).SingleOrDefaultAsync(x => x.UserId == userId && x.IsActive);
        }
        public Task<UserAvatar> GetInContext(int id) {
            var userId = _userService.GetUserId();
            return _dbContext.UserAvatars.Include(x => x.Blob).SingleOrDefaultAsync(x => x.UserId == userId && x.Id == id);
        }
        public Task<List<UserAvatar>> GetAllInContext() {
            var userId = _userService.GetUserId();
           return _dbContext.UserAvatars.Include(x => x.Blob).Where(x => x.UserId == userId).ToListAsync();
        }
        public Task<List<UserAvatar>> GetAllForUser_NoBlob(User user) {
            return _dbContext.UserAvatars.Where(x => x.UserId == user.Id).ToListAsync();
        }
        new public Task<UserAvatar> Get(int id) {
            return DbSet.Include(x => x.Blob).SingleOrDefaultAsync(x => x.Id == id);
        }


    }
}
