using System.Diagnostics;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using ImageMagick;
using Microsoft.AspNetCore.Http;
namespace Infrastructures.UploadImageService;

public class S3UploadImageService : IUploadImageService
{
    private const string ServiceURL = "https://s3.cloudfly.vn";
    private const string BucketName = "mobile-storage";

    private static AmazonS3Client _awsS3Client = new AmazonS3Client(
        awsAccessKeyId: "48QPH17HIRGDK13ZA8U1",
        awsSecretAccessKey: "49reVAz3l5yVZ3RBZXfvAdL95PuWXKMS0QLubWHv",
        clientConfig: new AmazonS3Config()
        {
            AuthenticationServiceName = "s3",
            ServiceURL = ServiceURL,
            ForcePathStyle = true
        });

    public async Task Remove(string imagePath)
    {
        var deleteRequest = new DeleteObjectRequest
        {
            BucketName = BucketName,
            Key = imagePath
        };

        try
        {
            // Execute the delete request
            var response = await _awsS3Client.DeleteObjectAsync(deleteRequest);

            // Check the response for success
            if (response.HttpStatusCode == System.Net.HttpStatusCode.NoContent)
            {
                Debug.WriteLine("Object deleted successfully.");
            }
            else
            {
                Debug.WriteLine($"Error deleting object. Status code: {response.HttpStatusCode}");
            }
        }
        catch (AmazonS3Exception ex)
        {
            Debug.WriteLine($"Amazon S3 error: {ex.Message}");
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error: {ex.Message}");
        }
    }
    public async Task<string> Upload(MemoryStream memoryStream, string fileName)
    {
        var imageUrl = $"{ServiceURL}/{BucketName}/{fileName}";
        await Remove(imageUrl);
        var request = new TransferUtilityUploadRequest
        {
            InputStream = memoryStream,
            Key = fileName == "" ? Guid.NewGuid().ToString() : fileName,
            BucketName = BucketName,
            ContentType = "image/webp",
            CannedACL = S3CannedACL.PublicRead,
            AutoCloseStream = false,
            PartSize = 50 * 1024 * 1024
        };
        var transferUtility = new TransferUtility(_awsS3Client);
        try
        {
            await transferUtility.UploadAsync(request);
        }
        catch (Exception ex)
        {
            throw ex;
        }

        return imageUrl;
    }
    public async Task<string> Upload(byte[] bytes, string fileName, int height, int width)
    {
        var result = string.Empty;
        using (MemoryStream memoryStream = new MemoryStream(bytes))
        {
            memoryStream.Seek(0, SeekOrigin.Begin);
            using (var image = new MagickImage(memoryStream))
            {
                var fName = fileName + ".webp";
                using (MemoryStream mStream = new MemoryStream())
                {
                    image.Resize(width, height);
                    image.Format = MagickFormat.WebP;
                    image.Write(mStream);
                }
            }
            result = await Upload(memoryStream, fileName);
            return result;
        }
    }
    public async Task<string> Upload(IFormFile file, string fileName, int height, int width)
    {
        var result = string.Empty;
        var memoryStream = new MemoryStream();
        using (var stream = file.OpenReadStream())
        {
            await stream.CopyToAsync(memoryStream);
            memoryStream.Seek(0, SeekOrigin.Begin);
            using (var image = new MagickImage(memoryStream))
            {
                using (MemoryStream mStream = new MemoryStream())
                {
                    image.Resize(width, height);
                    image.Format = MagickFormat.WebP;
                    image.Write(mStream);
                    result = await Upload(mStream, fileName + ".webp");
                }
            }
            return result;
        }
    }
    public async Task<string> Upload(string originUrl, string fileName, int height, int width)
    {
        var result = string.Empty;
        using (HttpClient httpClient = new HttpClient())
        {
            HttpResponseMessage response = await httpClient.GetAsync(originUrl);
            var stream = await response.Content.ReadAsStreamAsync();
            using (MemoryStream memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);
                memoryStream.Seek(0, SeekOrigin.Begin);
                using (var image = new MagickImage(memoryStream))
                {
                    var fName = fileName + ".webp";
                    using (MemoryStream mStream = new MemoryStream())
                    {
                        image.Resize(width, height);
                        image.Format = MagickFormat.WebP;
                        image.Write(mStream);
                    }
                }
                result = await Upload(memoryStream, fileName);
            }
        }
        return result;
    }
}
