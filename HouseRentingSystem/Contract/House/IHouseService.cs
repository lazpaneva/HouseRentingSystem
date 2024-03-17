using HouseRentingSystem.Services.House.Models;
using HouseRentingSystem.Services.Model;

namespace HouseRentingSystem.Contract.House
{
    public interface IHouseService
    {
        Task<List<HouseIndexServiceModel>> LastThreeHouses();
        Task<IEnumerable<HouseCategoryServiceModel>> AllCategories();

        Task<bool> CategoryExists(int catetegoryId);
        Task<int> Create(string title, string address, 
            string description, string imageUrl, decimal price,
            int categoriId, int agentId);

    }
}
