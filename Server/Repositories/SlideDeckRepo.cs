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
    public interface ISlideDeckRepo {
        Task<sr<SlideDeck>> Save(SlideDeck slideDeck);
        Task<sr<List<SlideDeck>>> GetAllAsUser();
        Task<sr<List<SlideDeck>>> GetAllAsContentCreator();
        Task<sr<SlideDeck>> Get(int id);
        Task<sr<SlideDeck>> Get(string slug);
    }

    public class SlideDeckRepo : ISlideDeckRepo {
        private readonly AppDbContext _dbContext;

        public SlideDeckRepo(AppDbContext dbContext) {
            _dbContext = dbContext;
        }
        public async Task<sr<SlideDeck>> Save(SlideDeck slideDeck) {
            var response = sr<SlideDeck>.Get();
            try {
                if (slideDeck.Id != default(int)) {
                    var slideDeckInDb = await _dbContext.SlideDecks.Include(sd => sd.Slides).FirstOrDefaultAsync(sd => sd.Id == slideDeck.Id);
                    slideDeckInDb.CopyValues(slideDeck);
                    //_dbContext.Update(slideDeckInDb);
                } else {
                    await _dbContext.SlideDecks.AddAsync(slideDeck);
                }
                await _dbContext.SaveChangesAsync();
                response.SetSuccess(slideDeck);
            } catch (Exception e){
                response.Message = e.Message;
            }
            return response;
        }
        public async Task<sr<List<SlideDeck>>> GetAllAsUser() {
            return await Get(false);
        }
        public async Task<sr<List<SlideDeck>>> GetAllAsContentCreator() {
            return await Get(true);
        }
        private async Task<sr<List<SlideDeck>>> Get(bool getUnpublished) {
            var response = sr<List<SlideDeck>>.Get();
            try {
                if (getUnpublished) {
                    var list = await _dbContext.SlideDecks.Where(sd => !sd.IsDeleted).Include(sd => sd.Slides).ToListAsync();
                    response.SetSuccess(list);
                } else {
                    var list = await _dbContext.SlideDecks.Where(sd => sd.Published != DateTime.MinValue && !sd.IsDeleted).Include(sd => sd.Slides).ToListAsync();
                    response.SetSuccess(list);
                }
            } catch (Exception e) {
                response.Message = e.Message;
            }
            return response;
        }
        public async Task<sr<SlideDeck>> Get(int id) {
            //TODO: WTF does not id 3 work when debugging?!
            System.Diagnostics.Debug.WriteLine(id);
            var response = sr<SlideDeck>.Get();
            try {
                var sd = await _dbContext.SlideDecks.Include(sd => sd.Slides).FirstOrDefaultAsync(sd => sd.Id == id);
                System.Diagnostics.Debug.WriteLine(sd);
                response.SetSuccess(sd);
            } catch (Exception e) {
                System.Diagnostics.Debug.WriteLine(e.Message);
                response.Message = e.Message;
            }
            return response;
        }
        public async Task<sr<SlideDeck>> Get(string slug) {
            //TODO: WTF does not id 3 work when debugging?!
            System.Diagnostics.Debug.WriteLine(slug);
            var response = sr<SlideDeck>.Get();
            try {
                var sd = await _dbContext.SlideDecks.Include(sd => sd.Slides).FirstOrDefaultAsync(sd => sd.Slug == slug);
                System.Diagnostics.Debug.WriteLine(sd);
                response.SetSuccess(sd);
            } catch (Exception e) {
                System.Diagnostics.Debug.WriteLine(e.Message);
                response.Message = e.Message;
            }
            return response;
        }
    }
}
