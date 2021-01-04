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
        Task<sr<List<SlideDeckProgram>>> GetAllAsContentCreator();
        Task<sr<List<SlideDeckProgram>>> GetAllAsUser();
        Task<sr<SlideDeckProgram>> GetFirst(int id);
        Task<sr<SlideDeckProgram>> GetFirst(string slug);
    }
    //public interface IGetter<T> {
    //    Task<sr<List<T>>> GetAllAsContentCreator();
    //    Task<sr<List<T>>> GetAllAsUser();
    //}

    //public static class IGetterExtensions {
    //    public static Task<sr<List<T>>> GetAllAsContentCreator<T>(this IGetter<T> getter) {
    //        var response = sr<List<T>>.Get();
    //        try {
    //            if (getUnpublished) {
    //                // TODO: same as below, returns wrong type..
    //                //var list = await _dbContext.SlideDeckPrograms.Where(where).Include(x => x.Entries).ToListAsync();
    //                var list = await _dbContext.SlideDeckPrograms.Where(x => !x.IsDeleted).Include(x => x.Entries).ToListAsync();
    //                response.SetSuccess(list);
    //            } else {
    //                var list = await _dbContext.SlideDeckPrograms.Where(x => x.Published != DateTime.MinValue && !x.IsDeleted).Include(x => x.Entries).ToListAsync();
    //                response.SetSuccess(list);
    //            }
    //        } catch (Exception e) {
    //            response.Message = e.Message;
    //        }
    //        return response;
    //    }
    //}
    public class SlideDeckProgramRepo : ISlideDeckProgramRepo/*, IGetter<SlideDeckProgram>*/ {
        private readonly AppDbContext _dbContext;

        public SlideDeckProgramRepo(AppDbContext dbContext) {
            _dbContext = dbContext;
        }
        public async Task<sr<SlideDeckProgram>> Save(SlideDeckProgram slideDeckProgram) {
            var response = sr<SlideDeckProgram>.Get();
            // TOOD : how to handle this, we cannot have existing slideDecks as EF will try to insert them again with same Id
            foreach (var entry in slideDeckProgram.Entries) {
                entry.SlideDeck = null;
            }
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
        public async Task<sr<List<SlideDeckProgram>>> GetAllAsUser() {
            return await Get(false, null);
        }
        public async Task<sr<List<SlideDeckProgram>>> GetAllAsContentCreator() {
            return await Get(true,null);
        }
        private async Task<sr<List<SlideDeckProgram>>> Get(bool getUnpublished, Func<SlideDeckProgram, bool> where) {
            var response = sr<List<SlideDeckProgram>>.Get();
            try {
                if (getUnpublished) {
                    // TODO: same as below, returns wrong type..
                    //var list = await _dbContext.SlideDeckPrograms.Where(where).Include(x => x.Entries).ToListAsync();
                    var list = await _dbContext.SlideDeckPrograms.Where(x => !x.IsDeleted).Include(x => x.Entries).ThenInclude(x => x.SlideDeck).ThenInclude(x => x.Slides).ToListAsync();
                    response.SetSuccess(list);
                } else {
                    var list = await _dbContext.SlideDeckPrograms.Where(x => x.Published != DateTime.MinValue && !x.IsDeleted).Include(x => x.Entries).ThenInclude(x => x.SlideDeck).ThenInclude(x => x.Slides).ToListAsync();
                    response.SetSuccess(list);
                }
            } catch (Exception e) {
                response.Message = e.Message;
            }
            return response;
        }
        public async Task<sr<SlideDeckProgram>> GetFirst(int id) {
            return await GetFirst(sd => sd.Id == id);
        }
        public async Task<sr<SlideDeckProgram>> GetFirst(string slug) {
            return await GetFirst(sd => sd.Slug == slug);
        }
        public async Task<sr<SlideDeckProgram>> GetFirst(System.Linq.Expressions.Expression<Func<SlideDeckProgram,bool>> selector) {
            var response = sr<SlideDeckProgram>.Get();
            try {
                var sd = await _dbContext.SlideDeckPrograms.Include(x => x.Entries).ThenInclude(x => x.SlideDeck).ThenInclude(x => x.Slides).FirstOrDefaultAsync(selector);
                if (sd == null) {
                    response.Message = "Not found";
                } else {
                    response.SetSuccess(sd);
                }
            } catch (Exception e) {
                response.Message = e.Message;
            }
            return response;
        }
        //private async Task<sr<List<T>>> Get<T, TE>(Func<TE, bool> selection, bool getUnpublished, IQueryable<TE> dbset) where TE : class {
        //    var response = sr<List<T>>.Get();
        //    try {
        //        if (getUnpublished) {
        //            // TODO: does not work, Where is not returning IQueryable
        //            var list = await dbset.Where(selection).Include(sd => sd.Slides).ToListAsync();
        //            response.SetSuccess(list);
        //        } else {
        //            var list = await _dbContext.SlideDecks.Where(sd => sd.Published != DateTime.MinValue && !sd.IsDeleted).Include(sd => sd.Slides).ToListAsync();
        //            response.SetSuccess(list);
        //        }
        //    } catch (Exception e) {
        //        response.Message = e.Message;
        //    }
        //    return response;
        //}
    }
}
