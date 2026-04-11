namespace AnrukiShop_API.Requests
{
    public class CreateCategoryRequest
    {
        public required string Name { get; set; }
        public int? ParentCategoryId { get; set; }
    }

    public class UpdateCategoryRequest
    {
        public required string Name { get; set; }
        public int? ParentCategoryId { get; set; }
    }
}
