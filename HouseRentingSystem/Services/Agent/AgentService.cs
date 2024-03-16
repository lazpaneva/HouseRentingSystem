using HouseRentingSystem.Contract.Agent;
using HouseRentingSystem.Data;
using Microsoft.EntityFrameworkCore;

namespace HouseRentingSystem.Services.Agent
{
    public class AgentService : IAgentService

    {
        private readonly HouseRentingDbContext _data;
        public AgentService(HouseRentingDbContext data)
        {
            _data = data;
        }
        public async Task<bool> ExistsById(string userId)
        {
            return await _data.Age.AnyAsync(a=> a.UserId == userId);
        }
    }
}
