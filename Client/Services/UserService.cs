using Learning.Client.Services.Base;
using Learning.Client.Shared;
using Learning.Shared;
using Learning.Shared.DataTransferModel;
using Learning.Shared.DbModels;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Learning.Client.Services {
    public interface IUserService {
        Task<sr<string>> Register(UserRegistration userRegistration);
        Task<sr<List<User>>> GetAll();
        Task<sr<User>> GetLogedInSelf();
        public event Action AuthenticatedUserChanged;
        string GetUserId();
    }



    public class UserService : IUserService {
        public event Action AuthenticatedUserChanged;
        public ClaimsPrincipal UserClaims;

        private readonly AuthenticationStateProvider _authStateProvider;
        private readonly ServiceBase2 _base;
        public UserService(HttpClient http, AuthenticationStateProvider authStateProvider)  {
            _base = new ServiceBase2(http);
            _authStateProvider = authStateProvider;
            _authStateProvider.AuthenticationStateChanged += _authStateProvider_AuthenticationStateChanged;
        }

        private async void _authStateProvider_AuthenticationStateChanged(Task<AuthenticationState> task) {
            Console.WriteLine("!! ********************** ");
            var t = await task;
            UserClaims = t.User;
            Console.WriteLine("!! UserService - _authStateProvider_AuthenticationStateChanged - " + GetUserId());
            AuthenticatedUserChanged?.Invoke();
        }

        public async Task<sr<string>> Register(UserRegistration userRegistration) {
            return await _base.Post<UserRegistration,string>("api/user/add", userRegistration);

            
        }
        public async Task<sr<List<User>>> GetAll() {
            return await _base.Get<List<User>>("api/user/all");
        }
        public async Task<sr<User>> GetLogedInSelf() {
            return await _base.Get<User>("api/user/self");
        }
        public string GetUserId() {
            if (UserClaims == null) {
                Console.WriteLine("!! userclaims null");
                return string.Empty;
            }
            var id = UserClaims.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
            return (id != null) ? id.Value : string.Empty;
        }
    }
}
