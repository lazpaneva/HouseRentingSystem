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
            var userId = User.Id();
            var isExist = _agents.ExistsById(User.Id()); //мислих го 5 часа, не знам защо не ставаше, да започня конструкцията
            if (await isExist)
            {
                return BadRequest();
            }
            if (await _agents.UserWithPhoneNumberExists(model.PhoneNumber))
            {
                ModelState.AddModelError(nameof(model.PhoneNumber), "Phone number already exist. Enter another one.");
            }
            if (await _agents.UserHasRents(userId))
            {
                ModelState.AddModelError("Error", "You should have no rents to become an agent!");

            }
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            return RedirectToAction(nameof(HouseController.All), "House");
        }
    }
}
