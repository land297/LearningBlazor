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
    public interface ISlideDeckRepo {
        Task<SlideDeck> SaveAndGetEntity(SlideDeck slideDeck);
        Task<int> SaveAndGetId(SlideDeck slideDeck);
        Task<List<SlideDeck>> GetAllAsUser();
        Task<List<SlideDeck>> GetAllAsContentCreator();
        Task<SlideDeck> Get(int id);
        Task<SlideDeck> Get(string slug);
    }

    public class SlideDeckRepo : RepoBase2<SlideDeck>, ISlideDeckRepo {

        public SlideDeckRepo(AppDbContext dbContext) : base (dbContext){
        }
        public override Task<int> SaveDtoAndGetId(object obj) {
            throw new NotImplementedException();
        }

        public override Task<SlideDeck> SaveDtoAndGetEntity(object obj) {
            var s = obj as SlideDeck;
            if (s != null) {
                return SaveAndGetEntity(s);
            } else {
                return null;
            }
        }
        public override async Task<int> SaveAndGetId(SlideDeck slideDeck) {
            if (slideDeck.Id != default(int)) {
                var slideDeckInDb = await _dbContext.SlideDecks.Include(sd => sd.Slides).FirstOrDefaultAsync(sd => sd.Id == slideDeck.Id);
                slideDeckInDb.CopyValues(slideDeck);
                //_dbContext.Update(slideDeckInDb);
            } else {
                await _dbContext.SlideDecks.AddAsync(slideDeck);
            }
            await _dbContext.SaveChangesAsync();

            return slideDeck.Id;
        }
        public override async Task<SlideDeck> SaveAndGetEntity(SlideDeck slideDeck) {
                if (slideDeck.Id != default(int)) {
                    var slideDeckInDb = await _dbContext.SlideDecks.Include(sd => sd.Slides).FirstOrDefaultAsync(sd => sd.Id == slideDeck.Id);
                    slideDeckInDb.CopyValues(slideDeck);
                    //_dbContext.Update(slideDeckInDb);
                } else {
                    await _dbContext.SlideDecks.AddAsync(slideDeck);
                }
                await _dbContext.SaveChangesAsync();
   
            return slideDeck;
        }
        public async Task<List<SlideDeck>> GetAllAsUser() {
            return await Get(false);
        }
        public async Task<List<SlideDeck>> GetAllAsContentCreator() {
            return await Get(true);
        }
        private async Task<List<SlideDeck>> Get(bool getUnpublished) {
            if (getUnpublished) {
                return await _dbContext.SlideDecks.Where(sd => !sd.IsDeleted).Include(sd => sd.Slides).ToListAsync();
            } else {
                return await _dbContext.SlideDecks.Where(sd => sd.Published != DateTime.MinValue && !sd.IsDeleted).Include(sd => sd.Slides).ToListAsync();
            }
        }
        public override async Task<SlideDeck> Get(int id) {
            //TODO: WTF does not id 3 work when debugging?!
            System.Diagnostics.Debug.WriteLine(id);
            var sd = await _dbContext.SlideDecks.Include(sd => sd.Slides).FirstOrDefaultAsync(sd => sd.Id == id);
            System.Diagnostics.Debug.WriteLine(sd);
            return sd;
        }
        public async Task<SlideDeck> Get(string slug) {
            //TODO: WTF does not id 3 work when debugging?!
            System.Diagnostics.Debug.WriteLine(slug);
            var sd = await _dbContext.SlideDecks.Include(sd => sd.Slides).FirstOrDefaultAsync(sd => sd.Slug == slug);
            System.Diagnostics.Debug.WriteLine(sd);
            return sd;
        }

 
    }
}
