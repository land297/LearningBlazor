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

namespace Learning.Server.Controllers {
    public abstract class ControllerBase2<TEntity,TDto> : ControllerBase where TEntity : IdEntity<TEntity> {
        protected IRepoBase3<TEntity,TDto> Repo;
        public ControllerBase2(IRepoBase3<TEntity, TDto> repo) {
            Repo = repo;
        }
        public async Task<IActionResult> Ok<T>(Task<sr<T>> task) {
            var result = await task;
            if (!result.Success) {
                return BadRequest(result.Message);
            } else {
                return Ok(result.Data);
            }
        }
        public async Task<IActionResult> Created<T>(Task<sr<T>> task, string uri) {
            var result = await task;
            if (!result.Success) {
                return BadRequest(result.Message);
            } else {
                return Created($"{uri}{result.Data}", result.Data);
            }
        }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase2<User,UserRegistration> {
        IUserService _userService;
        public UserController(IUserService userService, IRepoBase3<User, UserRegistration> repo3) : base(repo3) {
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
