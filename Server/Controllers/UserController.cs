using Learning.Server.Service;
using Learning.Server.Repositories;
using Learning.Server.Repositories.Base;
using Learning.Shared;
using Learning.Shared.DataTransferModel;
using Learning.Shared.DbModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Learning.Server.Controllers.Base;

namespace Learning.Server.Controllers {


    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase2<User> {
        IUserService _userService;
        public UserController(IUserService userService, IRepoBase3<User> repo3) : base(repo3) {
            _userService = userService;
        }
        [HttpPost("add")]
        public async Task<IActionResult> UserRegistration(UserRegistration userRegistration) {
            return await Created(Repo.Save(userRegistration), "api/user/");
  
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("all")]
        public async Task<IActionResult> GetAll() {
            return await Ok<IList<User>>(Repo.GetAll());
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id) {
            return await Ok<User>(Repo.Get(id));
        }
        [Authorize]
        [HttpGet("self")]
        public async Task<IActionResult> GetSelf() {
            return await Ok<User>(Repo.Get(_userService.GetUserId()));
        }
    }
}
