using Learning.Server.DbContext;
using Learning.Server.Repositories;
using Learning.Server.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learning.Server.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase {
        private const long MaxFileSize = 10L * 1024L * 1024L * 1024L; // 10GB, adjust to your need
                                                                      
        // Get the default form options so that we can use them to set the default 
        // limits for request body data.
        private static readonly FormOptions _defaultFormOptions = new FormOptions();
        readonly IUserService _userService;
        private readonly AppDbContext _dbContext;
        public UploadController(IAzureRepo azureRepo, IUserService userService, AppDbContext dbContext) {
            _azureRepo = azureRepo;
            _userService = userService;
            _dbContext = dbContext;
        }

        private IAzureRepo _azureRepo { get; set; }

        [HttpPost]
        [DisableFormValueModelBinding]
        [RequestSizeLimit(MaxFileSize)]
        [RequestFormLimits(MultipartBodyLengthLimit = MaxFileSize)]
        public async Task ReceiveFile() {
            if (!MultipartRequestHelper.IsMultipartContentType(Request.ContentType))
                throw new Exception("Not a multipart request");

            var boundary = MultipartRequestHelper.GetBoundary(MediaTypeHeaderValue.Parse(Request.ContentType),_defaultFormOptions.MultipartBoundaryLengthLimit);
            var reader = new MultipartReader(boundary, Request.Body);

            // note: this is for a single file, you could also process multiple files
            var section = await reader.ReadNextSectionAsync();

            if (section == null)
                throw new Exception("No sections in multipart defined");
            var exercise = section.Headers.GetValueOrDefault("exercise");

            if (!ContentDispositionHeaderValue.TryParse(section.ContentDisposition, out var contentDisposition))
                throw new Exception("No content disposition in multipart defined");

            var fileName = contentDisposition.FileNameStar.ToString();
            if (string.IsNullOrEmpty(fileName)) {
                fileName = contentDisposition.FileName.ToString();
            }

            fileName = _userService.GetUserId().ToString() + "_" + exercise.ToString() + fileName.Split('.').Last();
            if (string.IsNullOrEmpty(fileName))
                throw new Exception("No filename defined.");

            using var fileStream = section.Body;
            
            var uri = await _azureRepo.UploadFileToStorage(fileStream, "dfghdfh",fileName);
            
            //await SendFileSomewhere(fileStream);
        }
    }
}
