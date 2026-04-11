using AnrukiShop_Application.Interfaces.Repositories;
using AnrukiShop_Application.Interfaces.Services;
using AnrukiShop_Application.Models;
using AnrukiShop_Application.Mappings;
using AnrukiShop_Application.Exceptions;
using AnrukiShop_Domain.Entities;
using AnrukiShop_Domain.Exceptions;

namespace AnrukiShop_Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repo;

        private static List<CategoryModel> BuildCategoryTree(List<CategoryModel> categories)
        {
            Dictionary<int, CategoryModel> categoriesLookup = new Dictionary<int, CategoryModel>();

            foreach (var category in categories)
                categoriesLookup.Add(category.Id, category);

            var root = new List<CategoryModel>();

            foreach (var category in categories)
            {
                if (category.ParentCategoryId == null)
                    root.Add(category);
                else if (categoriesLookup.ContainsKey(category.ParentCategoryId.Value))
                    categoriesLookup[category.ParentCategoryId.Value].Children.Add(category);
            }

            return root;
        }

        public string? BuildCategoryPath(int categoryId, List<CategoryModel> categories)
        {
            Dictionary<int, CategoryModel> categoriesLookup = new Dictionary<int, CategoryModel>();

            foreach (var category in categories)
                categoriesLookup.Add(category.Id, category);

            var path = new List<string>();

            while (categoriesLookup.TryGetValue(categoryId, out var category))
            {
                path.Insert(0, category.Name);

                if (category.ParentCategoryId == null)
                    break;

                categoryId = category.ParentCategoryId.Value;
            }

            if (path.Count == 0)
                return null;

            return string.Join(" > ", path);
        }

        public CategoryService(ICategoryRepository repo)
        {
            _repo = repo;
        }

        public CategoryModel GetById(int id)
        {
            try
            {
                var entity = _repo.GetById(id)
                    ?? throw new AppException("CATEGORY_NOT_FOUND", "Category not found");

                return entity.ToModel();
            }
            catch (DomainException ex)
            {
                throw new AppException(ex.Code, ex.Message);
            }
        }

        public List<CategoryModel> GetCategoryTree()
        {
            return BuildCategoryTree(_repo.GetAll().ToModelList());
        }

        public string GetCategoryPathById(int categoryId)
        {
            return BuildCategoryPath(categoryId, _repo.GetAll().ToModelList())
                ?? throw new AppException("CATEGORY_NOT_FOUND", "Category not found");
        }

        public int Create(CategoryModel model)
        {
            try
            {
                var entity = new CategoryEntity(
                    model.Name,
                    model.ParentCategoryId
                );

                return _repo.Create(entity);
            }
            catch (DomainException ex)
            {
                throw new AppException(ex.Code, ex.Message);
            }
        }

        public bool UpdateBasicInfo(int id, string name, int? parentCategoryId)
        {
            try
            {
                var entity = _repo.GetById(id)
                    ?? throw new AppException("CATEGORY_NOT_FOUND", "Category not found");

                entity.ChangeName(name);
                entity.ChangeParent(parentCategoryId);

                return _repo.Update(entity);
            }
            catch (DomainException ex)
            {
                throw new AppException(ex.Code, ex.Message);
            }
        }

        public bool Activate(int id)
        {
            try
            {
                var entity = _repo.GetById(id)
                    ?? throw new AppException("CATEGORY_NOT_FOUND", "Category not found");

                entity.Activate();
                return _repo.Update(entity);
            }
            catch (DomainException ex)
            {
                throw new AppException(ex.Code, ex.Message);
            }
        }
        public bool Deactivate(int id)
        {
            try
            {
                var entity = _repo.GetById(id)
                    ?? throw new AppException("CATEGORY_NOT_FOUND", "Category not found");

                entity.Deactivate();
                return _repo.Update(entity);
            }
            catch (DomainException ex)
            {
                throw new AppException(ex.Code, ex.Message);
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var entity = _repo.GetById(id)
                    ?? throw new DomainException("CATEGORY_NOT_FOUND", "Category not found");

                entity.SoftDelete();
                return _repo.Update(entity);
            }
            catch (DomainException ex)
            {
                throw new AppException(ex.Code, ex.Message);
            }
        }
    }
}
