using Learning.Server.DbContext;
using Learning.Server.Repositories.Base;
using Learning.Shared;
using Learning.Shared.DbModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learning.Server.Repositories {
    public interface IUserAccessSlideDeckProgramRepo {
        Task<List<UserAccessSlideDeckProgram>> Get(UserAvatar userAvatar);
        Task<List<UserAccessSlideDeckProgram>> Get(User user);
        Task<UserAccessSlideDeckProgram> SaveAndGetEntity(UserAccessSlideDeckProgram entity);
        Task<UserAccessSlideDeckProgram> RemoveWithId(int id);
        Task<UserAccessSlideDeckProgram> GetIncluded(int id);
    }

    public class UserAccessSlideDeckProgramRepo : RepoBase2<UserAccessSlideDeckProgram>, IUserAccessSlideDeckProgramRepo {
        IUserAvatarRepo _userAvatarRepo;
        ISlideDeckProgramRepo _slideDeckProgramRepo;

        public UserAccessSlideDeckProgramRepo(AppDbContext dbContext, IUserAvatarRepo userAvatarRepo, ISlideDeckProgramRepo slideDeckRepo) : base(dbContext) {
            _userAvatarRepo = userAvatarRepo;
            _slideDeckProgramRepo = slideDeckRepo;
        }

        public Task<List<UserAccessSlideDeckProgram>> Get(UserAvatar userAvatar) {
            //TODO: not include the image-attribute
           return DbSet.Include(x => x.SlideDeckProgram).Include(x => x.UserAvatar).Where(x => x.UserAvatarId == userAvatar.Id).ToListAsync();
        }
        public Task<List<UserAccessSlideDeckProgram>> Get(User user) {
            return DbSet.Include(x => x.SlideDeckProgram).Where(x => x.UserId == user.Id).ToListAsync();
        }
        public Task<UserAccessSlideDeckProgram> GetIncluded(int id) {
            return DbSet.Include(x => x.SlideDeckProgram).FirstOrDefaultAsync(x => x.Id == id);
        }
        public Task<UserAccessSlideDeckProgram> RemoveWithId(int id) {
            return Remove(DbSet.FirstOrDefaultAsync(x => x.Id == id));
        }

        public override Task<int> Save(object obj) {
            throw new NotImplementedException();
        }

        public override async Task<UserAccessSlideDeckProgram> SaveAndGetEntity(UserAccessSlideDeckProgram ua) {
            if (ua.UserAvatar != null) {
                ua.UserAvatarId = ua.UserAvatar.Id;
                ua.UserAvatar = null;
            }
            if (ua.SlideDeckProgram != null) {
                ua.SlideDeckProgramId = ua.SlideDeckProgram.Id;
                ua.SlideDeckProgram = null;
            }
            if (ua.User != null) {
                ua.UserId = ua.User.Id;
                ua.User = null;
            }
            var sr = await base.SaveAndGetEntity(ua);
            if (sr != null) {
                var sr2 = await _slideDeckProgramRepo.GetFirst(ua.SlideDeckProgramId);
                if (sr2.Success) {
                    sr.SlideDeckProgram = sr2.Data;
                    
                } else {
                    await base.Remove(sr);
                    throw new Exception($"SlideDeckProgram with Id {ua.SlideDeckProgramId} doesn't exist");
                }
            }
            return sr;
        }

        public override Task<UserAccessSlideDeckProgram> SaveAndGetEntity(object obj) {
            throw new NotImplementedException();
        }
    }
}
