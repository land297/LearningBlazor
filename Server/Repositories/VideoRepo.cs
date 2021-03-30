using Learning.Server.DbContext;
using Learning.Shared;
using Learning.Shared.DataTransferModel;
using Learning.Shared.DbModels;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Learning.Server.Repositories {
    public interface IVideoRepo {
        Task<sr<IList<Media>>> GetAllVideos();
        Task<sr<IList<Media>>> GetAllImages();
        FileUpload file { get; set; }
        //public Task Test();
        public Task<Media> SaveIamgeToFtp();
        Task AddVideo(Media media);
    }

    public class VideoRepo : IVideoRepo {
        readonly IConfiguration _config;
        private readonly AppDbContext _dbContext;
        public string ContentUri { get; set; } = @"https://land297.se/test";
        public string ContentUriFtp { get; set; } = @"ftp://ftpcluster.loopia.se";
        //https://land297.se/test/upload/video/aggbollarmhaavningar.mp4
        IAzureRepo _azureRepo;
        public VideoRepo(AppDbContext dbContext, IConfiguration config, IAzureRepo azurerepo) {
            _dbContext = dbContext;
            _config = config;
            _azureRepo = azurerepo;
        }
        public async Task AddVideo(Media media) {
            await _dbContext.Media.AddAsync(media);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<sr<IList<Media>>> GetAllVideos() {
            var response = sr<IList<Media>>.Get();
            try {
                response.Data = await _dbContext.Media.Where(m => m.Type == "video/mp4").ToListAsync();
                foreach (var video in response.Data) {
                    if (!video.FullFileName.Contains("https://")) {
                        video.FullFileName = ContentUri + video.FullFileName;
                    } else {
                        video.FullFileName = (await _azureRepo.GetSasUriForBlob(new Uri(video.FullFileName))).ToString();
                    }
                }
                response.Success = true;
            } catch (Exception e) {
                response.Message = e.Message;
            }
            return response;
        }
        public async Task<sr<IList<Media>>> GetAllImages() {
            var response = sr<IList<Media>>.Get();
            try {
                response.Data = await _dbContext.Media.Where(m => m.Type.Contains("image/")).ToListAsync();
                foreach (var video in response.Data) {
                    video.FullFileName = ContentUri + video.FullFileName;
                }
                response.Success = true;
            } catch (Exception e) {
                response.Message = e.Message;
            }
            return response;
        }

        public FileUpload file { get; set; }
        //public async Task Test() {
        //    //await Task.Factory.StartNew<bool>(SaveIamgeToFtp);
        //}

        public async Task<Media> SaveIamgeToFtp() {
            //csharptest/csharptest

            string folder = ContentUriFtp + "/upload/image/";
            // Create an FtpWebRequest
            var request = (FtpWebRequest)WebRequest.Create(folder + file.FileName);
            //Set the method to UploadFile
            request.Method = WebRequestMethods.Ftp.UploadFile;
            //Set the NetworkCredentials
            var username = _config["AppSettings:FtpUserName"];
            var password = _config["AppSettings:FtpPasswrod"];
            request.Credentials = new NetworkCredential(username, password);

            //Set buffer length to any value you find appropriate for your use case
            byte[] buffer = new byte[1024];
            //var stream = file.OpenReadStream();
            byte[] fileContents = file.FileContent;
            //Copy everything to the 'fileContents' byte array
            //using (var ms = new MemoryStream()) {
            //    int read;
            //    while ((read = stream.Read(buffer, 0, buffer.Length)) > 0) {
            //        ms.Write(buffer, 0, read);
            //    }
            //    fileContents = ms.ToArray();
            //}

            //Upload the 'fileContents' byte array 
            using (Stream requestStream = request.GetRequestStream()) {
                requestStream.Write(fileContents, 0, fileContents.Length);
            }

            //Get the response
            // TODO: fix this shit. 
            var response = (FtpWebResponse)request.GetResponse();
            if (response.StatusCode == FtpStatusCode.FileActionOK || response.StatusCode == FtpStatusCode.ClosingData) {
                var media = new Media();
                media.Type = "image/" + file.FileName.Split('.').Last();
                media.DisplayName = file.FileName;
                media.FullFileName = "/upload/image/" + file.FileName;
                await _dbContext.Media.AddAsync(media);
                await _dbContext.SaveChangesAsync();
                media.FullFileName = ContentUri + media.FullFileName;
                return media;
            }
            return null;
        }
    }
}
