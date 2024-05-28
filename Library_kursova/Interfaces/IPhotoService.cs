using CloudinaryDotNet.Actions;

namespace Library_kursova.Interfaces
{
    public interface IPhotoService
    {
        Task<ImageUploadResult> AddPhotoAsync(IFormFile file, string photoType);

        Task<DeletionResult> DeletePhotoAsync(string publicId);
    }
}
