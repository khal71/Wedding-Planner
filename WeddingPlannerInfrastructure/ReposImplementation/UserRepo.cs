using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeddingPlannerApplication.RepositoriesInterfaces;
using WeddingPlannerDomain;
using WeddingPlannerDomain.Entities;
using WeddingPlannerInfrastructure.DB;

namespace WeddingPlannerInfrastructure.ReposImplementation
{
    public class UserRepo : IUserRepo
    {

        private readonly AppDbContext _dbContext;

        public UserRepo(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> AddAsync(User newUser)
        {
            var user = (await _dbContext.Users.AddAsync(newUser)).Entity;
            await _dbContext.SaveChangesAsync();
            return user;
        }

        public async Task<User> DeleteAsync(int id)
        {
            var user = await GetByIdAsync(id);
            var res = (_dbContext.Users.Remove(user)).Entity;
            await _dbContext.SaveChangesAsync();
            return res;
        }

        public async Task<User> FindByEmailAsync(string email)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user != null)
            {
                return user;
            }
            return null;
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _dbContext.Users.FindAsync(id);
        }

        public async Task<List<User>> ListAsync()
        {
            return await _dbContext.Users.ToListAsync();
        }

        public async Task<User> UpdateAsync(int id, User updatedUser)
        {
            var user = GetByIdAsync(id).Result;
            if (user != null)
            {
                user.Email = updatedUser.Email;
                user.Password = updatedUser.Password;
                await _dbContext.SaveChangesAsync();
            }
            return user;
        }
    }
}
