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
        public UserAvatarLocalService(IUserService userAvatarService, ILocalStorageService localStorageService) {
            _userService = userAvatarService;
            _localStorageService = localStorageService;
            _userService.OnChange += _userAvatarService_OnChange;
        }

        private async void _userAvatarService_OnChange() {
            await Get();
        }

        public async Task Set(UserAvatar userAvatar) {
            var id = _userService.GetUserId();
            await _localStorageService.SetItemAsync("activeUserAvatar" + id, userAvatar);
            OnChange?.Invoke();
        }

        public async Task<UserAvatar> Get() {
            var id = _userService.GetUserId();
            if (string.IsNullOrEmpty(id)) { return null; }

            Active = await _localStorageService.GetItemAsync<UserAvatar>("activeUserAvatar" + id);
            Console.WriteLine("!! returning active avatar " + Active.Name);
            return Active;
        }
    }
}
