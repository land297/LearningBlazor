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
        Task<sr<UserAccessSlideDeckProgram>> Save(UserAccessSlideDeckProgram entity);
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
            //if (programs.Success) {
            //    foreach (var program in programs.Data) {
            //        if (program.UserAvatarId != null && program.UserAvatarId != default(int)) {
            //            int id = program.UserAvatarId ?? default(int);
            //            program.UserAvatar = await _userAvatarRepo.Get(id);
            //        }
            //    }
            //}
            //var p = await DbSet.Select(x => new UserAccessSlideDeckProgram { Id = x.Id, UserAvatar = x.UserAvatar }).ToListAsync();

            //    return _context.Tenders.Select(t => new TenderViewModel
            //    {
            //        Id = t.Id,
            //        Creator = t.Creator,
            //        TenderCircles = t.TenderCircles.Select(tc => new TenderCircle { CirlceId = tc.CircleId, TenderId = tc.TenderId }).ToList();
            //}).ToList();

            return programs;
        }
        public async Task<sr<IList<UserAccessSlideDeckProgram>>> Get(User user) {
            return await Get(DbSet.Include(x => x.SlideDeckProgram).Where(x => x.UserId == user.Id).ToListAsync());
        }
        public async Task<sr<UserAccessSlideDeckProgram>> Get(int id) {
            return await Get(DbSet.Include(x => x.SlideDeckProgram).FirstOrDefaultAsync(x => x.Id == id));
        }
        public async Task<sr<UserAccessSlideDeckProgram>> RemoveWithId(int id) {
            return await Remove(DbSet.FirstOrDefaultAsync(x => x.Id == id));
        }

        public override async Task<sr<UserAccessSlideDeckProgram>> Save(UserAccessSlideDeckProgram ua) {
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
            var sr = await base.Save(ua);
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
    }
}
