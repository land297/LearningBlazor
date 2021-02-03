using Learning.Shared.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazored.LocalStorage;

namespace Learning.Client.Services {
    public interface IUserAvatarLocalService {
        Task<UserAvatar> Get();
        UserAvatar Active { get; }
        Task Set(UserAvatar userAvatar);
        event Action OnChange;
    }

    public class UserAvatarLocalService : IUserAvatarLocalService {
        public event Action OnChange;
        public UserAvatar Active { get; private set; }
        readonly IUserService _userService;
        readonly ILocalStorageService _localStorageService;
        private readonly IAuthService _authService;
        readonly IUserAvatarService _userAvatarService;

        public UserAvatarLocalService(IUserService userService,
            ILocalStorageService localStorageService, IAuthService authService,
            IUserAvatarService userAvatarService1) {
            _userService = userService;
            _userService.OnChange += _userAvatarService_OnChange;

            _localStorageService = localStorageService;
            
            _authService = authService;
            _authService.LoggedIn += _authService_LoggedIn;
            _authService.LoggedOut += _authService_LoggedOut;

            _userAvatarService = userAvatarService1;
        }

        private async void _authService_LoggedOut() {
            await Set(null);
        }

        private async void _authService_LoggedIn() {
            //throw new NotImplementedException();
            //var state = await _authStateProvider.GetAuthenticationStateAsync();
            //var user = state.User;
            // get active user avatar from database
        }

        private async void _userAvatarService_OnChange() {
            await Get();
        }

        public async Task Set(UserAvatar userAvatar) {
            //var id = _userService.GetUserId();
            await _localStorageService.SetItemAsync("activeUserAvatar", userAvatar);
            if (userAvatar != null) {
                await _userAvatarService.SetActive(userAvatar);
            }
            OnChange?.Invoke();
        }

        public async Task<UserAvatar> Get() {
            //var id = _userService.GetUserId();
            //if (string.IsNullOrEmpty(id)) { return null; }

            Active = await _localStorageService.GetItemAsync<UserAvatar>("activeUserAvatar");
            if (Active != null) {
                Console.WriteLine("!! returning active avatar " + Active.Name);
            } else {
                var response = await _userAvatarService.GetActive();
                if (response.Success) {
                    Active = response.Data;
                    await _localStorageService.SetItemAsync("activeUserAvatar", Active);
                }
            }

            return Active;
        }
    }
}
