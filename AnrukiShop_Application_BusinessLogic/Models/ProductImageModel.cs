namespace AnrukiShop_Application.Models
{
    public class ProductImageModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Url { get; set; }
        public bool IsPrimary { get; set; }
    }
}
