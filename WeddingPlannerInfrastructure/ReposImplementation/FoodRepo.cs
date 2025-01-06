using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeddingPlannerApplication.RepositoriesInterfaces;
using WeddingPlannerDomain.Entities;
using WeddingPlannerInfrastructure.DB;

namespace WeddingPlannerInfrastructure.ReposImplementation
{
    public class FoodRepo : IFoodRepo
    {
        private readonly AppDbContext _dbContext;

        public FoodRepo(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Food> AddAsync(Food newFood)
        {
            var food = (await _dbContext.Food.AddAsync(newFood)).Entity;
            await _dbContext.SaveChangesAsync();
            return food;
        }

        public async Task<Food> DeleteAsync(int id)
        {
            var food = await GetByIdAsync(id);
            var res = (_dbContext.Food.Remove(food)).Entity;
            await _dbContext.SaveChangesAsync();
            return food;
        }

        public async Task<Food> GetByIdAsync(int id)
        {
            return await _dbContext.Food.FindAsync(id); 
        }

        public async Task<List<Food>> ListAsync()
        {
            return await _dbContext.Food.ToListAsync();
        }

        public async Task<Food> UpdateAsync(int id, Food updatedFood)
        {
           var food = GetByIdAsync(id).Result;
            if (food != null)
            {
                food.Vegan = updatedFood.Vegan;
                food.Vegetarian = updatedFood.Vegetarian;
                food.ImageData = updatedFood.ImageData;
                food.Name = updatedFood.Name;
                food.Type = updatedFood.Type;
                await _dbContext.SaveChangesAsync();
                
            }
            return food; 
        }
    }
}
