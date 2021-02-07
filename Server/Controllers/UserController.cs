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
        //IRepoBase3<User> _repo;
        public UserController(IUserService userService, IUserRepo repo3) : base (repo3) {
            _userService = userService;
            //_repo = repo3;
        }
        [HttpPost("add")]
        public async Task<IActionResult> UserRegistration(UserRegistration userRegistration) {
            return await Created(RepoBase.Save(userRegistration), "api/user/");
  
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("all")]
        public async Task<IActionResult> GetAll() {
            return await Ok<List<User>>(RepoBase.GetAll());
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id) {
            return await Ok<User>(RepoBase.Get(id));
        }
        [Authorize]
        [HttpGet("self")]
        public async Task<IActionResult> GetSelf() {
            return await Ok<User>(RepoBase.Get(_userService.GetUserId()));
        }
    }
}
