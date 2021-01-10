using Learning.Client.Shared;
using Learning.Shared;
using Learning.Shared.DataTransferModel;
using Learning.Shared.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Learning.Client.Services {
    public interface IUserService {
        Task<sr<string>> Register(UserRegistration userRegistration);
        Task<sr<List<User>>> GetAll();
    }

    public class UserService : IUserService {
        readonly HttpClient _http;
        public UserService(HttpClient http) {
            _http = http;
        }

        public async Task<sr<string>> Register(UserRegistration userRegistration) {
            var response = await _http.PostAsJsonAsync<UserRegistration>("api/user", userRegistration);
            var content = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode) {
                return sr<string>.GetSuccess(content);
            } else {
                return sr<string>.Get(content);
            }
        }
        public async Task<sr<List<User>>> GetAll() {
            var response = await _http.GetAsync("api/user/all");

            if (response.IsSuccessStatusCode) {
                var stream = await response.Content.ReadAsStreamAsync();
                var possibleData = await stream.TryDeserializeJsonCamelCaseAsync<List<User>>();
                if (possibleData.Success) { 
                    return sr<List<User>>.GetSuccess(possibleData.Data);
                }
            } 
            var message = await response.Content.ReadAsStringAsync();
            return sr<List<User>>.Get(message);
        }
    }
}
