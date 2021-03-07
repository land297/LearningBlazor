using Learning.Shared.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using MediatR;
using Learning.Client.Features;

namespace Learning.Client.Services {
    public interface IUserAvatarLocalService {
        //Task<UserAvatar> Get(bool raiseEvent);
        UserAvatar ActiveUserAvatar { get; }
        Task Set(UserAvatar userAvatar);
        Task Delete(UserAvatar userAvatar);
        event Action<UserAvatar> UserAvatarChanged;
    }

    public class UserAvatarLocalService : IUserAvatarLocalService {
        public IMediator Mediator { get; set; }

        public event Action<UserAvatar> UserAvatarChanged;
        public UserAvatar ActiveUserAvatar { get; private set; }

        readonly IUserService _userService;
        readonly ILocalStorageService _localStorageService;
        readonly IAuthService _authService;
        readonly IUserAvatarService _userAvatarService;

        public UserAvatarLocalService(IUserService userService,
            ILocalStorageService localStorageService, IAuthService authService,
            IUserAvatarService userAvatarService1, IMediator mediator) {
            _userService = userService;
            //_userService.AuthenticatedUserChanged += _userAvatarService_OnChange;

            _localStorageService = localStorageService;
            
            _authService = authService;
            //_authService.LoggedIn += _authService_LoggedIn;
            //_authService.LoggedOut += _authService_LoggedOut;

            _userAvatarService = userAvatarService1;

            Mediator = mediator;
        }

        private async void _authService_LoggedOut() {
            await Set(null);
        }

        //private async void _authService_LoggedIn() {
        //    //throw new NotImplementedException();
        //    //var state = await _authStateProvider.GetAuthenticationStateAsync();
        //    //var user = state.User;
        //    // get active user avatar from database
        //}

        //private async void _userAvatarService_OnChange() {
        //    await Get();
        //}

        public async Task Set(UserAvatar userAvatar) {
            //var id = _userService.GetUserId();
            await _localStorageService.SetItemAsync("activeUserAvatar" + _userService.GetUserId(), userAvatar);
            if (userAvatar != null) {
                await _userAvatarService.SetActive(userAvatar);
            }
            ActiveUserAvatar = userAvatar;

            UserAvatarChanged?.Invoke(ActiveUserAvatar);
            await Mediator.Send(new ActiveUserAvatarState.ChangeActiveUserAvatarAction { UserAvatar  = ActiveUserAvatar });
        }
        public async Task Delete(UserAvatar userAvatar) {
            if (userAvatar != null) {
                await _userAvatarService.Delete(userAvatar);
            }
        }
        //public async Task<UserAvatar> Get(bool raiseEvent = true) {
        //    if (_userService.GetUserId() == string.Empty) { return null; }
        //    //var id = _userService.GetUserId();
        //    //if (string.IsNullOrEmpty(id)) { return null; }
        //    Console.WriteLine("!! UserAvatarLocalService Get");
        //    ActiveUserAvatar = await _localStorageService.GetItemAsync<UserAvatar>("activeUserAvatar" + _userService.GetUserId());
        //    if (ActiveUserAvatar != null && ActiveUserAvatar.UserId.ToString() == _userService.GetUserId()) {
        //        Console.WriteLine("!! returning active avatar " + ActiveUserAvatar.Name);
        //    } else {
        //        var response = await _userAvatarService.GetActive();
        //        if (response.Success) {
        //            await Set(response.Data);
        //        }
        //    }
        //    if (raiseEvent) {
        //        UserAvatarChanged?.Invoke(ActiveUserAvatar);
        //    }
        //    return ActiveUserAvatar;
        //}
    }
}
