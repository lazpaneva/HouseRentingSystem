namespace HouseRentingSystem.Contract.Agent
{
    public interface IAgentService
    {
        Task<bool> ExistsById(string userId);
    }
}
