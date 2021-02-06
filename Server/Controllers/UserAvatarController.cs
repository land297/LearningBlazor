using Learning.Server.Repositories;
using Learning.Shared.DbModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learning.Server.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class UserAvatarController : ControllerBase {
        readonly IUserAvatarRepo _userAvatarRepo;
        readonly ILogger _logger;
        public UserAvatarController(IUserAvatarRepo userAvatarRepo,ILogger<UserAvatarController> logger) {
            _userAvatarRepo = userAvatarRepo;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Post(UserAvatar userAvatar) {
            //TODO: check if user is authorized
            var result = await _userAvatarRepo.SaveInContext(userAvatar);
            if (!result.Success) {
                return BadRequest(result.Message);
            } else {
                return Created("uri purri prutt", userAvatar);
            }
        }
        [HttpPut("setactive/{id}")]
        public async Task<IActionResult> SetActive(int id) {
            _logger.LogInformation("setting active", id);
            var result = await _userAvatarRepo.SetActiveInContext(id);
            if (!result.Success) {
                return BadRequest(result.Message);
            } else {
                _logger.LogInformation("ok");
                return Ok(result.Data);
            }
        }
        //[HttpGet("test/{id:int}")]
        //public async Task<IActionResult> GetTest(int id) {
        //    var result = await _userAvatarRepo.GetInContext(id);
        //    if (!result.Success) {
        //        return BadRequest(result.Message);
        //    } else {
        //        return Ok(result.Data);
        //    }
        //}
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id) {
            var result = await _userAvatarRepo.GetInContext(id);
            if (!result.Success) {
                return BadRequest(result.Message);
            } else {
                return Ok(result.Data);
            }
        }
        [HttpPost("foruser")]
        public async Task<IActionResult> GetAllForUser(User user) {
            var result = await _userAvatarRepo.GetAllForUser_NoBlob(user);
            if (!result.Success) {
                return BadRequest(result.Message);
            } else {
                return Ok(result.Data);
            }
        }
        [HttpGet("foruserActive")]
        public async Task<IActionResult> GetActiveForUser() {
            var result = await _userAvatarRepo.GetActiveInContext();
            if (!result.Success) {
                return BadRequest(result.Message);
            } else {
                return Ok(result.Data);
            }
        }
        [HttpGet("all")]
        public async Task<IActionResult> GetAll() {
            //TODO: check if user is authorized
            var result = await _userAvatarRepo.GetAllInContext();
            if (!result.Success) {
                return BadRequest(result.Message);
            } else {
                return Ok(result.Data);
            }
        }
    }
}
