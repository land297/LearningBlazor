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
    public class UserController : ControllerBase {
        private readonly IUserRepo _userRepo;

        public UserController(IUserRepo userRepo) {
            _userRepo = userRepo;
        }
        [HttpPost]
        public async Task<IActionResult> AddUser(User user) {
            //TODO: check if user is authorized
            var result = await _userRepo.AddUser(user);
            if (!result.Success) {
                return BadRequest(result.Message);
            } else {
                return Created($"api/user/{user.Id}", user);
            }
        }
    }
}
