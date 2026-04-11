namespace AnrukiShop_Application.Models
{
    public class ProductModel
    {
        public int Id { get;  set; }
        public string Name { get;  set; }
        public string Description { get;  set; }
        public decimal Price { get; set; }
        public string SKU { get; set; }
        public int CategoryId { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsDeleted { get; set; }
    }
}
