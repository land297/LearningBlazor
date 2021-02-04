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
        Task<sr<List<UserAvatar>>> GetAllFor(User user);
        Task<sr<UserAvatar>> SetActive(UserAvatar userAvatar);
        Task<sr<UserAvatar>> GetActive();
        }

    public class UserAvatarService : IUserAvatarService {
        readonly HttpClient _http;
        public UserAvatarService(HttpClient http) {
            _http = http;
        }

        public async Task<sr<UserAvatar>> Save(UserAvatar userAvatar) {
            var response = await _http.PostAsJsonAsync("api/userAvatar", userAvatar);
            if (response.IsSuccessStatusCode) {
                var stream = await response.Content.ReadAsStreamAsync();
                var t = await stream.TryDeserializeJsonCamelCaseAsync<UserAvatar>();
                if (t.Success) {
                    return sr<UserAvatar>.GetSuccess(t.Data);
                } else {
                    var s = await response.Content.ReadAsStringAsync();
                    return sr<UserAvatar>.Get(s);
                }
            } else {
                var error = await response.Content.ReadAsStringAsync();
                return sr<UserAvatar>.Get(error);
            }
        }
        public async Task<sr<UserAvatar>> SetActive(UserAvatar userAvatar) {
            var response = await _http.PutAsJsonAsync($"api/userAvatar/setactive/{userAvatar.Id}", userAvatar.Id);
            if (response.IsSuccessStatusCode) {
                var stream = await response.Content.ReadAsStreamAsync();
                var t = await stream.TryDeserializeJsonCamelCaseAsync<UserAvatar>();
                if (t.Success) {
                    return sr<UserAvatar>.GetSuccess(t.Data);
                } else {
                    var s = await response.Content.ReadAsStringAsync();
                    return sr<UserAvatar>.Get(s);
                }
            } else {
                var error = await response.Content.ReadAsStringAsync();
                return sr<UserAvatar>.Get(error);
            }
        }
        public async Task<sr<UserAvatar>> GetActive() {
            var response = await _http.GetAsync($"api/useravatar/foruserActive");

            if (response.IsSuccessStatusCode) {
                var stream = await response.Content.ReadAsStreamAsync();
                var t = await stream.TryDeserializeJsonCamelCaseAsync<UserAvatar>();
                if (t.Success) {
                    return sr<UserAvatar>.GetSuccess(t.Data);
                } else {
                    var s = await response.Content.ReadAsStringAsync();
                    return sr<UserAvatar>.Get(s);
                }
            } else {
                var error = await response.Content.ReadAsStringAsync();
                return sr<UserAvatar>.Get(error);
            }
        }
        public async Task<sr<UserAvatar>> Get(int id) {
            var response = await _http.GetAsync($"api/useravatar/{id}");
            
            if (response.IsSuccessStatusCode) {
                var stream = await response.Content.ReadAsStreamAsync();
                var t = await stream.TryDeserializeJsonCamelCaseAsync<UserAvatar>();
                if (t.Success) {
                    return sr<UserAvatar>.GetSuccess(t.Data);
                } else {
                    var s = await response.Content.ReadAsStringAsync();
                    return sr<UserAvatar>.Get(s);
                }
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
        public async Task<sr<List<UserAvatar>>> GetAllFor(User user) {
             var response = await _http.PostAsJsonAsync<User>($"api/userAvatar/foruser", user);
            
            if (response.IsSuccessStatusCode) {
                var stream = await response.Content.ReadAsStreamAsync();
                var userAvatar = await stream.DeserializeJsonCamelCaseAsync<List<UserAvatar>>();
                return sr<List<UserAvatar>>.GetSuccess(userAvatar);
            } else {
                var error = await response.Content.ReadAsStringAsync();
                return sr<List<UserAvatar>>.Get(error + " " + response.StatusCode + " " + response.RequestMessage.RequestUri);
            }
        }
    }
}
