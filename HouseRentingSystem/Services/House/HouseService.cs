using HouseRentingSystem.Contract.House;
using HouseRentingSystem.Data;
using HouseRentingSystem.Services.Model;
using System.Collections.Generic;
using System.Linq;

using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using HouseRentingSystem.Services.House.Models;

namespace HouseRentingSystem.Services.House
{
    public class HouseService : IHouseService
    {
        private readonly HouseRentingDbContext _data;
        public HouseService(HouseRentingDbContext data)
        {
                _data = data;
        }

        public async Task<IEnumerable<HouseCategoryServiceModel>> AllCategories()
        {
            return await _data.Categories
                .Select(c => new HouseCategoryServiceModel()
                {
                    Id = c.Id,
                    Name = c.Name,
                })
                .ToListAsync();
        }

        public async Task<bool> CategoryExists(int categoryId)
        {
            return await _data.Categories.AnyAsync(c => c.Id == categoryId);
        }

        public async Task<int> Create(string title, string address, string description, string imageUrl, 
            decimal price, int categoriId, int agentId)
        {
            var house = new HouseRentingSystem.Data.Models.House()
            {
                Title = title,
                Address = address,
                Description = description,
                ImageUrl = imageUrl,
                PricePerMonth = price,
                CategoryId = categoriId,
                AgentId = agentId
            };

            await _data.Houses.AddAsync(house);
            await _data.SaveChangesAsync();

            return house.Id; //!!!!!ID на новата къща се връща
        }

        public Task<List<HouseIndexServiceModel>> LastThreeHouses()
        {

            var houses = _data
                .Houses
                .OrderByDescending(c => c.Id)
                .Select(c => new HouseIndexServiceModel()
                {
                    Id = c.Id,
                    Title = c.Title,
                    ImageUrl = c.ImageUrl,
                })
                .Take(3)
                .ToListAsync();

            return houses;
        }
    }
}
