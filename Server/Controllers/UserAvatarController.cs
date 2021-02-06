using Learning.Server.Controllers.Base;
using Learning.Server.Repositories;
using Learning.Server.Repositories.Base;
using Learning.Shared.DbModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learning.Server.Controllers {
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserAvatarController : ControllerBase2<UserAvatar> {
        readonly IUserAvatarRepo _userAvatarRepo;
        //readonly IRepoBase3<UserAvatar> _repobase;
        readonly ILogger _logger;
        public UserAvatarController(ILogger<UserAvatarController> logger,
            IUserAvatarRepo repo3) : base(repo3) {
            _logger = logger;
            //RepoBase = repo3 as IRepoBase3<UserAvatar>;
            _userAvatarRepo = repo3;
        }

        
        [HttpPost]
        public async Task<IActionResult> Post(UserAvatar userAvatar) {
            //TODO: check if user is authorized
            //var result = await _userAvatarRepo.SaveInContext(userAvatar);
            //if (!result.Success) {
            //    return BadRequest(result.Message);
            //} else {
            //    return Created("uri purri prutt", userAvatar);
            //}

            return await Created(_userAvatarRepo.SaveInContext(userAvatar),"TODO: some uri that I will return");
        }

        [HttpPut("setactive/{id}")]
        public async Task<IActionResult> SetActive(int id) {
            _logger.LogInformation("setting active", id);
            //var result = await _userAvatarRepo.SetActiveInContext(id);
            //if (!result.Success) {
            //    return BadRequest(result.Message);
            //} else {
            //    _logger.LogInformation("ok");
            //    return Ok(result.Data);
            //}
            return await Ok(_userAvatarRepo.SetActiveInContext(id));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id) {
            //var result = await _userAvatarRepo.GetInContext(id);
            //if (!result.Success) {
            //    return BadRequest(result.Message);
            //} else {
            //    return Ok(result.Data);
            //}
            return await Ok(_userAvatarRepo.GetInContext(id));
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id) {
            //var result = await _userAvatarRepo.GetInContext(id);
            //if (!result.Success) {
            //    return BadRequest(result.Message);
            //} else {
            //    return Ok(result.Data);
            //}
            return await Ok(RepoBase.Remove(id));
        }
        [HttpPost("foruser")]
        public async Task<IActionResult> GetAllForUser(User user) {
            //var result = await _userAvatarRepo.GetAllForUser_NoBlob(user);
            //if (!result.Success) {
            //    return BadRequest(result.Message);
            //} else {
            //    return Ok(result.Data);
            //}
            return await Ok(_userAvatarRepo.GetAllForUser_NoBlob(user));
        }
        [HttpGet("foruserActive")]
        public async Task<IActionResult> GetActiveForUser() {
            //var result = await _userAvatarRepo.GetActiveInContext();
            //if (!result.Success) {
            //    return BadRequest(result.Message);
            //} else {
            //    return Ok(result.Data);
            //}
            return await Ok(_userAvatarRepo.GetActiveInContext());
        }
        [HttpGet("all")]
        public async Task<IActionResult> GetAll() {
            //TODO: check if user is authorized
            //var result = await _userAvatarRepo.GetAllInContext();
            //if (!result.Success) {
            //    return BadRequest(result.Message);
            //} else {
            //    return Ok(result.Data);
            //}
            return await Ok(_userAvatarRepo.GetAllInContext());
        }
    }
}
