using HouseRentingSystem.Services.House.Models;
using HouseRentingSystem.Models.Houses;
using HouseRentingSystem.Infrastructure;

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

        HouseQueryServiceModel All(string category = null,
            string searchTerm = null,
            HouseSorting sorting = HouseSorting.Newest,
            int currentPage = 1,
            int housesPerPage = 1
            );

        Task<IEnumerable<string>> AllCategoriesName();
    }
}
