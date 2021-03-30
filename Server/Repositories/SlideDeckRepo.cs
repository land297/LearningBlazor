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
        Task<Tuple<SlideDeck, string>> SaveAndGetEntity(SlideDeck slideDeck);
        Task<Tuple<int, string>> SaveAndGetId(SlideDeck slideDeck);
        Task<List<SlideDeck>> GetAllAsUser();
        Task<List<SlideDeck>> GetAllAsContentCreator();
        Task<SlideDeck> Get(int id);
        Task<SlideDeck> Get(string slug);
    }

    public class SlideDeckRepo : RepoBase2<SlideDeck>, ISlideDeckRepo {
        IVideoRepo _videoRepo;
        public SlideDeckRepo(AppDbContext dbContext, IVideoRepo v) : base (dbContext){
            _videoRepo = v;
        }
        public override Task<Tuple<int, string>> SaveDtoAndGetId(object obj) {
            throw new NotImplementedException();
        }

        public override Task<Tuple<SlideDeck, string>> SaveDtoAndGetEntity(object obj) {
            var s = obj as SlideDeck;
            if (s != null) {
                return SaveAndGetEntity(s);
            } else {
                return null;
            }
        }
        public override async Task<Tuple<int, string>> SaveAndGetId(SlideDeck slideDeck) {
            if (slideDeck.Id != default(int)) {
                var slideDeckInDb = await _dbContext.SlideDecks.Include(sd => sd.Slides).FirstOrDefaultAsync(sd => sd.Id == slideDeck.Id);
                slideDeckInDb.CopyValues(slideDeck);
                //_dbContext.Update(slideDeckInDb);
            } else {
                await _dbContext.SlideDecks.AddAsync(slideDeck);
            }
            await _dbContext.SaveChangesAsync();

            return Tuple.Create(slideDeck.Id, string.Empty);
        }
        public override async Task<Tuple<SlideDeck, string>> SaveAndGetEntity(SlideDeck slideDeck) {
                if (slideDeck.Id != default(int)) {
                    var slideDeckInDb = await _dbContext.SlideDecks.Include(sd => sd.Slides).FirstOrDefaultAsync(sd => sd.Id == slideDeck.Id);
                    slideDeckInDb.CopyValues(slideDeck);
                    //_dbContext.Update(slideDeckInDb);
                } else {
                    await _dbContext.SlideDecks.AddAsync(slideDeck);
                }
                await _dbContext.SaveChangesAsync();

            return Tuple.Create(slideDeck, string.Empty);
        }
        public async Task<List<SlideDeck>> GetAllAsUser() {
            return await Get(false);
        }
        public async Task<List<SlideDeck>> GetAllAsContentCreator() {
            return await Get(true);
        }
        private async Task<List<SlideDeck>> Get(bool getUnpublished) {
            if (getUnpublished) {
                var d = await _dbContext.SlideDecks.Where(sd => !sd.IsDeleted).Include(sd => sd.Slides).ToListAsync();
                foreach (var item in d) {
                    foreach (var slide in item.Slides) {
                        slide.VideoUrl = await _videoRepo.GetUrl(slide.VideoUrl);
                    }
                }
                return d;
            } else {
                var d =  await _dbContext.SlideDecks.Where(sd => sd.Published != DateTime.MinValue && !sd.IsDeleted).Include(sd => sd.Slides).ToListAsync();
                foreach (var item in d) {
                    foreach (var slide in item.Slides) {
                        slide.VideoUrl = await _videoRepo.GetUrl(slide.VideoUrl);
                    }
                }
                return d;
            }
        }
        public override async Task<SlideDeck> Get(int id) {
            //TODO: WTF does not id 3 work when debugging?!
            System.Diagnostics.Debug.WriteLine(id);
            var sd = await _dbContext.SlideDecks.Include(sd => sd.Slides).FirstOrDefaultAsync(sd => sd.Id == id);
                foreach (var slide in sd.Slides) {
                    slide.VideoUrl = await _videoRepo.GetUrl(slide.VideoUrl);
                }
            System.Diagnostics.Debug.WriteLine(sd);
            return sd;
        }
        public async Task<SlideDeck> Get(string slug) {
            //TODO: WTF does not id 3 work when debugging?!
            System.Diagnostics.Debug.WriteLine(slug);
            var sd = await _dbContext.SlideDecks.Include(sd => sd.Slides).FirstOrDefaultAsync(sd => sd.Slug == slug);
            foreach (var slide in sd.Slides) {
                slide.VideoUrl = await _videoRepo.GetUrl(slide.VideoUrl);
            }
            System.Diagnostics.Debug.WriteLine(sd);
            return sd;
        }

 
    }
}
