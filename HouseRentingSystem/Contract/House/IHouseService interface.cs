using HouseRentingSystem.Services.Model;

namespace HouseRentingSystem.Contract.House
{
    public interface IHouseService_interface
    {
        Task<IEnumerable<HouseIndexServiceModel>> LastThreeHouses();
    }
}
