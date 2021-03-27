using Learning.Server.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Learning.Server.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class VideoController : ControllerBase {
        private readonly IVideoRepo _videoRepo;
        private IAzureRepo _azureRepo { get; set; }

        public VideoController(IVideoRepo videoRepo, IAzureRepo azureRepo) {
            _videoRepo = videoRepo;
            _azureRepo = azureRepo;
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

        //TODO: add auth
        [HttpPost("image/up")]
        public async Task<IActionResult> Rename(IList<IFormFile> UploadFiles) {
            foreach (var item in UploadFiles) {
                if(item == null) { continue; }
                string filename = ContentDispositionHeaderValue.Parse(item.ContentDisposition).FileName.Trim('"').Replace(" ",null);
                //TODO: not use filename
                var uri = await _azureRepo.NewBlobFromStream(item.OpenReadStream(), "publicblogupload", filename);
                Response.Headers.Add("name", uri.ToString());
                Response.StatusCode = 200;
                Response.ContentType = "application/json; charset=utf-8";
            }

            return Ok();
        }
    }
}
