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
    public interface IUserAccessSlideDeckProgramService {
        Task<sr<List<UserAccessSlideDeckProgram>>> GetViaUser(int id);
        Task<sr<List<UserAccessSlideDeckProgram>>> GetViaUserAvatar(int id);
        Task<sr<bool>> DeleteAccessWithId(int id);
        Task<sr<UserAccessSlideDeckProgram>> PostToUserAvatar(UserAccessSlideDeckProgram ua);
    }

    public abstract class ClientServiceBase<T> {
        protected readonly HttpClient _http;
        public ClientServiceBase(HttpClient http) {
            _http = http;
        }
        public async Task<sr<T>> Get(string uri) {
            var response = await _http.GetAsync(uri);

            if (response.IsSuccessStatusCode) {
                var stream = await response.Content.ReadAsStreamAsync();
                var t = await stream.TryDeserializeJsonCamelCaseAsync<T>();
                if (t.Success) {
                    return sr<T>.GetSuccess(t.Data);
                } else {
                    var s = await response.Content.ReadAsStringAsync();

                    Console.WriteLine(s);
                    Console.WriteLine(response.RequestMessage.RequestUri);
                    Console.WriteLine(response.RequestMessage.Content);

                    return sr<T>.Get(response.RequestMessage.RequestUri + " " + response.Headers.Location + " " + s);
                }
            } else {
                var error = await response.Content.ReadAsStringAsync();
                return sr<T>.Get(response.Headers.Location + error);
            }
        }
        public async Task<sr<T>> Post<TJson>(string uri, TJson json) {
            var response = await _http.PostAsJsonAsync(uri, json);

            if (response.IsSuccessStatusCode) {
                var stream = await response.Content.ReadAsStreamAsync();
                var t = await stream.TryDeserializeJsonCamelCaseAsync<T>();
                if (t.Success) {
                    return sr<T>.GetSuccess(t.Data);
                } else {
                    var s = await response.Content.ReadAsStringAsync();

                    Console.WriteLine(s);
                    Console.WriteLine(response.RequestMessage.RequestUri);
                    Console.WriteLine(response.RequestMessage.Content);

                    return sr<T>.Get(response.RequestMessage.RequestUri + " " + response.Headers.Location + " " + s);
                }
            } else {
                var error = await response.Content.ReadAsStringAsync();
                return sr<T>.Get(response.Headers.Location + error);
            }
        }
        public async Task<sr<bool>> Delete(string uri) {
            var response = await _http.DeleteAsync(uri);

            if (response.IsSuccessStatusCode) {
                return sr<bool>.GetSuccess(true);
            } else {
                var error = await response.Content.ReadAsStringAsync();
                return sr<bool>.Get(response.Headers.Location + error);
            }
        }
    }

    public class UserAccessSlideDeckProgramService : ClientServiceBase<UserAccessSlideDeckProgram>, IUserAccessSlideDeckProgramService {
        public UserAccessSlideDeckProgramService(HttpClient http) : base(http) {
        
        }
        public async Task<sr<List<UserAccessSlideDeckProgram>>> GetViaUserAvatar(int id) {
            var response = await _http.GetAsync($"api/UserAccessSlideDeckProgram/userAvatar/{id}");

            if (response.IsSuccessStatusCode) {
                var stream = await response.Content.ReadAsStreamAsync();
                var t = await stream.TryDeserializeJsonCamelCaseAsync<List<UserAccessSlideDeckProgram>>();
                if (t.Success) {
                    return sr<List<UserAccessSlideDeckProgram>>.GetSuccess(t.Data);
                } else {
                    var s = await response.Content.ReadAsStringAsync();
                    
                    Console.WriteLine(s);
                    Console.WriteLine(response.RequestMessage.RequestUri);
                    Console.WriteLine(response.RequestMessage.Content);

                    return sr<List<UserAccessSlideDeckProgram>>.Get(response.Headers.Location + " " + s);
                }
            } else {
                var error = await response.Content.ReadAsStringAsync();
                return sr<List<UserAccessSlideDeckProgram>>.Get(response.Headers.Location + error);
            }
        }
        public async Task<sr<List<UserAccessSlideDeckProgram>>> GetViaUser(int id) {
            var response = await _http.GetAsync($"api/UserAccessSlideDeckProgram/user/{id}");

            if (response.IsSuccessStatusCode) {
                var stream = await response.Content.ReadAsStreamAsync();
                var t = await stream.TryDeserializeJsonCamelCaseAsync<List<UserAccessSlideDeckProgram>>();
                if (t.Success) {
                    return sr<List<UserAccessSlideDeckProgram>>.GetSuccess(t.Data);
                } else {
                    var s = await response.Content.ReadAsStringAsync();
                    return sr<List<UserAccessSlideDeckProgram>>.Get(this.ToString() + s);
                }
            } else {
                var error = await response.Content.ReadAsStringAsync();
                return sr<List<UserAccessSlideDeckProgram>>.Get(this.ToString() + error);
            }
        }
        public async Task<sr<UserAccessSlideDeckProgram>> GetAccessWithId(int id) {
            return await Get("api/UserAccessSlideDeckProgram/{id}");
            
        }
        public async Task<sr<bool>> DeleteAccessWithId(int id) {
            return await Delete($"api/UserAccessSlideDeckProgram/{id}");
        }

        public async Task<sr<UserAccessSlideDeckProgram>> PostToUserAvatar(UserAccessSlideDeckProgram ua) {
            return await Post<UserAccessSlideDeckProgram>($"api/UserAccessSlideDeckProgram", ua);
        }
    }
}
