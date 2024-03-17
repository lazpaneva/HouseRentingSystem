using HouseRentingSystem.Contract.House;
using HouseRentingSystem.Data;
using System.Collections.Generic;
using System.Linq;

using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using HouseRentingSystem.Services.House.Models;
using HouseRentingSystem.Models.Houses;
using HouseRentingSystem.Infrastructure;

namespace HouseRentingSystem.Services.House
{
    public class HouseService : IHouseService
    {
        private readonly HouseRentingDbContext _data;
        public HouseService(HouseRentingDbContext data)
        {
                _data = data;
        }

        public HouseQueryServiceModel All(string category = null, string searchTerm = null, HouseSorting sorting = HouseSorting.Newest, int currentPage = 1, int housesPerPage = 1)
        {
            var housesQuery = _data.Houses.AsQueryable();
            if (!string.IsNullOrWhiteSpace(category))
            {
                housesQuery = _data.Houses
                    .Where(c => c.Category.Name == category);
            }
            if (!String.IsNullOrWhiteSpace(searchTerm))
            {
                housesQuery = _data.Houses
                    .Where(h=>h.Title.ToLower().Contains(searchTerm.ToLower()) ||
                    h.Address.ToLower().Contains(searchTerm.ToLower()) ||
                    h.Description.ToLower().Contains(searchTerm.ToLower()));
            }

            housesQuery = sorting switch {
                HouseSorting.Price => housesQuery
                .OrderBy(h=>h.PricePerMonth),
                HouseSorting.NotRentedFirst => housesQuery
                .OrderByDescending(h=>h.RenterId != null)
                .ThenByDescending(h=>h.Id),
                _ => housesQuery.OrderByDescending(h=>h.Id) 
                };

            var houses = housesQuery
                .Skip((currentPage-1) * housesPerPage)
                .Take(housesPerPage)
                .Select(h => new HouseServiceModel
                {
                    Id = h.Id,
                    Title = h.Title,
                    Address = h.Address,
                    ImageUrl = h.ImageUrl,
                    IsRented = h.RenterId != null,
                    PricePerMonth = h.PricePerMonth
                })
                .ToList();

            var totalHouses = housesQuery.Count();
            return new HouseQueryServiceModel()
            {
                Houses = houses,
                TotalHouseCount = totalHouses
            };
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

        public async Task<IEnumerable<string>> AllCategoriesName()
        {
            return await _data.Categories
                .Select(c=>c.Name)
                .Distinct()
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
