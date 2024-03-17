using HouseRentingSystem.Contract.Agent;
using HouseRentingSystem.Contract.House;
using HouseRentingSystem.Infrastructure;
using HouseRentingSystem.Models.Houses;
using HouseRentingSystem.Services.House;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HouseRentingSystem.Controllers
{

    [Authorize]
    public class HouseController : Controller
    {

        private readonly IHouseService _houses;
        private readonly IAgentService _agents;
        public HouseController(IHouseService houses, IAgentService agent)
        {
                _houses = houses;
                _agents = agent;
        }
        

        [AllowAnonymous]
        public async Task<IActionResult> Add()
        {
            if (await _agents.ExistsById(User.Id()) == false)
            {
                return RedirectToAction(nameof(AgentController.Become), "Agent");
            };
            return View(new HouseFormModel
            {
                Categories = await _houses.AllCategories()
            });

        }
        [HttpPost]
        public async Task<IActionResult> Add(HouseFormModel model)
        {
            if (await _agents.ExistsById(User.Id()) == false)
            {
                return RedirectToAction(nameof(AgentController.Become), "Agent");
            };
            if (await _houses.CategoryExists(model.CategoryId) == false)
            {
                ModelState.AddModelError(nameof(model.CategoryId), "Категорията не съществува");
            }
            if (!ModelState.IsValid)
            {
                model.Categories = await _houses.AllCategories();
                return View(model);
            }
            var agentId = await _agents.GetAgentId(User.Id());
            var newHouseId = await _houses.Create(model.Title, model.Address, model.Description,
                    model.ImageUrl, model.PricePerMonth, model.CategoryId, agentId);

            return RedirectToAction(nameof(Details), new {Id = newHouseId});

        }
        public async Task<IActionResult> Mine()
        {
            return View(new AllHousesQueryModel());
        }
        public async Task<IActionResult> Details(int id)
        {
            return View(new HouseDetailsViewModel());
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> All(HouseFormModel model)
        {
            return RedirectToAction(nameof(Details), new {id = "1" });
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            return View(new HouseFormModel());
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, HouseFormModel house)
        {
            return RedirectToAction(nameof(Details), new { id = "1" });
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            return View(new HouseFormModel());
        }

        [HttpPost]
        public async Task<IActionResult> Dekete(HouseDetailsViewModel house)
        {
            return RedirectToAction(nameof(All));
        }

        [HttpPost]
        public async Task<IActionResult> Rent(int id)
        {
            return RedirectToAction(nameof(Mine));
        }

        [HttpPost]
        public async Task<IActionResult> Leave(int id)
        {
            return RedirectToAction(nameof(Mine));
        }

    }
}
