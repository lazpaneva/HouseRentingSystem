using HouseRentingSystem.Contract.House;
using HouseRentingSystem.Data;
using HouseRentingSystem.Services.Model;

namespace HouseRentingSystem.Services.House
{
    public class HouseService //: IHouseService_interface
    {
        private readonly HouseRentingDbContext data;
        public HouseService(HouseRentingDbContext _data)
        {
                data = _data;
        }
        //public Task<IEnumerable<HouseIndexServiceModel>> LastThreeHouses()
        //{
        //    return data.
        //}
    }
}
