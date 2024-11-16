using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeddingPlannerDomain;
using WeddingPlannerDomain.Entities;

namespace WeddingPlannerApplication.RepositoriesInterfaces
{
    public interface IUserRepo
    {
        Task<List<User>> ListAsync();
        Task<User> AddAsync(User newUser);
        Task<User> UpdateAsync(int id, User updatedUser);
        Task<User> DeleteAsync(int id);
        Task<User> GetByIdAsync(int id);
        Task<User> FindByEmailAsync(string email);
    }
}
