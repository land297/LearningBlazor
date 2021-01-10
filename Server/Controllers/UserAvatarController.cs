using Learning.Server.Repositories;
using Learning.Shared.DbModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learning.Server.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class UserAvatarController : ControllerBase {
        readonly IUserAvatarRepo _userAvatarRepo;
        public UserAvatarController(IUserAvatarRepo userAvatarRepo) {
            _userAvatarRepo = userAvatarRepo; ;
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
        [HttpGet("{id}}")]
        public async Task<IActionResult> Get(int id) {
            var result = await _userAvatarRepo.GetInContext(id);
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
