using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeddingPlannerDomain;
using WeddingPlannerDomain.Entities;

namespace WeddingPlannerApplication.Services.ServicesInterfaces
{
    public interface IUserService
    {
  
            Task<List<User>> ListAsync();
            Task<ActionResponse<User>> AddAsync(User newUser);
            Task<ActionResponse<User>> UpdateAsync(int id, User updateUser);
            Task<ActionResponse<User>> DeleteAsync(int id);
            Task<ActionResponse<User>> GetByIdAsync(int id);
            Task<ActionResponse<User>> FindByEmailAsync(string email);
        bool ValidatePasswordAsync(string request, string stored);

    }
}
