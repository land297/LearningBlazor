﻿using Learning.Server.DbContext;
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
    }

    public class UserAccessSlideDeckProgramRepo : RepoBase2<UserAccessSlideDeckProgram>, IUserAccessSlideDeckProgramRepo {
        IUserAvatarRepo _userAvatarRepo;
        public UserAccessSlideDeckProgramRepo(AppDbContext dbContext, IUserAvatarRepo userAvatarRepo) : base(dbContext) {
            _userAvatarRepo = userAvatarRepo;
        }

        public async Task<sr<IList<UserAccessSlideDeckProgram>>> Get(UserAvatar userAvatar) {
            var programs =  await Get(DbSet.Include(x => x.SlideDeckProgram).Include(x => x.UserAvatar).Where(x => x.UserAvatarId == userAvatar.Id).ToListAsync());
            //if (programs.Success) {
            //    foreach (var program in programs.Data) {
            //        if (program.UserAvatarId != null && program.UserAvatarId != default(int)) {
            //            int id = program.UserAvatarId ?? default(int);
            //            program.UserAvatar = await _userAvatarRepo.Get(id);
            //        }
            //    }
            //}

            return programs;
        }
        public async Task<sr<IList<UserAccessSlideDeckProgram>>> Get(User user) {
            return await Get(DbSet.Include(x => x.SlideDeckProgram).Where(x => x.UserId == user.Id).ToListAsync());
        }

    }
}
