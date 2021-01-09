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
    }
}
