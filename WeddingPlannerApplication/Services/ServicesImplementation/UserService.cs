using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeddingPlanner.Helpers;
using WeddingPlannerApplication.RepositoriesInterfaces;
using WeddingPlannerApplication.Services.ServicesInterfaces;
using WeddingPlannerDomain;
using WeddingPlannerDomain.Entities;

namespace WeddingPlannerApplication.Services.ServicesImplementation
{
    public class UserService : IUserService
    {
        private readonly IUserRepo _userRepo;
        public UserService(IUserRepo userRepo)
        {
            _userRepo = userRepo;
        }

        public async Task<ActionResponse<User>> AddAsync(User newUser)
        {
            if (newUser != null)
            {
                var hasher = new PasswhordHash();
                newUser.Password = hasher.HashPassword(newUser.Password);
                var res = await _userRepo.AddAsync(newUser);
                if (res != null)
                {
                    return new ActionResponse<User>(res);
                }
                return new ActionResponse<User>("User was mot created");
            }
            return new ActionResponse<User>("User was mot created");
        }

        public async Task<ActionResponse<User>> DeleteAsync(int id)
        {
            if (id != null)
            {
                var res = await _userRepo.DeleteAsync(id);
                if (res != null)
                {
                    return new ActionResponse<User>(res);
                }
                return new ActionResponse<User>("User was not deleted");
            }
            return new ActionResponse<User>("User was not deleted");
        }

        public async Task<ActionResponse<User>> GetByIdAsync(int id)
        {
            if (id != null)
            {
                var res = _userRepo.GetByIdAsync(id);
                if (res != null)
                {
                    return new ActionResponse<User>(res.Result);
                }
                return new ActionResponse<User>("User was not found");
            }
            return new ActionResponse<User>("User was not found");
        }

        public async Task<List<User>> ListAsync()
        {
            return await _userRepo.ListAsync();
        }

        public async Task<ActionResponse<User>> UpdateAsync(int id, User updateUser)
        {
            if (updateUser != null)
            {
                var res = await _userRepo.UpdateAsync(id, updateUser);
                if (res != null)
                {

                    return new ActionResponse<User>(res);
                }
                return new ActionResponse<User>("User was not updated");
            }
            return new ActionResponse<User>("User was not updated");
        }
        public async Task<ActionResponse<User>> FindByEmailAsync(string email)
        {
            var res = await _userRepo.FindByEmailAsync(email);
            if (res != null)
            {
                return new ActionResponse<User>(res);
            }
            return new ActionResponse<User>("User was not found");
        }
        public bool ValidatePasswordAsync(string request, string stored)
        {
            var hasher = new PasswhordHash();
            return hasher.VerifyPassword(request, stored);
        }
    }

}
