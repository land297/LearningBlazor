using Learning.Shared;
using Learning.Shared.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace Learning.Client.Services {
    public interface IVideoService {

        Task<sr<List<Media>>> GetVideos();
        Task<sr<List<Media>>> GetImages();
    }

    public class VideoService : IVideoService {
        private readonly HttpClient _http;
        public VideoService(HttpClient http) {
            _http = http;
        }
        public async Task<sr<List<Media>>> GetVideos() {
            var response = await _http.GetAsync("api/video/all");
            if (response.IsSuccessStatusCode) {
                var stream = await response.Content.ReadAsStreamAsync();
                var options = new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };
                var list = await System.Text.Json.JsonSerializer.DeserializeAsync<List<Media>>(stream,options);

                return sr<List<Media>>.GetSuccess(list);

            }

            return sr<List<Media>>.Get();
        }
        public async Task<sr<List<Media>>> GetImages() {
            var response = await _http.GetAsync("api/video/all/images");
            if (response.IsSuccessStatusCode) {
                var stream = await response.Content.ReadAsStreamAsync();
                var options = new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };
                var list = await System.Text.Json.JsonSerializer.DeserializeAsync<List<Media>>(stream, options);

                return sr<List<Media>>.GetSuccess(list);

            }

            return sr<List<Media>>.Get();
        }
        //public async Task<bool> SaveExercise(Exercise exercise) {
        //    var response = await _http.PostAsJsonAsync<Exercise>("api/content", exercise);
        //    return false;
        //}
    }
}
