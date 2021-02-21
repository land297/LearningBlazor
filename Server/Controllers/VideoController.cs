using Learning.Server.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learning.Server.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class VideoController : ControllerBase {
        private readonly IVideoRepo _videoRepo;

        public VideoController(IVideoRepo videoRepo) {
            _videoRepo = videoRepo;
        }
        [HttpGet("all")]
        public async Task<IActionResult> GetAll() {
            var result = await _videoRepo.GetAllVideos();
            if (!result.Success) {
                return BadRequest(result.Message);
            } else {
                return Ok(result.Data);
            }
        }
        [HttpGet("all/images")]
        public async Task<IActionResult> GetAllImages() {
            var result = await _videoRepo.GetAllImages();
            if (!result.Success) {
                return BadRequest(result.Message);
            } else {
                return Ok(result.Data);
            }
        }
    }
}
