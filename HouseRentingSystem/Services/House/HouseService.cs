using HouseRentingSystem.Contract.House;
using HouseRentingSystem.Data;
using HouseRentingSystem.Services.Model;
using System.Collections.Generic;
using System.Linq;

using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace HouseRentingSystem.Services.House
{
    public class HouseService : IHouseService
    {
        private readonly HouseRentingDbContext _data;
        public HouseService(HouseRentingDbContext data)
        {
                _data = data;
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
