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
    }
}
