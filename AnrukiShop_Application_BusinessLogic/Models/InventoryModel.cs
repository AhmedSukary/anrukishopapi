namespace AnrukiShop_Application.Models
{
    public class InventoryModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public string Location { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}