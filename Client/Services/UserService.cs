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
        Task<sr<User>> GetLogedInSelf();
    }

    public abstract class ServiceBase {
        protected HttpClient _http;
        public ServiceBase(HttpClient http) {
            _http = http;
        }

        protected async Task<sr<T>> Get<T>(string uri) {
            try {
                var response = await _http.GetAsync("api/user/self");
                if (response.IsSuccessStatusCode) {
                    var stream = await response.Content.ReadAsStreamAsync();
                    var obj = await stream.DeserializeJsonCamelCaseAsync<T>();
                    return sr<T>.GetSuccess(obj);
                } else {
                    var message = await response.Content.ReadAsStringAsync();
                    return sr<T>.Get(message);
                }
            } catch (Exception e) {
                return sr<T>.Get(e);
            }
        }
    }

    public class UserService : ServiceBase, IUserService {
        public UserService(HttpClient http) : base(http) {
        }

        public async Task<sr<string>> Register(UserRegistration userRegistration) {
            var response = await _http.PostAsJsonAsync<UserRegistration>("api/user/add", userRegistration);
            var content = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode) {
                return sr<string>.GetSuccess(content);
            } else {
                return sr<string>.Get(content);
            }
        }
        public async Task<sr<List<User>>> GetAll() {
            return await Get<List<User>>("api/user/all");
        }
        public async Task<sr<User>> GetLogedInSelf() {
            return await Get<User>("api/user/self");
        }
    }
}
