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
    }

    public class CompletedSlideDeckProgramService : ICompletedSlideDeckProgramService {
        readonly HttpClient _http;
        public CompletedSlideDeckProgramService(HttpClient http) {
            _http = http;
        }
        public async Task<sr<CompletedSlideDeckProgram>> Save(CompletedSlideDeckProgram completedProgram) {
            var response = await _http.PostAsJsonAsync("api/CompletedSlideDeckProgram", completedProgram);
            if (response.IsSuccessStatusCode) {
                var stream = await response.Content.ReadAsStreamAsync();
                completedProgram = await stream.DeserializeJsonCamelCaseAsync<CompletedSlideDeckProgram>();
                return sr<CompletedSlideDeckProgram>.GetSuccess(completedProgram);
            } else {
                var error = await response.Content.ReadAsStringAsync();
                return sr<CompletedSlideDeckProgram>.Get(error);
            }
        }
        public async Task<sr<List<CompletedSlideDeckProgram>>> GetAll(UserAvatar userAvatar) {
            var response = await _http.GetAsync($"api/CompletedSlideDeckProgram/all/{userAvatar.Id}");
            if (response.IsSuccessStatusCode) {
                var stream = await response.Content.ReadAsStreamAsync();
                var result = await stream.TryDeserializeJsonCamelCaseAsync<List<CompletedSlideDeckProgram>>();
                return result;
            } else {
                var error = await response.Content.ReadAsStringAsync();
                return sr<List<CompletedSlideDeckProgram>>.Get(error);
            }
        }
        public async Task<sr<CompletedSlideDeckProgram>> GetShared(int id) {
            var response = await _http.GetAsync($"api/CompletedSlideDeckProgram/shared/{id}");
            if (response.IsSuccessStatusCode) {
                var stream = await response.Content.ReadAsStreamAsync();
                var result = await stream.DeserializeJsonCamelCaseAsync<CompletedSlideDeckProgram>();
                return sr<CompletedSlideDeckProgram>.GetSuccess(result);
            } else {
                var error = await response.Content.ReadAsStringAsync();
                return sr<CompletedSlideDeckProgram>.Get(error);
            }
        }
    }
}
