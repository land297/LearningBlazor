using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fluxor;
using Learning.Shared.DbModels;

namespace Learning.Client.Store.SessionUseCase {
    public class SessionState {
        public UserAvatar Avatar { get; }
        public bool IsLoggedIn { get; }
        public SessionState(bool isLoggedIn, UserAvatar avatar) {
            Avatar = avatar;
            isLoggedIn = isLoggedIn;
        }
    }
    public class Feature : Feature<SessionState> {
        public override string GetName() => "SessionState";
        protected override SessionState GetInitialState() =>
            new SessionState(false,null);
    }

    public class ChangeActiveAvatarAction {
        public UserAvatar Avatar { get; }
        public bool IsLoggedIn { get; } = true;
        public ChangeActiveAvatarAction(UserAvatar avatar) {
            Avatar = avatar;
        }
    }
    public class LoggedInViaTokenAction { }
    public class RemoveActiveAvatarAction { }
    public class LoggedOutSessionAction { }
    public static class Reducers {
        [ReducerMethod]
        public static SessionState ReduceIncrementCounterAction(SessionState state, ChangeActiveAvatarAction action) => new SessionState(action.IsLoggedIn ,action.Avatar);
        [ReducerMethod]
        public static SessionState ReduceIncrementCounterAction2(SessionState state, LoggedInViaTokenAction action) => new SessionState(true,null);
        [ReducerMethod]
        public static SessionState ReduceIncrementCounterAction3(SessionState state, RemoveActiveAvatarAction action) => new SessionState(true,null);
        [ReducerMethod]
        public static SessionState ReduceIncrementCounterAction4(SessionState state, LoggedOutSessionAction action) => new SessionState(false, null);

    }

}
