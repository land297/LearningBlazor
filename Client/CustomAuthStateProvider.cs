﻿using Blazored.LocalStorage;
using Fluxor;
using Learning.Client.Features;
using Learning.Shared.DataTransferModel;
using MediatR;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;

namespace Learning.Client {
    public class CustomAuthStateProvider : AuthenticationStateProvider {
        private readonly ILocalStorageService localStorageService;
        private readonly IDispatcher _dispatcher;
        private readonly HttpClient _http;
        public CustomAuthStateProvider(ILocalStorageService localStorageService, HttpClient http, IDispatcher dispatcher) {
            this.localStorageService = localStorageService;
            _http = http;
            _dispatcher = dispatcher;
        }
        
        public override async Task<AuthenticationState> GetAuthenticationStateAsync() {
            Console.WriteLine("!! CustomAuthStateProvider - GetAuthenticationStateAsync");
            //var login = new Login()
            //{
            //    Email = "a@a.com",
            //    Password = "aaa"
            //};
            //var result = await _http.PostAsJsonAsync("api/auth/login", login);
            //var t = await result.Content.ReadAsStringAsync();

            string token = await localStorageService.GetItemAsStringAsync("token");
            //if (string.IsNullOrWhiteSpace(token)) {
            //    token = t;
            //}
            var identity = new ClaimsIdentity();
            _http.DefaultRequestHeaders.Authorization = null;

            if (!string.IsNullOrEmpty(token)) {


                identity = new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt");

                var expClaim = identity.Claims.First(x => x.Type == "exp").Value;
                Console.WriteLine(expClaim);
                var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                var expireDateTime = epoch.AddSeconds(double.Parse(expClaim));
                if (expireDateTime < DateTime.UtcNow) {
                    identity = new ClaimsIdentity();
                    Console.WriteLine("has expired");
                } else {
                    Console.WriteLine("token is valid");
                    // adding the token to all http calls
                    _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    _dispatcher.Dispatch(new Store.SessionUseCase.LoggedInViaTokenAction());
                }
                
            }

            var user = new ClaimsPrincipal(identity);
            var state = new AuthenticationState(user);
            var test = state.User.IsInRole("role2");
            Console.WriteLine($"!! User is in role role2: {test}");
            NotifyAuthenticationStateChanged(Task.FromResult(state));
            return state;
        }

        private byte[] ParseBase64WithoutPadding(string base64) {
            switch (base64.Length % 4) {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }
        public IEnumerable<Claim> ParseClaimsFromJwt(string jwt) {
            var payload = jwt.Split('.')[1];
            var jsonBytes = ParseBase64WithoutPadding(payload);
            var kvp = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

            kvp.TryGetValue(ClaimTypes.Role, out object roles);

            var claims = new List<Claim>();
            if (roles != null) {
                if (roles.ToString().Trim().StartsWith("[")) {
                    var parsedRoles = JsonSerializer.Deserialize<string[]>(roles.ToString());
                    foreach (var parsedRole in parsedRoles) {
                        claims.Add(new Claim(ClaimTypes.Role, parsedRole));
                    }
                } else {
                    claims.Add(new Claim(ClaimTypes.Role, roles.ToString()));
                }
                kvp.Remove(ClaimTypes.Role);
            }
            claims.AddRange(kvp.Select(k => new Claim(k.Key, k.Value.ToString())));

            return claims;
        }
    }
}
