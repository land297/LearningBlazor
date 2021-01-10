using Learning.Server.DbContext;
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
        Task<sr<UserAccessSlideDeckProgram>> Save(UserAccessSlideDeckProgram entity);
    }

    public class UserAccessSlideDeckProgramRepo : RepoBase2<UserAccessSlideDeckProgram>, IUserAccessSlideDeckProgramRepo {
        public UserAccessSlideDeckProgramRepo(AppDbContext dbContext) : base(dbContext) {
        }

        public async Task<sr<IList<UserAccessSlideDeckProgram>>> Get(UserAvatar userAvatar) {
            return await Get(DbSet.Include(x => x.SlideDeckProgram).Where(x => x.UserAvatarId == userAvatar.Id).ToListAsync());
        }
     
    }
}
