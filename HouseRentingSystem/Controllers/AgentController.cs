using HouseRentingSystem.Contract.Agent;
using HouseRentingSystem.Infrastructure;
using HouseRentingSystem.Models.Agents;
using static HouseRentingSystem.Infrastructure.ClaimsPrincipalExtensions;
using Microsoft.AspNetCore.Mvc;

namespace HouseRentingSystem.Controllers
{
    public class AgentController : Controller
    {
        private readonly IAgentService _agents;
        public AgentController(IAgentService agents)
        {
                _agents = agents;
        }

        [HttpGet]
        public IActionResult Become()
        {
            var model = new BecomeAgentFormModel();

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Become(BecomeAgentFormModel model)
        {
            var isExist = _agents.ExistsById(User.Id()); //мислих го 5 часа, не знам защо не ставаше, да започня конструкцията
            if (await isExist)
            {
                return BadRequest();
            }
           return View();
        }
    }
}
