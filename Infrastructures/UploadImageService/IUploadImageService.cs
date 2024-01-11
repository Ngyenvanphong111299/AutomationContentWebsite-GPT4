using Microsoft.AspNetCore.Http;

namespace Infrastructures.UploadImageService;

public interface IUploadImageService
{
    private const int imageHeight = 460;
    private const int imageWidth = 806;
    Task<string> Upload(byte[] bytes, string fileName, int height = imageHeight, int width = imageWidth);
    Task<string> Upload(IFormFile file, string fileName, int height = imageHeight, int width = imageWidth);
    Task<string> Upload(string originUrl, string fileName, int height = imageHeight, int width = imageWidth);
    Task Remove(string imagePath);
}
