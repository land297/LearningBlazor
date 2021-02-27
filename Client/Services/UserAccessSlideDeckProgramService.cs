using Learning.Client.Services.Base;
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



    public class UserAccessSlideDeckProgramService : IUserAccessSlideDeckProgramService {
        private readonly ServiceBase2 _base;
        public UserAccessSlideDeckProgramService(HttpClient http) {
            _base = new ServiceBase2(http);
        }
        public async Task<sr<List<UserAccessSlideDeckProgram>>> GetViaUserAvatar(int id) {
            var response = await _base.Get<List<UserAccessSlideDeckProgram>>($"api/UserAccessSlideDeckProgram/userAvatar/{id}");
            return response;
           
        }
        public async Task<sr<List<UserAccessSlideDeckProgram>>> GetViaUser(int id) {
            var response = await _base.Get<List<UserAccessSlideDeckProgram>>($"api/UserAccessSlideDeckProgram/user/{id}");
            return response;
        }
        public async Task<sr<UserAccessSlideDeckProgram>> GetAccessWithId(int id) {
            return await _base.Get<UserAccessSlideDeckProgram>($"api/UserAccessSlideDeckProgram/{id}");
            
        }
        public async Task<sr<bool>> DeleteAccessWithId(int id) {
            return await _base.Delete($"api/UserAccessSlideDeckProgram/{id}");
        }

        public async Task<sr<UserAccessSlideDeckProgram>> PostToUserAvatar(UserAccessSlideDeckProgram ua) {
            return await _base.Post<UserAccessSlideDeckProgram,UserAccessSlideDeckProgram>($"api/UserAccessSlideDeckProgram", ua);
        }
    }
}
