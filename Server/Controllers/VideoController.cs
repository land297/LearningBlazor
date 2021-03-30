using Learning.Server.Repositories;
using Learning.Shared.DbModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Net.Http.Headers;
using System.Threading.Tasks;

namespace Learning.Server.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class VideoController : ControllerBase {
        private const long MaxFileSize = 1L * 1024L * 1024L * 1024L; // 1GB, adjust to your need
        // Get the default form options so that we can use them to set the default 
        // limits for request body data.
        private static readonly FormOptions _defaultFormOptions = new FormOptions();


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
                if (item == null) { continue; }
                string filename = ContentDispositionHeaderValue.Parse(item.ContentDisposition).FileName.ToString().Trim('"').Replace(" ", null);
                //TODO: not use filename
                var uri = await _azureRepo.NewBlobFromStream(item.OpenReadStream(), "publicblogupload", filename);
                Response.Headers.Add("name", uri.ToString());
                Response.StatusCode = 200;
                Response.ContentType = "application/json; charset=utf-8";
            }
            return Ok();
        }

        [HttpPost]
        [DisableFormValueModelBinding]
        [RequestSizeLimit(MaxFileSize)]
        [RequestFormLimits(MultipartBodyLengthLimit = MaxFileSize)]
        public async Task<IActionResult> ReceiveFile() {
            if (!MultipartRequestHelper.IsMultipartContentType(Request.ContentType))
                throw new Exception("Not a multipart request");

            var boundary = MultipartRequestHelper.GetBoundary(MediaTypeHeaderValue.Parse(Request.ContentType), _defaultFormOptions.MultipartBoundaryLengthLimit);
            var reader = new MultipartReader(boundary, Request.Body);

            // note: this is for a single file, you could also process multiple files
            var section = await reader.ReadNextSectionAsync();

            if (section == null)
                return BadRequest();

            Request.Headers.TryGetValue("Description", out Microsoft.Extensions.Primitives.StringValues videoDesecription);

            var i = 0;
            do {
                if (!ContentDispositionHeaderValue.TryParse(section.ContentDisposition, out var contentDisposition))
                    throw new Exception("No content disposition in multipart defined");

                var fileName2 = contentDisposition.FileNameStar.ToString();
                if (string.IsNullOrEmpty(fileName2)) {
                    fileName2 = contentDisposition.FileName.ToString();
                }

                using var fileStream = section.Body;

                var uri = await _azureRepo.NewBlobFromStream(fileStream, "videos", fileName2);

                await _videoRepo.AddVideo(new Media() { FullFileName = uri.ToString(), DisplayName = fileName2  + " " + videoDesecription, Type = contentDisposition.Name.Value });
                //media.Type = "image/" + file.FileName.Split('.').Last();
                //media.DisplayName = file.FileName;
                //media.FullFileName = "/upload/image/" + file.FileName;

                i++;
                section = await reader.ReadNextSectionAsync();
            } while (section != null);

            //await SendFileSomewhere(fileStream);

            return Ok();
        }
    }
}
