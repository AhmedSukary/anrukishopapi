using AnrukiShop_Domain.Entities;

namespace AnrukiShop_Application.Interfaces.Repositories
{
    public interface ICategoryRepository
    {
        public CategoryEntity? GetById(int id);
        public List<CategoryEntity> GetAll();
        public int Create(CategoryEntity entity);
        public bool Update(CategoryEntity entity);
        public bool Delete(int id);
    }
}
