using WeddingPlannerDomain.Entities;

namespace WeddingPlannerApplication.RepositoriesInterfaces
{
    public interface IFoodRepo
    {
        Task<List<Food>> ListAsync();
        Task<Food> AddAsync(Food newFood);
        Task<Food> UpdateAsync(int id, Food updatedFood);
        Task<Food> DeleteAsync(int id);
        Task<Food> GetByIdAsync(int id);
    }
}
