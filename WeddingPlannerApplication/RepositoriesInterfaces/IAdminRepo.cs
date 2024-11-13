using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeddingPlannerDomain.Entities;

namespace WeddingPlannerApplication.RepositoriesInterfaces
{
    public interface IAdminRepo
    {
        Task<List<Admin>> ListAsync();
        Task<Admin> AddAsync(Admin newAdmin);
        Task<Admin> UpdateAsync(int id, Admin updatedAdmin);
        Task<Admin> DeleteAsync(int id);
        Task<Admin> GetByIdAsync(int id);
    }
}
