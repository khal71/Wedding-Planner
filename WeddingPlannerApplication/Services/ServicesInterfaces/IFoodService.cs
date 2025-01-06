using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeddingPlannerDomain;
using WeddingPlannerDomain.Entities;

namespace WeddingPlannerApplication.Services.ServicesInterfaces
{
    public interface IFoodService
    {
        Task<List<Food>> ListAsync();
        Task<ActionResponse<Food>> AddAsync(Food newFood);
        Task<ActionResponse<Food>> UpdateAsync(int id, Food updateFood);
        Task<ActionResponse<Food>> DeleteAsync(int id);
        Task<ActionResponse<Food>> GetByIdAsync(int id);
    }
}
