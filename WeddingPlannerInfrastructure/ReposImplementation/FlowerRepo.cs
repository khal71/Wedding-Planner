using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeddingPlannerApplication.RepositoriesInterfaces;
using WeddingPlannerDomain;
using WeddingPlannerInfrastructure.DB;

namespace WeddingPlannerInfrastructure.ReposImplementation
{
    public class FlowerRepo : IFlowerRepo
    {
        private readonly AppDbContext _dbContext;

        public FlowerRepo(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Flower> AddAsync(Flower newFlower)
        {
            var flower = (await _dbContext.Flower.AddAsync(newFlower)).Entity;
            await _dbContext.SaveChangesAsync();
            return flower;
        }

        public async Task<Flower> DeleteAsync(int id)
        {
            var flower = await GetByIdAsync(id);
            var res = (_dbContext.Flower.Remove(flower)).Entity;
            await _dbContext.SaveChangesAsync();
            return res;
        }

        public async Task<Flower> GetByIdAsync(int id)
        {
            return await _dbContext.Flower.FindAsync(id);
        }

        public async Task<List<Flower>> ListAsync()
        {
            return await _dbContext.Flower.ToListAsync();
        }

        public async Task<Flower> UpdateAsync(int id, Flower updatedFlower)
        {
            var flower = GetByIdAsync(id).Result;
            if (flower != null)
            {
                flower.Name = updatedFlower.Name;
                flower.Type = updatedFlower.Type;
                flower.ImageData = updatedFlower.ImageData;
                flower.Color = updatedFlower.Color;
                await _dbContext.SaveChangesAsync();
            }
            return flower;
        }
    }
}
