using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeddingPlannerDomain;
using WeddingPlannerDomain.Entities;

namespace WeddingPlannerApplication.RepositoriesInterfaces
{
    public interface ILocationRepo
    {
        Task<List<Location>> ListAsync();
        Task<Location> AddAsync(Location newLocation);
        Task<Location> UpdateAsync(int id, Location updatedLocation);
        Task<Location> DeleteAsync(int id);
        Task<Location> GetByIdAsync(int id);
    }
}
