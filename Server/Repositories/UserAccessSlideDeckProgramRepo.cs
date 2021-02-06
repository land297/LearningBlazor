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
        Task<sr<IList<UserAccessSlideDeckProgram>>> Get(UserAvatar userAvatar);
        Task<sr<IList<UserAccessSlideDeckProgram>>> Get(User user);
        Task<sr<UserAccessSlideDeckProgram>> SaveReturnEntity(UserAccessSlideDeckProgram entity);
        Task<sr<UserAccessSlideDeckProgram>> RemoveWithId(int id);
        Task<sr<UserAccessSlideDeckProgram>> Get(int id);
    }

    public class UserAccessSlideDeckProgramRepo : RepoBase2<UserAccessSlideDeckProgram>, IUserAccessSlideDeckProgramRepo {
        IUserAvatarRepo _userAvatarRepo;
        ISlideDeckProgramRepo _slideDeckProgramRepo;

        public UserAccessSlideDeckProgramRepo(AppDbContext dbContext, IUserAvatarRepo userAvatarRepo, ISlideDeckProgramRepo slideDeckRepo) : base(dbContext) {
            _userAvatarRepo = userAvatarRepo;
            _slideDeckProgramRepo = slideDeckRepo;
        }

        public async Task<sr<IList<UserAccessSlideDeckProgram>>> Get(UserAvatar userAvatar) {
            //TODO: not include the image-attribute
            var programs = await Get(DbSet.Include(x => x.SlideDeckProgram).Include(x => x.UserAvatar).Where(x => x.UserAvatarId == userAvatar.Id).ToListAsync());


            return programs;
        }
        public async Task<sr<IList<UserAccessSlideDeckProgram>>> Get(User user) {
            return await Get(DbSet.Include(x => x.SlideDeckProgram).Where(x => x.UserId == user.Id).ToListAsync());
        }
        new public async Task<sr<UserAccessSlideDeckProgram>> Get(int id) {
            return await Get(DbSet.Include(x => x.SlideDeckProgram).FirstOrDefaultAsync(x => x.Id == id));
        }
        public async Task<sr<UserAccessSlideDeckProgram>> RemoveWithId(int id) {
            return await Remove(DbSet.FirstOrDefaultAsync(x => x.Id == id));
        }

        public override Task<sr<int>> Save(object obj) {
            throw new NotImplementedException();
        }

        public override async Task<sr<UserAccessSlideDeckProgram>> SaveReturnEntity(UserAccessSlideDeckProgram ua) {
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
            var sr = await base.SaveReturnEntity(ua);
            if (sr.Success) {
                var sr2 = await _slideDeckProgramRepo.GetFirst(ua.SlideDeckProgramId);
                if (sr2.Success) {
                    sr.Data.SlideDeckProgram = sr2.Data;
                    
                } else {
                    await base.Remove(sr.Data);
                    sr.Success = false;
                    sr.Message = $"SlideDeckProgram with Id {ua.SlideDeckProgramId} doesn't exist";
                }
            }
            return sr;
        }

        public override Task<sr<UserAccessSlideDeckProgram>> SaveReturnEntity(object obj) {
            throw new NotImplementedException();
        }
    }
}
