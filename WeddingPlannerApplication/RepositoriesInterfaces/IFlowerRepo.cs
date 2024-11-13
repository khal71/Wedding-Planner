using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeddingPlannerDomain;

namespace WeddingPlannerApplication.RepositoriesInterfaces
{
    public interface IFlowerRepo
    {
        Task<List<Flower>> ListAsync();
        Task<Flower> AddAsync(Flower newFlower);
        Task<Flower> UpdateAsync(int id, Flower updatedFlower);
        Task<Flower> DeleteAsync(int id);
        Task<Flower> GetByIdAsync(int id);
    }
}
