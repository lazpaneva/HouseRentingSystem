using HouseRentingSystem.Models.Agents;
using Microsoft.AspNetCore.Mvc;

namespace HouseRentingSystem.Controllers
{
    public class AgentController : Controller
    {
        public async Task<IActionResult> Become(BecomeAgentFormModel agent)
        {
            return RedirectToAction(nameof(HouseController.All), "House");
        }
    }
}
