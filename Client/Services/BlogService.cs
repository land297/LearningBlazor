using Learning.Client.Services.Base;
using Learning.Shared;
using Learning.Shared.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Learning.Client.Services {
    public interface IBlogService {
        Task<sr<int>> Save(BlogPost slideDeck);
        Task<sr<List<BlogPost>>> GetAllPublished();
        Task<sr<bool>> Delete(int id);
    }

    public class BlogService : IBlogService {
        private readonly HttpClient _http;
        private readonly ServiceBase2 _base;
        public BlogService(HttpClient http) {
            _http = http;
            _base = new ServiceBase2(http);
        }
        public async Task<sr<int>> Save(BlogPost slideDeck) {
            // TODO: add user to set author
            slideDeck.AuthorId = 1;

            return await _base.Post<BlogPost, int>("api/blog", slideDeck);
        }

        public async Task<sr<bool>> Delete(int id) {
            return await _base.Delete($"api/blog/delete/{id}");
        }
        public async Task<sr<List<BlogPost>>> GetAllPublished() {
            return await _base.Get<List<BlogPost>>("api/blog");
        }
    }
}
