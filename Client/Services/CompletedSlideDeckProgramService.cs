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
    public interface ICompletedSlideDeckProgramService {
        Task<sr<CompletedSlideDeckProgram>> Save(CompletedSlideDeckProgram completedProgram);
        Task<sr<List<CompletedSlideDeckProgram>>> GetAll(UserAvatar userAvatar);
        Task<sr<CompletedSlideDeckProgram>> GetShared(int id);
        Task<sr<CompletedSlideDeckProgram>> Get(int id);
    }

    public class CompletedSlideDeckProgramService : ICompletedSlideDeckProgramService {
        readonly HttpClient _http;
        private readonly ServiceBase2 _base;
        public CompletedSlideDeckProgramService(HttpClient http) {
            _http = http;
            _base = new ServiceBase2(http);
        }
        public async Task<sr<CompletedSlideDeckProgram>> Save(CompletedSlideDeckProgram completedProgram) {
            return await _base.Post<CompletedSlideDeckProgram, CompletedSlideDeckProgram>("api/CompletedSlideDeckProgram", completedProgram);
        }
        public async Task<sr<List<CompletedSlideDeckProgram>>> GetAll(UserAvatar userAvatar) {
            return await _base.Get<List<CompletedSlideDeckProgram>>($"api/CompletedSlideDeckProgram/all/{userAvatar.Id}");

        }
        public async Task<sr<CompletedSlideDeckProgram>> GetShared(int id) {
            return await _base.Get<CompletedSlideDeckProgram>($"api/CompletedSlideDeckProgram/all/shared/{id}");
        }
        public async Task<sr<CompletedSlideDeckProgram>> Get(int id) {
            return await _base.Get<CompletedSlideDeckProgram>($"api/CompletedSlideDeckProgram/any/{id}");
        }
    }
}
