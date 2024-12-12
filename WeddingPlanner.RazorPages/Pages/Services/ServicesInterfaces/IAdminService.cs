using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeddingPlannerDomain.Entities;

namespace WeddingPlannerApplication.Services.ServicesInterfaces
{
    public interface IAdminService
    {
        Task<List<Admin>> ListAsync();
        Task<ActionResponse<Admin>> AddAsync(Admin newAdmin);
        Task<ActionResponse<Admin>> UpdateAsync(int id, Admin updateAdmin);
        Task<ActionResponse<Admin>> DeleteAsync(int id);
        Task<ActionResponse<Admin>> GetByIdAsync(int id);
        Task<ActionResponse<Admin>> FindByEmailAsync(string email);
        bool ValidatePasswordAsync(string request, string stored);
    }
}
