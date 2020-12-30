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
    public interface ISlideDeckService {
        Task<sr<SlideDeck>> Save(SlideDeck slideDeck);
    }

    public class SlideDeckService : ISlideDeckService {
        private readonly HttpClient _http;
        public SlideDeckService(HttpClient http) {
            _http = http;
        }
        public async Task<sr<SlideDeck>> Save(SlideDeck slideDeck) {
            // TODO: add user to set author
            slideDeck.AuthorId = 1;

            var response = await _http.PostAsJsonAsync<SlideDeck>("api/slideDeck", slideDeck);
            if (response.IsSuccessStatusCode) {
                var stream = await response.Content.ReadAsStreamAsync();
                slideDeck = await stream.DeserializeJsonCamelCaseAsync<SlideDeck>();
                return sr<SlideDeck>.GetSuccess(slideDeck);
            }
            return sr<SlideDeck>.Get();
        }
    }
}
