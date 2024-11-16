using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeddingPlannerApplication.RepositoriesInterfaces;
using WeddingPlannerDomain.Entities;
using WeddingPlannerInfrastructure.DB;

namespace WeddingPlannerInfrastructure.ReposImplementation
{
    public class AdminRepo:IAdminRepo
    {

        private readonly AppDbContext _dbContext;

        public AdminRepo(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Admin> AddAsync(Admin newAdmin)
        {
            var admin = (await _dbContext.Admins.AddAsync(newAdmin)).Entity;
            await _dbContext.SaveChangesAsync();
            return admin;
        }

        public async Task<Admin> DeleteAsync(int id)
        {
            var admin = await GetByIdAsync(id);
            var res = (_dbContext.Admins.Remove(admin)).Entity;
            await _dbContext.SaveChangesAsync();
            return res;
        }

        public async Task<Admin> FindByEmailAsync(string email)
        {
            var admin = await _dbContext.Admins.FirstOrDefaultAsync(u => u.Email == email);
            if (admin != null)
            {
                return admin;
            }
            return null;
        }

        public async Task<Admin> GetByIdAsync(int id)
        {
            return await _dbContext.Admins.FindAsync(id);
        }

        public async Task<List<Admin>> ListAsync()
        {
            return await _dbContext.Admins.ToListAsync();
        }

        public async Task<Admin> UpdateAsync(int id, Admin updatedAdmin)
        {
            var admin = GetByIdAsync(id).Result;
            if (admin != null)
            {
                admin.Email = updatedAdmin.Email;
                admin.Password = updatedAdmin.Password;
                await _dbContext.SaveChangesAsync();
            }
            return admin;
        }
    }
}
