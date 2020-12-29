using Learning.Server.DbContext;
using Learning.Shared;
using Learning.Shared.DbModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learning.Server.Repositories {
    public interface IVideoRepo {
        Task<sr<IList<Media>>> GetAllVideos();
    }

    public class VideoRepo : IVideoRepo {
        private readonly AppDbContext _dbContext;
        public string ContentUri { get; set; } = @"https://land297.se/test";
        //https://land297.se/test/upload/video/aggbollarmhaavningar.mp4

        public VideoRepo(AppDbContext dbContext) {
            _dbContext = dbContext;
        }

        public async Task<sr<IList<Media>>> GetAllVideos() {
            var response = sr<IList<Media>>.Get();
            try {
                response.Data = await _dbContext.Media.Where(m => m.Type == "video/mp4").ToListAsync();
                foreach (var video in response.Data) {
                    video.FullFileName = ContentUri + video.FullFileName;
                }
                response.Success = true;
            } catch (Exception e) {
                response.Message = e.Message;
            }
            return response;
        }
    }
}
