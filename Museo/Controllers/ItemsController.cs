using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Museo.Models;
using Museo.Services;
using Museo.ViewModels;


namespace Museo.Controllers
{
    public class ItemsController : Controller
    {

        private readonly IItemsService _ItemsService;


        public ItemsController(IItemsService ItemsService)
        {
            _ItemsService = ItemsService;
        }


        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Item()
        {
            var item = await _ItemsService.getItem(User);
            if (item == null) return NotFound();

            return View(item);
        }

        [Authorize]
        [HttpGet]
        public IActionResult CreateItem()
        {
            var Model = _ItemsService.GetCreatItem();
            if (Model == null) return NotFound();
            return View(Model);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateItem(CreatItemVeiwModel Model)
        {
            if (!ModelState.IsValid) return View(Model);
            var u = await _ItemsService.ceratItem(User, Model);

            if (u == null) return NotFound();
            return RedirectToAction("Index", "Home");

        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> EditItem(int id)
        {
            var Model = await _ItemsService.GetEditItem(User, id);
            if (Model == null) return NotFound();
            return View(Model);

        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> MoveItem(
     [FromBody] MoveItemViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var success = await _ItemsService.MoveItem(User, model);

            if (!success)
                return BadRequest();

            return Ok();
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Details(int id) {
        
            var details = await  _ItemsService.GetItemDetails(id);

            if (details == null) return NotFound();

            return View(details);


        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Like(int id)
        {
            var like = await _ItemsService.ToggleLike(id , User);

            if (!like) return NotFound();

            return RedirectToAction("Details", new { id = id });
        }
    }
    }
