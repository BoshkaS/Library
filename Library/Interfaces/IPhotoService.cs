using CloudinaryDotNet.Actions;

namespace Library.Interfaces
{
    public interface IPhotoService
    {
        Task<ImageUploadResult> AddPhotoAsync(IFormFile file, string photoType);

        Task<DeletionResult> DeletePhotoAsync(string publicId);
    }
}
