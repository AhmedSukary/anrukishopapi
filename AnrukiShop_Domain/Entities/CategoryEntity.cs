using AnrukiShop_Domain.Exceptions;

namespace AnrukiShop_Domain.Entities
{
    public class CategoryEntity
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public int? ParentCategoryId { get; private set; }
        public bool IsActive { get; private set; }
        public bool IsDeleted { get; private set; }

        public CategoryEntity(string name, int? parentCategoryId)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("CATEGORY_NAME_REQUIRED", "Category name is required");

            if (name.Length < 3)
                throw new DomainException("CATEGORY_NAME_TOO_SHORT", "Category name must be at least 3 characters");          

            if (parentCategoryId < 0)
                throw new DomainException("PARENT_CATEGORY_INVALID", "Parent category id is invalid");

            Name = name.Trim();
            ParentCategoryId = parentCategoryId;

            IsActive = true;
            IsDeleted = false;
        }

        public void ChangeName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("CATEGORY_NAME_REQUIRED", "Category name is required");

            if (name.Length < 3)
                throw new DomainException("CATEGORY_NAME_TOO_SHORT", "Category name must be at least 3 characters");

            Name = name.Trim();
        }

        public void ChangeParent(int? parentCategoryId)
        {
            if (parentCategoryId < 0)
                throw new DomainException("PARENT_CATEGORY_INVALID", "Parent category id is invalid");

            ParentCategoryId = parentCategoryId;
        }

        public void Activate() => IsActive = true;

        public void Deactivate() => IsActive = false;

        public void SoftDelete()
        {
            IsDeleted = true;
            IsActive = false;
        }

        internal CategoryEntity(
            int id,
            string name,
            int? parentCategoryId,
            bool isActive,
            bool isDeleted)
        {
            Id = id;
            Name = name;
            ParentCategoryId = parentCategoryId;
            IsActive = isActive;
            IsDeleted = isDeleted;
        }
    }
}
