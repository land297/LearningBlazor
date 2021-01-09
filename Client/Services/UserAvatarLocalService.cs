using Learning.Shared.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazored.LocalStorage;

namespace Learning.Client.Services {
    public interface IUserAvatarLocalService {
        Task<UserAvatar> Get();

        Task Set(UserAvatar userAvatar);
        event Action OnChange;
    }

    public class UserAvatarLocalService : IUserAvatarLocalService {
        public event Action OnChange;
        public UserAvatar Active { get; private set; }
        readonly IUserAvatarService _userAvatarService;
        readonly ILocalStorageService _localStorageService;
        public UserAvatarLocalService(IUserAvatarService userAvatarService, ILocalStorageService localStorageService) {
            _userAvatarService = userAvatarService;
            _localStorageService = localStorageService;
        }

        public async Task Set(UserAvatar userAvatar) {
            await _localStorageService.SetItemAsync("activeUserAvatar", userAvatar);
            OnChange?.Invoke();
        }

        public async Task<UserAvatar> Get() {
            Active = await _localStorageService.GetItemAsync<UserAvatar>("activeUserAvatar");
            return Active;
        }
    }
}
