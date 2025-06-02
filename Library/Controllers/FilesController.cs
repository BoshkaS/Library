using Amazon.S3;
using Amazon.S3.Model;
using Library.DTO;
using Library.DTO.bucketDTO;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : Controller
    {
        private readonly IAmazonS3 _s3Client;

        public FilesController(IAmazonS3 s3Client)
        {
            _s3Client = s3Client;   
        }

        [HttpPost]
        public async Task<IActionResult> UploadFileAsync(IFormFile file, string bucketName, string? prefix)
        {
            var bucketExists = await Amazon.S3.Util.AmazonS3Util.DoesS3BucketExistV2Async(_s3Client, bucketName);
            if (!bucketExists) return NotFound($"Bucket {bucketName} does not exists.");

            var request = new PutObjectRequest()
            {
                BucketName = bucketName,
                Key = string.IsNullOrEmpty(prefix) ? file.FileName : $"{prefix?.TrimEnd('/')}/{file.FileName}",
                InputStream = file.OpenReadStream()
            };
            request.Metadata.Add("Content-Type", file.ContentType);
            await _s3Client.PutObjectAsync(request);
            return Ok($"File {prefix}/{file.FileName} aploaded to S3 successdully!");

        }

        [HttpGet]
        public async Task<IActionResult> GetAllFilesAsync(BucketNameDTO bucketNameDTO)
        {
            var bucketExists = await Amazon.S3.Util.AmazonS3Util.DoesS3BucketExistV2Async(_s3Client, bucketNameDTO.BucketName);
            if (!bucketExists) return NotFound($"Bucket {bucketNameDTO.BucketName} does not exists.");

            var request = new ListObjectsV2Request()
            {
                BucketName = bucketNameDTO.BucketName,
                Prefix = bucketNameDTO.Prefix
            };
            var result = await _s3Client.ListObjectsV2Async(request);
            var s3Objects = result.S3Objects.Select(s =>
            {
                var urlRequest = new GetPreSignedUrlRequest()
                {
                    BucketName = bucketNameDTO.BucketName,
                    Key = s.Key,
                    Expires = DateTime.UtcNow.AddMinutes(1),
                };
                return new S3ObjectDTO()
                {
                    Name = s.Key.ToString(),
                    PresignedUrl = _s3Client.GetPreSignedURL(urlRequest)
                };
            });
            return Ok(s3Objects);
        }

        [HttpGet("preview")]
        public async Task<IActionResult> GetFileByKeyAsync(string bucketName, string key)
        {
            var bucketExists = await Amazon.S3.Util.AmazonS3Util.DoesS3BucketExistV2Async(_s3Client, bucketName);
            if (!bucketExists) return NotFound($"Bucket {bucketName} does not exists.");

            var s3Object = await _s3Client.GetObjectAsync(bucketName, key);
            return File(s3Object.ResponseStream, s3Object.Headers.ContentType);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteFileAsync(string bucketName, string key)
        {
            var bucketExists = await Amazon.S3.Util.AmazonS3Util.DoesS3BucketExistV2Async(_s3Client, bucketName);
            if (!bucketExists) return NotFound($"Bucket {bucketName} does not exists.");

            await _s3Client.DeleteObjectAsync(bucketName, key);
            return NoContent();
        }
    }
}
