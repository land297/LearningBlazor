using Learning.Server.DbContext;
using Learning.Shared.DbModels;
using Learning.Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learning.Server.Repositories {
    public interface ISlideDeckRepo {
        Task<int> Add(SlideDeck slideDeck);
    }

    public class SlideDeckRepo : ISlideDeckRepo {
        private readonly AppDbContext _dbContext;

        public SlideDeckRepo(AppDbContext dbContext) {
            _dbContext = dbContext;
        }
        public async Task<int> Add(SlideDeck slideDeck) {
            try {
                await _dbContext.SlideDecks.AddAsync(slideDeck);
                await _dbContext.SaveChangesAsync();

                return slideDeck.Id;
            } catch {
                return 0;
            }
        }
    }
}
