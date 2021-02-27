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
    public interface ICompletedSlideDeckProgramRepo {
        Task<Tuple<CompletedSlideDeckProgram,string>> Save(CompletedSlideDeckProgram completedProgram);
        Task<IList<CompletedSlideDeckProgram>> GetAll();
        Task<IList<CompletedSlideDeckProgram>> GetAllForUserAvatar(int id);
        Task<CompletedSlideDeckProgram> GetShared(int id);
    }

    public class CompletedSlideDeckProgramRepo : RepoBase2<CompletedSlideDeckProgram>, 
        ICompletedSlideDeckProgramRepo
        {
        //private readonly AppDbContext _dbContext;
        readonly IUserService _userService;
        public CompletedSlideDeckProgramRepo(AppDbContext dbContext, IUserService userService) : base(dbContext){
            //_dbContext = dbContext;
            _userService = userService;
        }
        public async Task<Tuple<CompletedSlideDeckProgram, string>> Save(CompletedSlideDeckProgram completedProgram) {
            // TOOD : how to handle this, we cannot have existing slideDecks as EF will try to insert them again with same Id
            completedProgram.SlideDeckProgram = null;
            completedProgram.UserAvatar = null;

            if (completedProgram.Id != default(int)) {
                var slideDeckProgramInDb = await _dbContext.CompletedSlideDeckPrograms.FirstOrDefaultAsync(x => x.Id == completedProgram.Id);
                //TODO implement update
                //slideDeckProgramInDb.CopyValues(completedProgram);
            } else {
                await _dbContext.CompletedSlideDeckPrograms.AddAsync(completedProgram);
            }
            await _dbContext.SaveChangesAsync();
            return Tuple.Create(completedProgram,string.Empty);
            
        }
        public async Task<IList<CompletedSlideDeckProgram>> GetAllForUserAvatar(int id) {
            var userId = _userService.GetUserId();
            return await _dbContext.CompletedSlideDeckPrograms.Include(x => x.SlideDeckProgram).Where(x => x.UserAvatar.UserId == userId && x.UserAvatar.Id == id).ToListAsync();
        }
        public async Task<CompletedSlideDeckProgram> GetShared(int id) {
            var userId = _userService.GetUserId();
            
            var result = await _dbContext.CompletedSlideDeckPrograms.Where(x => x.Id == id).Include(x => x.SlideDeckProgram).Include(x => x.UserAvatar).SingleOrDefaultAsync();
            if (userId > 0) {
                result.IsPublic = true;
                await _dbContext.SaveChangesAsync();
            } else if (!result.IsPublic) {
                result = null;
            }
            return result;
        }

        //public override Task<int> SaveDtoAndGetId(object obj) {
        //    throw new NotImplementedException();
        //}

        public override Task<Tuple<CompletedSlideDeckProgram,string>> SaveDtoAndGetEntity(object obj) {
            throw new NotImplementedException();
        }

        Task<IList<CompletedSlideDeckProgram>> ICompletedSlideDeckProgramRepo.GetAll() {
            throw new NotImplementedException();
        }
    }
}
