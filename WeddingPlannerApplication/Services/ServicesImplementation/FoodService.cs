using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeddingPlannerApplication.RepositoriesInterfaces;
using WeddingPlannerApplication.Services.ServicesInterfaces;
using WeddingPlannerDomain.Entities;

namespace WeddingPlannerApplication.Services.ServicesImplementation
{
    public class FoodService : IFoodService
    {
        private readonly IFoodRepo _foodRepo;
        public FoodService(IFoodRepo foodRepo)
        {
            _foodRepo = foodRepo;
        }
        public async Task<ActionResponse<Food>> AddAsync(Food newFood)
        {
            if (newFood != null)
            {
                var res = await _foodRepo.AddAsync(newFood);
                if (res != null)
                {
                    return new ActionResponse<Food>(res);
                }
                return new ActionResponse<Food>("Food was not created");
            }
            return new ActionResponse<Food>("food was not created ");

        }
        public async Task<ActionResponse<Food>> DeleteAsync(int id)
        {
            if (id != null)
            {
                var res = await _foodRepo.DeleteAsync(id);
                if (res != null)
                {
                    return new ActionResponse<Food>(res);
                }
                return new ActionResponse<Food>("food was not deleted");
            }
            return new ActionResponse<Food>("food was not deleted");
        }


        public async Task<ActionResponse<Food>> GetByIdAsync(int id)
        {
            if (id != null)
            {
                var res = _foodRepo.GetByIdAsync(id);
                if (res != null)
                {
                    return new ActionResponse<Food>(res.Result);
                }
                return new ActionResponse<Food>("Food was not found");

            }
            return new ActionResponse<Food>("food was not found"); 
        }
        public async Task<List<Food>> ListAsync()
        { 
            return await _foodRepo.ListAsync();
        }


        public async Task<ActionResponse<Food>> UpdateAsync(int id, Food updateFood)
        {
            if (updateFood != null)
            {
                var res = await _foodRepo.UpdateAsync(id, updateFood);
                if (res != null)
                {
                    return new ActionResponse<Food>(res); 

                }
                return new ActionResponse<Food>("food was not updated");

            }
            return new ActionResponse<Food>("food was not updated"); 
        }
    }
}

