using Learning.Client.Services.Base;
using Learning.Client.Shared;
using Learning.Shared;
using Learning.Shared.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Learning.Client.Services {
    public interface ISlideDeckProgramService {
        Task<sr<int>> Save(SlideDeckProgram slideDeckProgram);
        Task<sr<List<SlideDeckProgram>>> GetPublished();
        Task<sr<List<SlideDeckProgram>>> GetAsContentCreator();
        Task<sr<SlideDeckProgram>> Get(int id);
        Task<sr<SlideDeckProgram>> Get(string slug);
    }

    public class SlideDeckProgramService : ISlideDeckProgramService {
        private readonly HttpClient _http;
        private readonly ServiceBase2 _base;
        public SlideDeckProgramService(HttpClient http) {
            _http = http;
            _base = new ServiceBase2(http);
        }
        public async Task<sr<int>> Save(SlideDeckProgram slideDeckProgram) {
            // TODO: add user to set author
            slideDeckProgram.AuthorId = 1;
            return await _base.Post<SlideDeckProgram,int>("api/slideDeckProgram", slideDeckProgram);
        }
        public async Task<sr<SlideDeckProgram>> Get(int id) {
            return await GetAny(id);
        }
        public async Task<sr<SlideDeckProgram>> Get(string slug) {
            return await GetAny(slug);
        }
        private async Task<sr<SlideDeckProgram>> GetAny(object any) {
            return await _base.Get<SlideDeckProgram>($"api/slideDeckProgram/{any}");
        }
        public async Task<sr<List<SlideDeckProgram>>> GetPublished() {
            return await Get(true);
        }
        public async Task<sr<List<SlideDeckProgram>>> GetAsContentCreator() {
            return await Get(false);
        }
        private async Task<sr<List<SlideDeckProgram>>> Get(bool onlyPublished) {
            var uri = onlyPublished ? "api/slideDeckProgram" : "api/slideDeckProgram/contentcreator";
            return await _base.Get<List<SlideDeckProgram>>(uri);
        }
    }
}
