namespace AnrukiShop_Application.Models
{
    public class CartModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<CartItemModel> Items { get; set; } = new List<CartItemModel>();
    }
}