namespace AnrukiShop_Application.Interfaces.Services
{
   public interface IUploadImageService
    {
        public Task<string> UploadImage(string base64Image);
    }
}
