using Learning.Server.Service;
using Learning.Shared.DataTransferModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learning.Server.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService) {
            _authService = authService;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(Login login) {
            var response = await _authService.Login(login.Email, login.Password);
            return response.Success ? Created("TODO location what should this ?", response) : BadRequest(response);
        }
    }
}
