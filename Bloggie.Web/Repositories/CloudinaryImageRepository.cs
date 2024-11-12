using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using dotenv.net;

namespace Bloggie.Web.Repositories
{
    public class CloudinaryImageRepository : IImageRepository
    {
        private readonly IConfiguration _configuration;
        private readonly Account _account;

        public CloudinaryImageRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            DotEnv.Load(options: new DotEnvOptions(probeForEnv: true));
            var cloudinaryUrl = Environment.GetEnvironmentVariable("CLOUDINARY_URL");
            if (cloudinaryUrl != null)
            {
                var uri = new Uri(cloudinaryUrl);
                var userInfo = uri.UserInfo.Split(':');

                var cloudName = uri.Host;         // CloudName
                var apiKey = userInfo[0];          // ApiKey
                var apiSecret = userInfo[1];       // ApiSecret
                
                _account = new Account(cloudName, apiKey, apiSecret);
            }
            else
            {
                throw new Exception("CLOUDINARY_URL not found in environment variables");
            }
        }

        public async Task<string> UploadAsync(IFormFile file)
        {
            var client = new Cloudinary(_account);

            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(file.FileName, file.OpenReadStream()),
                DisplayName = file.FileName
            };
            var uploadResult = await client.UploadAsync(uploadParams);

            if (uploadResult != null && uploadResult.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return uploadResult.SecureUrl.ToString();
            }

            return null;
        }
    }
}
