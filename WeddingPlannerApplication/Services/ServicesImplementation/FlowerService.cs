using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeddingPlannerApplication.RepositoriesInterfaces;
using WeddingPlannerApplication.Services.ServicesInterfaces;
using WeddingPlannerDomain;

namespace WeddingPlannerApplication.Services.ServicesImplementation
{
    public class FlowerService : IFlowerService
    {
        private readonly IFlowerRepo _flowerRepo;
        public FlowerService(IFlowerRepo flowerRepo)
        {
            _flowerRepo = flowerRepo;
        }

        public async Task<ActionResponse<Flower>> AddAsync(Flower newFlower)
        {
            if (newFlower != null)
            {
                var res= await _flowerRepo.AddAsync(newFlower);
                if (res != null)
                {
                    return new ActionResponse<Flower>(res);
                }
                return new ActionResponse<Flower>("Flower was mot created");
            }
            return new ActionResponse<Flower>("Flower was mot created");
        }

        public async Task<ActionResponse<Flower>> DeleteAsync(int id)
        {
            if (id != null)
            {
                var res = await _flowerRepo.DeleteAsync(id);
                if (res != null)
                {
                    return new ActionResponse<Flower>(res);
                }
                return new ActionResponse<Flower>("Flower was not deleted");
            }
            return new ActionResponse<Flower>("Flower was not deleted");
        }

        public async Task<ActionResponse<Flower>> GetByIdAsync(int id)
        {
            if (id != null)
            {
                var res = _flowerRepo.GetByIdAsync(id);
                if (res != null)
                {
                    return new ActionResponse<Flower>(res.Result);
                }
                return new ActionResponse<Flower>("Flower was not found");
            }
            return new ActionResponse<Flower>("Flower was not found");
        }

        public async Task<List<Flower>> ListAsync()
        {
            return await _flowerRepo.ListAsync();
        }

        public async Task<ActionResponse<Flower>> UpdateAsync(int id, Flower updateFlower)
        {
            if (updateFlower != null)
            {
                var res = await _flowerRepo.UpdateAsync(id, updateFlower);
                if (res != null)
                {

                    return new ActionResponse<Flower>(res);
                }
                return new ActionResponse<Flower>("Flower was not updated");
            }
            return new ActionResponse<Flower>("Flower was not updated");
        }
    }
    
}
