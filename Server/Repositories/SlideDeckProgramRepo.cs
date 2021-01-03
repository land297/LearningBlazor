using Learning.Server.DbContext;
using Learning.Shared;
using Learning.Shared.DbModels;
using Learning.Shared.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learning.Server.Repositories {
    public interface ISlideDeckProgramRepo {
        Task<sr<SlideDeckProgram>> Save(SlideDeckProgram slideDeckProgram);
    }

    public class SlideDeckProgramRepo : ISlideDeckProgramRepo {
        private readonly AppDbContext _dbContext;

        public SlideDeckProgramRepo(AppDbContext dbContext) {
            _dbContext = dbContext;
        }
        public async Task<sr<SlideDeckProgram>> Save(SlideDeckProgram slideDeckProgram) {
            var response = sr<SlideDeckProgram>.Get();
            try {
                if (slideDeckProgram.Id != default(int)) {
                    var slideDeckProgramInDb = await _dbContext.SlideDeckPrograms.Include(x => x.Entries).FirstOrDefaultAsync(x => x.Id == slideDeckProgram.Id);
                    // TODO: copy copy not done
                    slideDeckProgramInDb.CopyValues(slideDeckProgram);
                } else {
                    await _dbContext.SlideDeckPrograms.AddAsync(slideDeckProgram);
                }
                await _dbContext.SaveChangesAsync();
                response.SetSuccess(slideDeckProgram);
            } catch (Exception e) {
                response.Message = e.Message;
            }
            return response;
        }
    }
}
