using Learning.Server.DbContext;
using Learning.Server.Repositories;
using Learning.Server.Service;
using Learning.Shared.DbModels;
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
        readonly IUserAvatarRepo _userAvatarRepo;
        private readonly AppDbContext _dbContext;
        public UploadController(IAzureRepo azureRepo, IUserService userService, IUserAvatarRepo IUserAvatarRepo, AppDbContext dbContext) {
            _azureRepo = azureRepo;
            _userService = userService;
            _dbContext = dbContext;
            _userAvatarRepo = IUserAvatarRepo;
        }

        private IAzureRepo _azureRepo { get; set; }

        [HttpPost]
        [DisableFormValueModelBinding]
        [RequestSizeLimit(MaxFileSize)]
        [RequestFormLimits(MultipartBodyLengthLimit = MaxFileSize)]
        public async Task<IActionResult> ReceiveFile() {
            if (!MultipartRequestHelper.IsMultipartContentType(Request.ContentType))
                throw new Exception("Not a multipart request");

            var boundary = MultipartRequestHelper.GetBoundary(MediaTypeHeaderValue.Parse(Request.ContentType),_defaultFormOptions.MultipartBoundaryLengthLimit);
            var reader = new MultipartReader(boundary, Request.Body);

            // note: this is for a single file, you could also process multiple files
            var section = await reader.ReadNextSectionAsync();

            if (section == null)
                return BadRequest();

            Request.Headers.TryGetValue("CompletedSlideDeckProgram", out Microsoft.Extensions.Primitives.StringValues programIdStringValue);
            var reviewable = new CompletedProgramReviewable();
            reviewable.CompletedSlideDeckProgramId = int.Parse(programIdStringValue.ToString());
            var activeUserAvatar = await _userAvatarRepo.GetActiveInContext();
            reviewable.UserAvatarId = activeUserAvatar.Id;

            var i = 0;
            do {
                if (!ContentDispositionHeaderValue.TryParse(section.ContentDisposition, out var contentDisposition))
                    throw new Exception("No content disposition in multipart defined");

                var fileName2 = contentDisposition.FileNameStar.ToString();
                if (string.IsNullOrEmpty(fileName2)) {
                    fileName2 = contentDisposition.FileName.ToString();
                }

                
                var fileName = _userService.GetUserId().ToString() + "_" + programIdStringValue.ToString() + "_" + activeUserAvatar.Id + "_" + i + "." + fileName2.Split('.').Last();
                if (string.IsNullOrEmpty(fileName2))
                    throw new Exception("No filename defined.");

                using var fileStream = section.Body;

                var uri = await _azureRepo.UploadFileToStorage(fileStream, "dfghdfh", fileName);


                reviewable.Content.Add(new Shared.DbModels.AzureBlob() { Uri = uri.ToString(), Name = fileName2 });
                i++;
                section = await reader.ReadNextSectionAsync();
            } while (section != null);



            


            await _dbContext.CompletedProgramReviewables.AddAsync(reviewable);
            await _dbContext.SaveChangesAsync();
            //await SendFileSomewhere(fileStream);

            return Ok();
        }
    }
}
