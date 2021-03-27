using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fluxor;
using Learning.Shared.DbModels;

namespace Learning.Client.Store.ActiveAvatarUseCase {
    public class ActiveAvatarState {
        public UserAvatar Avatar { get; }
        public ActiveAvatarState(UserAvatar avatar) {
            Avatar = avatar;
        }
    }
    public class Feature : Feature<ActiveAvatarState> {
        public override string GetName() => "ActiveAvatarState";
        protected override ActiveAvatarState GetInitialState() =>
            new ActiveAvatarState(null);
    }

    public class ChangeActiveAvatarAction {
        public UserAvatar Avatar { get; }
        public ChangeActiveAvatarAction(UserAvatar avatar) {
            Avatar = avatar;
        }
    }
    public class CheckServerForActiveAvatarAction {
  
    }

    public class RemoveActiveAvatarAction {

    }
    public static class Reducers {
        [ReducerMethod]
        public static ActiveAvatarState ReduceIncrementCounterAction(ActiveAvatarState state, ChangeActiveAvatarAction action) =>
            new ActiveAvatarState(action.Avatar);
        [ReducerMethod]
        public static ActiveAvatarState ReduceIncrementCounterAction2(ActiveAvatarState state, CheckServerForActiveAvatarAction action) =>
    new ActiveAvatarState(null);
        [ReducerMethod]
        public static ActiveAvatarState ReduceIncrementCounterAction3(ActiveAvatarState state, RemoveActiveAvatarAction action) =>
    new ActiveAvatarState(null);

    }

}
