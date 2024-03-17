using HouseRentingSystem.Services.House.Models;

namespace HouseRentingSystem.Models.Houses
{
    public class HouseQueryServiceModel
    {
        public int TotalHouseCount { get; set; }
        public IEnumerable<HouseServiceModel> Houses { get; set; } = new List<HouseServiceModel>();
    }
}
