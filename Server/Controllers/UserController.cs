using Learning.Server.Repositories;
using Learning.Shared.DataTransferModel;
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
        public async Task<IActionResult> UserRegistration(UserRegistration userRegistration) {
            //TODO: check if user is authorized
            var result = await _userRepo.AddUser(userRegistration);
            if (!result.Success) {
                return BadRequest(result.Message);
            } else {
                return Created($"api/user/{result.Data}",result.Data);
            }
        }
    }
}
