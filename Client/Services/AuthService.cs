using Blazored.LocalStorage;
using Learning.Shared;
using Learning.Shared.DataTransferModel;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Learning.Client.Services {
    public interface IAuthService {
        Task<sr<string>> Login(Login login);
        Task<sr<string>> Logout();
    }

    public class AuthService : IAuthService {
        readonly HttpClient _http;
        private readonly ILocalStorageService _localStorageService;
        private readonly AuthenticationStateProvider _authStateProvider;

        public AuthService(HttpClient http, ILocalStorageService localStorageService, AuthenticationStateProvider authStateProvider) {
            _http = http;
            _localStorageService = localStorageService;
            _authStateProvider = authStateProvider;
        }

        public async Task<sr<string>> Login(Login login) {
            var response = await _http.PostAsJsonAsync("api/auth/login", login);
            var content = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode) {
                await _localStorageService.SetItemAsync("token", content);
                await _authStateProvider.GetAuthenticationStateAsync();
                return sr<string>.GetSuccess(content);
            } else {
                await _localStorageService.SetItemAsync("token", string.Empty);
                return sr<string>.Get(content);
            }
        }
        public async Task<sr<string>> Logout() {
            //TODO: totaly fake, only delets token from localStorage.
            //      if saved, it could still be used
            await _localStorageService.SetItemAsync("token", string.Empty);
            await _authStateProvider.GetAuthenticationStateAsync();
            return sr<string>.GetSuccess("Loguut");

        }
    }
}
