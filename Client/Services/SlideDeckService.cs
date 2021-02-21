using Learning.Client.Services.Base;
using Learning.Client.Shared;
using Learning.Shared;
using Learning.Shared.DataTransferModel;
using Learning.Shared.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Learning.Client.Services {
    public interface ISlideDeckService {
        Task<sr<int>> Save(SlideDeck slideDeck);
        Task<sr<List<SlideDeck>>> GetPublished();
        Task<sr<List<SlideDeck>>> GetAsContentCreator();
        Task<sr<SlideDeck>> Get(int id);
        Task<sr<SlideDeck>> Get(string slug);
        Task<sr<Media>> UploadImage(FileUpload file);
    }

    public class SlideDeckService : ISlideDeckService {
        private readonly HttpClient _http;
        private readonly ServiceBase2 _base;
        public SlideDeckService(HttpClient http) {
            _http = http;
            _base = new ServiceBase2(http);
        }
        public async Task<sr<int>> Save(SlideDeck slideDeck) {
            // TODO: add user to set author
            slideDeck.AuthorId = 1;

            return await _base.Post<SlideDeck,int>("api/slideDeck", slideDeck);
        }
        public async Task<sr<SlideDeck>> Get(int id) {
            return await GetAny(id);
        }
        public async Task<sr<SlideDeck>> Get(string slug) {
            return await GetAny(slug);
        }
        private async Task<sr<SlideDeck>> GetAny(object any) {
            //TODO: id 3 issues
            return await _base.Get<SlideDeck>($"api/slideDeck/{any}");
        }
        public async Task<sr<List<SlideDeck>>> GetPublished() {
            return await Get(true);
        }
        public async Task<sr<List<SlideDeck>>> GetAsContentCreator() {
            return await Get(false);
        }
        private async Task<sr<List<SlideDeck>>> Get(bool onlyPublished) {
            var uri = onlyPublished ? "api/slideDeck" : "api/slideDeck/contentcreator";
      
            return await _base.Get<List<SlideDeck>>(uri);
        }

        public async Task<sr<Media>> UploadImage(FileUpload file) {
            return await _base.Post<FileUpload, Media>("api/slideDeck/file", file);
        }
    }
}
