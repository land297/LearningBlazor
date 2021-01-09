using Learning.Server.DbContext;
using Learning.Shared;
using Learning.Shared.DbModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learning.Server.Repositories {
    public interface ICompletedSlideDeckProgramRepo {
        Task<sr<CompletedSlideDeckProgram>> Save(CompletedSlideDeckProgram completedProgram);
    }

    public class CompletedSlideDeckProgramRepo : ICompletedSlideDeckProgramRepo {
        private readonly AppDbContext _dbContext;
        public CompletedSlideDeckProgramRepo(AppDbContext dbContext) {
            _dbContext = dbContext;
        }
        public async Task<sr<CompletedSlideDeckProgram>> Save(CompletedSlideDeckProgram completedProgram) {
            var response = sr<CompletedSlideDeckProgram>.Get();
            // TOOD : how to handle this, we cannot have existing slideDecks as EF will try to insert them again with same Id
            completedProgram.SlideDeckProgram = null;
            completedProgram.UserAvatar = null;

            try {
                if (completedProgram.Id != default(int)) {
                    var slideDeckProgramInDb = await _dbContext.CompletedSlideDeckPrograms.FirstOrDefaultAsync(x => x.Id == completedProgram.Id);
                    //TODO implement update
                    //slideDeckProgramInDb.CopyValues(completedProgram);
                } else {
                    await _dbContext.CompletedSlideDeckPrograms.AddAsync(completedProgram);
                }
                await _dbContext.SaveChangesAsync();
                response.SetSuccess(completedProgram);
            } catch (Exception e) {
                response.Message = e.Message;
            }
            return response;
        }
    }
}
