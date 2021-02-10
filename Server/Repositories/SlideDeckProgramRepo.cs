using Learning.Server.DbContext;
using Learning.Server.Repositories.Base;
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
        Task<int> SaveAndGetId(SlideDeckProgram slideDeckProgram);
        Task<List<SlideDeckProgram>> GetAllAsContentCreator();
        Task<List<SlideDeckProgram>> GetAllAsUser();
        Task<SlideDeckProgram> GetFirst(int id);
        Task<SlideDeckProgram> GetFirst(string slug);
    }
    public class SlideDeckProgramRepo : RepoBase2<SlideDeckProgram>, ISlideDeckProgramRepo/*, IGetter<SlideDeckProgram>*/ {
        public SlideDeckProgramRepo(AppDbContext dbContext) : base(dbContext){
        }
        public override async Task<int> SaveAndGetId(SlideDeckProgram slideDeckProgram) {
            // TOOD : how to handle this, we cannot have existing slideDecks as EF will try to insert them again with same Id
            foreach (var entry in slideDeckProgram.Entries) {
                entry.SlideDeck = null;
            }
           
            if (slideDeckProgram.Id != default(int)) {
                var slideDeckProgramInDb = await _dbContext.SlideDeckPrograms.Include(x => x.Entries).FirstOrDefaultAsync(x => x.Id == slideDeckProgram.Id);
                slideDeckProgramInDb.CopyValues(slideDeckProgram);
            } else {
                await _dbContext.SlideDeckPrograms.AddAsync(slideDeckProgram);
            }
            await _dbContext.SaveChangesAsync();
       
            return slideDeckProgram.Id;
        }
        public override Task<int> SaveAndDtoGetId(object obj) { 
            var program = obj as SlideDeckProgram;
            if (program != null) {
               return SaveAndGetId(program);
            }
            return null;
        }

        public override Task<SlideDeckProgram> SaveDtoAndGetEntity(object obj) {
            throw new NotImplementedException();
        }
        public async Task<List<SlideDeckProgram>> GetAllAsUser() {
            return await Get(false, null);
        }
        public async Task<List<SlideDeckProgram>> GetAllAsContentCreator() {
            return await Get(true, x => !x.IsDeleted);
        }
        private async Task<List<SlideDeckProgram>> Get(bool getUnpublished, System.Linq.Expressions.Expression<Func<SlideDeckProgram, bool>> where) {
                if (getUnpublished) {
                    return await _dbContext.SlideDeckPrograms.Where(where).Include(x => x.Entries).ThenInclude(x => x.SlideDeck).ThenInclude(x => x.Slides).ToListAsync();
                } else {
                    return await _dbContext.SlideDeckPrograms.Where(x => x.Published != DateTime.MinValue && !x.IsDeleted).Include(x => x.Entries).ThenInclude(x => x.SlideDeck).ThenInclude(x => x.Slides).ToListAsync();
                }
        }
        public async Task<SlideDeckProgram> GetFirst(int id) {
            return await GetFirstWithIncludes(sd => sd.Id == id);
        }
        public async Task<SlideDeckProgram> GetFirst(string slug) {
            return await GetFirstWithIncludes(sd => sd.Slug == slug);
        }
        public async Task<SlideDeckProgram> GetFirstWithIncludes(System.Linq.Expressions.Expression<Func<SlideDeckProgram,bool>> selector) {
                return await _dbContext.SlideDeckPrograms.Include(x => x.Entries).ThenInclude(x => x.SlideDeck).ThenInclude(x => x.Slides).FirstOrDefaultAsync(selector);
        }

 
    }
}
