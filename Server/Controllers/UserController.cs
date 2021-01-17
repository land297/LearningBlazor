using Learning.Server.Repositories;
using Learning.Server.Repositories.Base;
using Learning.Shared;
using Learning.Shared.DataTransferModel;
using Learning.Shared.DbModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learning.Server.Controllers {
    public abstract class ControllerBase2<TEntity,TDto> : ControllerBase where TEntity : IdEntity<TEntity> {

        protected IRepoBase3<TEntity,TDto> Repo;
        //public async Task<IActionResult> Save(TDto dto, string uri) {
        //    var result = await Repo.Save(dto);
        //    if (!result.Success) {
        //        return BadRequest(result.Message);
        //    } else {
        //        return Created($"{uri}{result.Data}", result.Data);
        //    }
        //}

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
        private readonly IUserRepo _userRepo;

        public UserController(IUserRepo userRepo, IRepoBase3<User, UserRegistration> repo3) {
            _userRepo = userRepo;
            Repo = repo3;
        }
        //[HttpPost("add2")]
        //public async Task<IActionResult> UserRegistration(UserRegistration userRegistration) {
        //    //return await Save(userRegistration, "api/user/");
        //    //var result = await _userRepo.AddUser(userRegistration);
        //    //if (!result.Success) {
        //    //    return BadRequest(result.Message);
        //    //} else {
        //    //    return Created($"api/user/{result.Data}",result.Data);
        //    //}
        //}
        [HttpPost("add")]
        public async Task<IActionResult> UserRegistration2(UserRegistration userRegistration) {
            return await Created(Repo.Save(userRegistration), "api/user/");
  
        }
        [HttpPost("addCompletedProgarm")]
        public async Task<IActionResult> SaveCompletedSlideDeckProgram(UserRegistration userRegistration) {
            var result = await _userRepo.AddUser(userRegistration);
            if (!result.Success) {
                return BadRequest(result.Message);
            } else {
                return Created($"api/user/{result.Data}", result.Data);
            }
        }
        [HttpGet("all")]
        public async Task<IActionResult> GetAll2() {
            return await Ok<IList<User>>(Repo.GetAll());
        }
        // TODO: having routes in the base class feels at he moment off..
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id) {
            return await Ok<User>(Repo.Get(id));
        }
    }
}
