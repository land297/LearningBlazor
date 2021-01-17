using Learning.Server.Repositories;
using Learning.Server.Repositories.Base;
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

        protected readonly IRepoBase3<TEntity,TDto> Repo;
        public async Task<IActionResult> Save(TDto dto, string uri) {
            var result = await Repo.Save(dto);
            if (!result.Success) {
                return BadRequest(result.Message);
            } else {
                return Created($"{uri}{result.Data}", result.Data);
            }
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id) {
            var result = await Repo.Get(id);
            if (!result.Success) {
                return BadRequest(result.Message);
            } else {
                return Ok(result.Data);
            }
        }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase2<User,UserRegistration> {
        private readonly IUserRepo _userRepo;

        public UserController(IUserRepo userRepo) {
            _userRepo = userRepo;
        }
        [HttpPost("add")]
        public async Task<IActionResult> UserRegistration(UserRegistration userRegistration) {
            return await Save(userRegistration, "api/user/");
            //var result = await _userRepo.AddUser(userRegistration);
            //if (!result.Success) {
            //    return BadRequest(result.Message);
            //} else {
            //    return Created($"api/user/{result.Data}",result.Data);
            //}
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
        public async Task<IActionResult> GetAll() {
            var result = await _userRepo.GetAll();
            if (!result.Success) {
                return BadRequest(result.Message);
            } else {
                return Ok(result.Data);
            }
        }
    }
}
