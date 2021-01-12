using Learning.Client.Shared;
using Learning.Shared;
using Learning.Shared.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Learning.Client.Services {
    public interface IUserAccessSlideDeckProgramService {
        Task<sr<List<UserAccessSlideDeckProgram>>> GetViaUser(int id);
        Task<sr<List<UserAccessSlideDeckProgram>>> GetViaUserAvatar(int id);
    }

    public class UserAccessSlideDeckProgramService : IUserAccessSlideDeckProgramService {
        readonly HttpClient _http;
        public UserAccessSlideDeckProgramService(HttpClient http) {
            _http = http;
        }
        public async Task<sr<List<UserAccessSlideDeckProgram>>> GetViaUserAvatar(int id) {
            var response = await _http.GetAsync($"api/UserAccessSlideDeckProgram/useravatar/{id}");

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
    }
}
