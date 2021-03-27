﻿using Fluxor;
using Learning.Client.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Learning.Client.Store.ActiveAvatarUseCase {
    public class ActiveAvatarEffect : Effect<CheckServerForActiveAvatarAction> {
        private readonly IUserAvatarService _userAvatarService;
        public ActiveAvatarEffect(IUserAvatarService userAvatarService) {
            _userAvatarService = userAvatarService;
    }
        public override async Task HandleAsync(CheckServerForActiveAvatarAction action, IDispatcher dispatcher) {
            var response = await _userAvatarService.GetActive();
            if (response.Success) {
                dispatcher.Dispatch(new ChangeActiveAvatarAction(response.Data));
            }
        }
    }
}
