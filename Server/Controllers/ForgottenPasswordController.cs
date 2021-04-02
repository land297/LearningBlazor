using Learning.Server.Repositories;
using Learning.Server.Service;
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
    public class ForgottenPasswordController : ControllerBase {
        private readonly IMailKitEmailSender _emailSender;
        private readonly IForgottenPasswordRepo _forgottenPasswordRepo;

        public ForgottenPasswordController(IMailKitEmailSender emailSender, IForgottenPasswordRepo forgottenPasswordRepo) {
            _emailSender = emailSender;
            _forgottenPasswordRepo = forgottenPasswordRepo;
        }

        [HttpPost]
        public async Task<IActionResult> Requestx(StringValue sv) {
            var request = await _forgottenPasswordRepo.NewRquest(sv.Value);
            if (request == null) {
                return BadRequest();
            }
            await SendRequestEmail(request);
            return Ok();

        }
        private async Task SendRequestEmail(ForgottenPasswordRequest request) {
            var subject = "Forgotten password";
            var body = "You reset-code is: " + request.Code;
            await _emailSender.SendEmailAsync(request.Email, subject, body);
        }
        [HttpGet("reset/{code}/{password}")]
        public async Task<IActionResult> Requestxx(string code,string password) {
            var r = await _forgottenPasswordRepo.CheckValidRequst(code,password);
            return r ? Ok() : BadRequest();

        }
        //[HttpPost]
        //public async Task<IActionResult> Send(string toAddress) {
        //    var subject = "sample subject";
        //    var body = "sample body";
        //    await _emailSender.SendEmailAsync("henrik@land297.se", subject, body);
        //    return Ok();
        //}
    }
}
