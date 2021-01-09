using Learning.Shared.DbModels;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learning.Client.Services {
    public class CoverImageService {
        public string Format { get; set; } = "image/png";
        public async Task SetCoverImage(InputFileChangeEventArgs e, ICoverImageable coverImageable) {
            if (coverImageable == null) { return; }

            var maxAllowedFiles = 1;
            var format = "image/png";

            foreach (var imageFile in e.GetMultipleFiles(maxAllowedFiles)) {
                var resizedImageFile = await imageFile.RequestImageFileAsync(Format,
                    100, 100);
                var buffer = new byte[resizedImageFile.Size];
                await resizedImageFile.OpenReadStream().ReadAsync(buffer);
                var imageDataUrl = $"data:{format};base64,{Convert.ToBase64String(buffer)}";
                coverImageable.CoverImage = imageDataUrl;
            }
        }
    }
}
