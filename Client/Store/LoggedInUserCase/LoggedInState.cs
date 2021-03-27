using Fluxor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learning.Client.Store.LoggedInUserCase {
    public class LoggedInState {
        public bool IsLoggedIn { get; }
        public LoggedInState(bool isLoggedIn) {
            IsLoggedIn = isLoggedIn;

        }
    }
    public class Feature : Feature<LoggedInState> {
        public override string GetName() => "LoggedIn";
        protected override LoggedInState GetInitialState() =>
            new LoggedInState(false);
    }
    public class LoggedInAction { }
    public class LoggedOutAction { }
    public static class Reducers {
        [ReducerMethod]
        public static LoggedInState ReduceLoggedInAction(LoggedInState state, LoggedInAction action) =>
            new LoggedInState(true);
        
        [ReducerMethod]
        public static LoggedInState ReduceLoggedOutAction(LoggedInState state, LoggedOutAction action) =>
          new LoggedInState(false);
        
    }
}
