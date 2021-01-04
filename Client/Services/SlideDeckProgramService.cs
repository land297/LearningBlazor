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
        Task<sr<SlideDeckProgram>> Save(SlideDeckProgram slideDeckProgram);
        Task<sr<SlideDeckProgram>> Get(int id);
        Task<sr<SlideDeckProgram>> Get(string slug);
    }

    public class SlideDeckProgramService : ISlideDeckProgramService {
        private readonly HttpClient _http;
        public SlideDeckProgramService(HttpClient http) {
            _http = http;
        }
        public async Task<sr<SlideDeckProgram>> Save(SlideDeckProgram slideDeckProgram) {
            // TODO: add user to set author
            slideDeckProgram.AuthorId = 1;

            var response = await _http.PostAsJsonAsync<SlideDeckProgram>("api/slideDeckProgram", slideDeckProgram);
            if (response.IsSuccessStatusCode) {
                var stream = await response.Content.ReadAsStreamAsync();
                slideDeckProgram = await stream.DeserializeJsonCamelCaseAsync<SlideDeckProgram>();
                return sr<SlideDeckProgram>.GetSuccess(slideDeckProgram);
            }
            return sr<SlideDeckProgram>.Get();
        }
        public async Task<sr<SlideDeckProgram>> Get(int id) {
            return await GetAny(id);
        }
        public async Task<sr<SlideDeckProgram>> Get(string slug) {
            return await GetAny(slug);
        }
        private async Task<sr<SlideDeckProgram>> GetAny(object any) {
            //TODO: id 3 issues
            try {
                var response = await _http.GetAsync($"api/slideDeckProgram/{any}");
                if (response.IsSuccessStatusCode) {
                    var stream = await response.Content.ReadAsStreamAsync();
                    var slideDeck = await stream.DeserializeJsonCamelCaseAsync<SlideDeckProgram>();
                    return sr<SlideDeckProgram>.GetSuccess(slideDeck);
                }
                return sr<SlideDeckProgram>.Get(response.ReasonPhrase);
            } catch (Exception e) {
                Console.WriteLine(e.Message);
                return sr<SlideDeckProgram>.Get(e.Message);
            }
        }
    }
}
