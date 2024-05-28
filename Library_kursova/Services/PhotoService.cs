using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Library_kursova.Helpers;
using Library_kursova.Interfaces;
using Microsoft.Extensions.Options;

namespace Library_kursova.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly Cloudinary _cloudinary;

        public PhotoService(IOptions<CloudinarySettings> config)
        {
            var acc = new Account
                (
                    config.Value.CloudName,
                    config.Value.ApiKey,
                    config.Value.ApiSecret
                );

            _cloudinary = new Cloudinary(acc);
        }

        public async Task<ImageUploadResult> AddPhotoAsync(IFormFile file, string photoType)
        {
            var uploadResult = new ImageUploadResult();

            if(file.Length > 0)
            {
                using var stream = file.OpenReadStream();
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, stream),
                    Transformation = GetTransformationForPhotoType(photoType),
                    Folder = GetFolderForPhotoType(photoType)
                };

                uploadResult = await _cloudinary.UploadAsync(uploadParams);
            }
            return uploadResult;
        }

        private Transformation GetTransformationForPhotoType(string photoType)
        {
            switch (photoType.ToLower())
            {
                case "user":
                    return new Transformation().Crop("fill");
                case "book":
                    return new Transformation().Crop("fill");
                default:
                    throw new ArgumentException("Invalid photo type");
            }
        }

        private string GetFolderForPhotoType(string photoType)
        {
            switch (photoType.ToLower())
            {
                case "user":
                    return "library-users";
                case "book":
                    return "library-books";
                default:
                    throw new ArgumentException("Invalid photo type");
            }
        }

        public async Task<DeletionResult> DeletePhotoAsync(string publicId)
        {
            var deleteParams = new DeletionParams(publicId);

            return await _cloudinary.DestroyAsync(deleteParams);
        }
    }
}
