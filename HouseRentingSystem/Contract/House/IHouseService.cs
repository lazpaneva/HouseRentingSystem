using HouseRentingSystem.Services.Model;

namespace HouseRentingSystem.Contract.House
{
    public interface IHouseService
    {
        Task<List<HouseIndexServiceModel>> LastThreeHouses();
    }
}
