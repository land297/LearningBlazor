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
    public interface IUserAvatarService {
        Task<sr<UserAvatar>> Get(int id);
        Task<sr<List<UserAvatar>>> GetAll();
        Task<sr<UserAvatar>> Save(UserAvatar userAvatar);
    }

    public class UserAvatarService : IUserAvatarService {
        readonly HttpClient _http;
        public UserAvatarService(HttpClient http) {
            _http = http;
        }

        public async Task<sr<UserAvatar>> Save(UserAvatar userAvatar) {
            var response = await _http.PostAsJsonAsync("api/userAvatar", userAvatar);
            var stream = await response.Content.ReadAsStreamAsync();
            if (response.IsSuccessStatusCode) {
                userAvatar = await stream.DeserializeJsonCamelCaseAsync<UserAvatar>();
                return sr<UserAvatar>.GetSuccess(userAvatar);
            } else {
                var error = await response.Content.ReadAsStringAsync();
                return sr<UserAvatar>.Get(error);
            }
        }
        public async Task<sr<UserAvatar>> Get(int id) {
            var response = await _http.GetAsync($"api/userAvatar/{id}");

            if (response.IsSuccessStatusCode) {
                var stream = await response.Content.ReadAsStreamAsync();
                var userAvatar = await stream.DeserializeJsonCamelCaseAsync<UserAvatar>();
                return sr<UserAvatar>.GetSuccess(userAvatar);
            } else {
                var error = await response.Content.ReadAsStringAsync();
                return sr<UserAvatar>.Get(error);
            }
        }
        public async Task<sr<List<UserAvatar>>> GetAll() {
            var response = await _http.GetAsync($"api/userAvatar/all");

            if (response.IsSuccessStatusCode) {
                var stream = await response.Content.ReadAsStreamAsync();
                var userAvatar = await stream.DeserializeJsonCamelCaseAsync<List<UserAvatar>>();
                return sr<List<UserAvatar>>.GetSuccess(userAvatar);
            } else {
                var error = await response.Content.ReadAsStringAsync();
                return sr<List<UserAvatar>>.Get(error);
            }
        }
    }
}
