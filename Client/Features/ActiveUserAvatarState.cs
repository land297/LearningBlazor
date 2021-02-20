using BlazorState;
using Learning.Shared.DbModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Learning.Client.Features {
    public class ActiveUserAvatarState : State<ActiveUserAvatarState> {
        public UserAvatar UserAvatar { get; private set; }

        public override void Initialize() {
            UserAvatar = null;
        }

        public class ChangeActiveUserAvatarAction : IAction {
            public UserAvatar UserAvatar { get; set; }
        }
        public class ChangeActiveUserAvatarHandler : ActionHandler<ChangeActiveUserAvatarAction> {
            public ChangeActiveUserAvatarHandler(IStore s) : base(s) { }

            ActiveUserAvatarState State => Store.GetState<ActiveUserAvatarState>();

            public override Task<Unit> Handle(ChangeActiveUserAvatarAction changeActiveUserAvatar, CancellationToken aCancellationToken) {
                State.UserAvatar = changeActiveUserAvatar.UserAvatar;
                return Unit.Task;
            }
        }
    }
}
