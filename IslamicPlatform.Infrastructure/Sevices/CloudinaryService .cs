using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using IslamicPlatform.Application.DTOs.CloudinaryUploadResultDTOs;
using IslamicPlatform.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IslamicPlatform.Infrastructure.Sevices
{
   
    public class CloudinaryService : ICloudinaryService
    {
        private readonly Cloudinary _cloudinary;

        public CloudinaryService(IConfiguration config)
        {
            var account = new Account(
                config["Cloudinary:CloudName"],
                config["Cloudinary:ApiKey"],
                config["Cloudinary:ApiSecret"]
            );

            _cloudinary = new Cloudinary(account);
            _cloudinary.Api.Secure = true; // عشان الرابط يبدأ بـ https
        }

        public async Task<CloudinaryUploadResult> UploadImageAsync(IFormFile file, string folder)
        {
            // تحقق إن الملف مش فاضي
            if (file == null || file.Length == 0)
                throw new ArgumentException("File is empty");

            await using var stream = file.OpenReadStream();

            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(file.FileName, stream),
                Folder = folder,

                // بنعمل Transformation تلقائي للصورة
                Transformation = new Transformation()
                    .Width(400)          // العرض 400 بكسل
                    .Height(400)         // الطول 400 بكسل
                    .Crop("fill")        // بيقطع الزيادة
                    .Gravity("face")     // بيركز على الوجه
                    .Quality("auto")     // بيضغط الحجم تلقائي
                    .FetchFormat("auto") // بيختار أحسن Format تلقائي
            };

            var result = await _cloudinary.UploadAsync(uploadParams);

            // لو فيه Error
            if (result.Error != null)
                throw new Exception(result.Error.Message);

            return new CloudinaryUploadResult
            {
                Url = result.SecureUrl.ToString(),
                PublicId = result.PublicId
            };
        }

        public async Task<bool> DeleteImageAsync(string publicId)
        {
            var deleteParams = new DeletionParams(publicId);
            var result = await _cloudinary.DestroyAsync(deleteParams);
            return result.Result == "ok";
        }
    }
}
