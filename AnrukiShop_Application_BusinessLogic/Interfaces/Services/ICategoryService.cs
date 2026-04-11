using AnrukiShop_Application.Models;

namespace AnrukiShop_Application.Interfaces.Services
{
    public interface ICategoryService
    {
        public CategoryModel GetById(int id);
        public List<CategoryModel> GetCategoryTree();
        public string GetCategoryPathById(int categoryId);
        public int Create(CategoryModel model);
        public bool UpdateBasicInfo(int id, string name, int? parentCategoryId);
        public bool Activate(int id);
        public bool Deactivate(int id);
        public bool Delete(int id);
    }
}