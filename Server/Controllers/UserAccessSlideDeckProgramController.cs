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
    public class UserAccessSlideDeckProgramController : ControllerBase {
        private readonly IUserAccessSlideDeckProgramRepo _userAccessSlideDeckProgramRepo;
        readonly IUserRepo _userRepo;
        public UserAccessSlideDeckProgramController(IUserAccessSlideDeckProgramRepo userAccessSlideDeckProgramRepo, IUserRepo userRepo) {
            _userAccessSlideDeckProgramRepo = userAccessSlideDeckProgramRepo;
            _userRepo = userRepo;
        }

        //TODO: refactor get methods
        [HttpGet("userAvatar/{id:int}")]
        //public async Task<IActionResult> GetViaUserAvatar([FromBody] UserAvatar ua) {
        public async Task<IActionResult> GetViaUserAvatar(int id) {
            // TODO: need to check if user can get unpublished or not
            var result = await _userAccessSlideDeckProgramRepo.Get(new UserAvatar() { Id = id });
            if (!result.Success) {
                return BadRequest(result.Message);
            } else {
                return Ok(result.Data);
            }
        
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> RemoveAccessWithId(int id) {
            var result = await _userAccessSlideDeckProgramRepo.RemoveWithId(id);
            if (!result.Success) {
                return BadRequest(result.Message);
            } else {
                return NoContent();
            }
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAccessWithId(int id) {
            var result = await _userAccessSlideDeckProgramRepo.Get(id);
            if (!result.Success) {
                return BadRequest(result.Message);
            } else {
                return Ok(result.Data);
            }
        }
        [HttpGet("user")]
        public async Task<IActionResult> GetViaUser(User u) {
            // TODO: need to check if user can get unpublished or not
            var user = await _userRepo.GetUser(u.Email);
            if (user == null) {
                return BadRequest("No user");
            }
            var result = await _userAccessSlideDeckProgramRepo.Get(user);
            if (!result.Success) {
                return BadRequest(result.Message);
            } else {
                return Ok(result.Data);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Post(UserAccessSlideDeckProgram userAccess) {
            // TODO: need to check if user can get unpublished or not
            
            var result = await _userAccessSlideDeckProgramRepo.SaveReturnEntity(userAccess);
            if (!result.Success) {
                return BadRequest(result.Message);
            } else {
                return Ok(result.Data);
            }
        }
    }
}
