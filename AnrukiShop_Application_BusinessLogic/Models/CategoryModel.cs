namespace AnrukiShop_Application.Models
{
    public class CategoryModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentCategoryId { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public List<CategoryModel> Children { get; set; } = new List<CategoryModel>();
    }
}