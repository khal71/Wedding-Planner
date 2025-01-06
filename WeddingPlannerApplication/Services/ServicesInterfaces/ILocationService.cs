using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeddingPlannerDomain.Entities;

namespace WeddingPlannerApplication.Services.ServicesInterfaces
{
    public interface ILocationService
    {
        Task<List<Location>> ListAsync();
        Task<ActionResponse<Location>> AddAsync(Location newLocation);
        Task<ActionResponse<Location>> UpdateAsync(int id, Location updateLocation);
        Task<ActionResponse<Location>> DeleteAsync(int id);
        Task<ActionResponse<Location>> GetByIdAsync(int id);
    }
}
