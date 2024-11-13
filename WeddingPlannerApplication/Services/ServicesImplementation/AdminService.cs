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
    public class AdminService : IAdminService
    {
        private readonly IAdminRepo _adminRepo;
        public AdminService(IAdminRepo adminRepo)
        {
            _adminRepo = adminRepo;
        }

        public async Task<ActionResponse<Admin>> AddAsync(Admin newAdmin)
        {
            if (newAdmin != null)
            {
                var res = await _adminRepo.AddAsync(newAdmin);
                if (res != null)
                {
                    return new ActionResponse<Admin>(res);
                }
                return new ActionResponse<Admin>("Admin was mot created");
            }
            return new ActionResponse<Admin>("Admin was mot created");
        }

        public async Task<ActionResponse<Admin>> DeleteAsync(int id)
        {
            if (id != null)
            {
                var res = await _adminRepo.DeleteAsync(id);
                if (res != null)
                {
                    return new ActionResponse<Admin>(res);
                }
                return new ActionResponse<Admin>("Admin was not deleted");
            }
            return new ActionResponse<Admin>("Admin was not deleted");
        }

        public async Task<ActionResponse<Admin>> GetByIdAsync(int id)
        {
            if (id != null)
            {
                var res = _adminRepo.GetByIdAsync(id);
                if (res != null)
                {
                    return new ActionResponse<Admin>(res.Result);
                }
                return new ActionResponse<Admin>("Admin was not found");
            }
            return new ActionResponse<Admin>("Admin was not found");
        }

        public async Task<List<Admin>> ListAsync()
        {
            return await _adminRepo.ListAsync();
        }

        public async Task<ActionResponse<Admin>> UpdateAsync(int id, Admin updateAdmin)
        {
            if (updateAdmin != null)
            {
                var res = await _adminRepo.UpdateAsync(id, updateAdmin);
                if (res != null)
                {

                    return new ActionResponse<Admin>(res);
                }
                return new ActionResponse<Admin>("Admin was not updated");
            }
            return new ActionResponse<Admin>("Admin was not updated");
        }
    }
}
