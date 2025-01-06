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
    public class LocationRepo : ILocationRepo
    {
        private readonly AppDbContext _dbContext;

        public LocationRepo(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Location> AddAsync(Location newLocation)
        {
            var flower = (await _dbContext.Locations.AddAsync(newLocation)).Entity;
            await _dbContext.SaveChangesAsync();
            return newLocation;
        }

        public async Task<Location> DeleteAsync(int id )
        {
            var locatiom= await GetByIdAsync(id);
            var res = (_dbContext.Locations.Remove(locatiom)).Entity;
            await _dbContext.SaveChangesAsync();
            return res; 
        }

        public async Task<Location> GetByIdAsync(int id)
        {
            return await _dbContext.Locations.FindAsync(id);
        }
        public async Task<List<Location>> ListAsync()
        {
            return await _dbContext.Locations.ToListAsync();
        }
        public async Task<Location> UpdateAsync(int id, Location newLocation)
        {
               var location = GetByIdAsync(id).Result;
            if (location != null)
            {
                location.Region = newLocation.Region;
                location.City = newLocation.City;
                location.Country = newLocation.Country;
                location.StreetName = newLocation.StreetName;
                location.PostalCode = newLocation.PostalCode;
                location.ImageData = newLocation.ImageData;
                location.Name = newLocation.Name;
                location.Type = newLocation.Type;
                await _dbContext.SaveChangesAsync();

            }
            return location; 
        }
    }
}
