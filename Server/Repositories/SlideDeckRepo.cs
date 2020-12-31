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
        Task<sr<SlideDeck>> Add(SlideDeck slideDeck);
        Task<sr<List<SlideDeck>>> Get();
        Task<sr<List<SlideDeck>>> GetAsContentCreator();
    }

    public class SlideDeckRepo : ISlideDeckRepo {
        private readonly AppDbContext _dbContext;

        public SlideDeckRepo(AppDbContext dbContext) {
            _dbContext = dbContext;
        }
        public async Task<sr<SlideDeck>> Add(SlideDeck slideDeck) {
            var response = sr<SlideDeck>.Get();
            try {
                await _dbContext.SlideDecks.AddAsync(slideDeck);
                await _dbContext.SaveChangesAsync();
                response.SetSuccess(slideDeck);
            } catch (Exception e){
                response.Message = e.Message;
            }
            return response;
        }
        public async Task<sr<List<SlideDeck>>> Get() {
            return await Get(false);
        }
        public async Task<sr<List<SlideDeck>>> GetAsContentCreator() {
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
    }
}
