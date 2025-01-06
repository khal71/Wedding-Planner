using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeddingPlannerApplication.RepositoriesInterfaces;
using WeddingPlannerApplication.Services.ServicesInterfaces;
using WeddingPlannerDomain.Entities;

namespace WeddingPlannerApplication.Services.ServicesImplementation
{
    public class LocationService : ILocationService
    {
        private readonly ILocationRepo _locationRepo;
        public LocationService(ILocationRepo locationRepo)
        {
            _locationRepo = locationRepo;
        }
        public async Task<ActionResponse<Location>> AddAsync(Location newLocation)
        {
            if(newLocation != null)
            {
                var res= await _locationRepo.AddAsync(newLocation);
                if (res != null)
                {
                    return new ActionResponse<Location>(res);
                }
                return new ActionResponse<Location>("location was not created");
            }
            return new ActionResponse<Location>("location was not created"); 
        }
        public async Task<ActionResponse<Location>> DeleteAsync(int id)
        {
            if (id != null)
            {
                var res = await _locationRepo.DeleteAsync(id);
                if (res != null)
                {
                    return new ActionResponse<Location>(res);
                }
                return new ActionResponse<Location>("location was not deleted"); 
            }
            return new ActionResponse<Location>("location was not created"); 
        }

        public async Task<ActionResponse<Location>> GetByIdAsync(int id)
        {
            if (id != null)
            {
                var res = _locationRepo.GetByIdAsync(id);
                if (res != null)
                {
                    return new ActionResponse<Location>(res.Result);
                }
                return new ActionResponse<Location>("location was not found");
            }
            return new ActionResponse<Location>("location was not found"); 
        }

        public async Task<List<Location>> ListAsync()
        {
            return await _locationRepo.ListAsync();
        }

        public async Task<ActionResponse<Location>> UpdateAsync(int id, Location updateLocation)
        {
            if(updateLocation != null)
            {
                var res = await _locationRepo.UpdateAsync(id, updateLocation);
                if (res != null)
                {
                    return new ActionResponse<Location>(res);
                }

                return new ActionResponse<Location>("location was not updated"); 
            }
            return new ActionResponse<Location>("location was not updated"); 
        }
    }
}
    
