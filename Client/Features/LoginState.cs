using BlazorState;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Learning.Client.Features {
    public partial class LoggedInState : State<LoggedInState> {
        public bool IsLoggedIn { get; private set; }
        public string UserId { get; private set; }
        public override void Initialize() => IsLoggedIn = false;


        public class LoggedInAction : IAction {
            public bool IsLoggedIn { get; set; }
            public string UserId { get; set; }
        }


        public class LoggedInActionHandler : ActionHandler<LoggedInAction> {
            public LoggedInActionHandler(IStore s) : base(s) { }

            LoggedInState State => Store.GetState<LoggedInState>();

            public override Task<Unit> Handle(LoggedInAction action, CancellationToken aCancellationToken) {
                State.IsLoggedIn = action.IsLoggedIn;
                State.UserId = action.UserId;
                return Unit.Task;
            }
        }
    }
}
