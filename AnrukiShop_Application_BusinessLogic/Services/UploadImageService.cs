using AnrukiShop_Application.Interfaces.Services;
using AnrukiShop_Application.Exceptions;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace AnrukiShop_Application.Services
{
    public class UploadImageService : IUploadImageService
    {
        private readonly IConfiguration _config;

        public UploadImageService(IConfiguration config)
        {
            _config = config;
        }

        public async Task<string> UploadImage(string base64Image)
        {
            using var client = new HttpClient();
            var content = new MultipartFormDataContent();
            content.Add(new StringContent(base64Image), "image");

            string apiKey = _config["ImgBB:ApiKey"];

            var response = await client.PostAsync(
                $"https://api.imgbb.com/1/upload?key={apiKey}",
                content);

            if (!response.IsSuccessStatusCode)
                throw new AppException("UPLOAD_FAILED", "Upload failed");

            var json = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<ImgBBResponse>(json);

            if (result?.data?.url == null)
                throw new AppException("UPLOAD_FAILED", "Invalid response from image server");

            return result.data.url;
        }

        public class ImgBBResponse
        {
            public ImgBBData data { get; set; }
        }

        public class ImgBBData
        {
            public string url { get; set; }
        }
    }
}
