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
        Task<sr<List<SlideDeck>>> GetPublished();
        Task<sr<List<SlideDeck>>> GetAsContentCreator();
        Task<sr<SlideDeck>> Get(int id);
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
        public async Task<sr<SlideDeck>> Get(int id) {
            //TODO: id 3 issues
            try {
                var response = await _http.GetAsync($"api/slideDeck/{id}");
                if (response.IsSuccessStatusCode) {
                    var stream = await response.Content.ReadAsStreamAsync();
                    var slideDeck = await stream.DeserializeJsonCamelCaseAsync<SlideDeck>();
                    return sr<SlideDeck>.GetSuccess(slideDeck);
                }
                return sr<SlideDeck>.Get(response.ReasonPhrase);
            } catch (Exception e) {
                Console.WriteLine(e.Message);
                return sr<SlideDeck>.Get(e.Message);
            }
        }
        public async Task<sr<List<SlideDeck>>> GetPublished() {
            return await Get(true);
        }
        public async Task<sr<List<SlideDeck>>> GetAsContentCreator() {
            return await Get(false);
        }
        private async Task<sr<List<SlideDeck>>> Get(bool onlyPublished) {
            HttpResponseMessage response;
            if (onlyPublished) {
                response = await _http.GetAsync("api/slideDeck");
            } else {
                response = await _http.GetAsync("api/slideDeck/contentcreator");
            }
            if (response.IsSuccessStatusCode) {
                var stream = await response.Content.ReadAsStreamAsync();
                var data = await stream.DeserializeJsonCamelCaseAsync<List<SlideDeck>>();
                return sr<List<SlideDeck>>.GetSuccess(data);
            }
            return sr<List<SlideDeck>>.Get();
        }
    }
}
