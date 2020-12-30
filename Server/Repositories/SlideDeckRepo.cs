using Learning.Server.DbContext;
using Learning.Shared;
using Learning.Shared.DbModels;
using Learning.Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learning.Server.Repositories {
    public interface ISlideDeckRepo {
        Task<sr<SlideDeck>> Add(SlideDeck slideDeck);
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
    }
}
