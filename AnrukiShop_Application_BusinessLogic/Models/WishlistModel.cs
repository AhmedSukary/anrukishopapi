namespace AnrukiShop_Application.Models
{
    public class WishlistModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public List<WishlistItemModel> Items { get; set; } = new List<WishlistItemModel>();
    }
}