using AutoMapper;
using Azure;
using Backend.Entities;
using Backend.MVC.Extensions;
using Backend.MVC.Models;
using Backend.MVC.Services;
using Backend.WebAPI.DataAccess.UnitOfWork;
using Backend.WebAPI.Dto;
using Backend.WebAPI.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Backend.MVC.Controllers
{
    public class ThingsController : Controller
    {
        private readonly IThingService thingService;
        private readonly ICategoryService categoryService;
        private readonly ILogger<ThingsController> logger;

        public ThingsController(IThingService thingService, ICategoryService categoryService, ILogger<ThingsController> logger)
        {
            this.logger = logger;
            this.thingService = thingService;
            this.categoryService = categoryService;
        }

        async public Task<IActionResult> Index()
        {
            var dbThings = await thingService.GetAllAsync();
            var viewmodels = dbThings.ToViewModels();
            return View(viewmodels);
        }

        [HttpGet]
        async public Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var thing = await thingService.GetByIdAsync(id.Value);
            if (thing == null)
            {
                return NotFound();
            }

            return View(thing.ToViewModel());
        }

        async public Task<IActionResult> Create()
        {
            var categories = await categoryService.GetAllAsync();
            ViewBag.Categories = categories;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        async public Task<IActionResult> Create(CreateThingViewModel thingViewModel)
        {
            var categories = await categoryService.GetAllAsync();
            ViewBag.Categories = categories;

            if (!ModelState.IsValid)
            {
                return View("Create", thingViewModel);
            }
                

            var list = await thingService.GetAllAsync();
            if (list.Any(t => t.Description == thingViewModel.Description))
            {
                ModelState.AddModelError(String.Empty, "A thing with that description already exists");
                return View(thingViewModel);
            }

            var category = await categoryService.GetByIdAsync(thingViewModel.Category);

            var thing = new Thing
            {
                Description = thingViewModel.Description,
                Category = category!
            };
            await thingService.AddAsync(thing);
            await thingService.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        async public Task<IActionResult> Edit(int? id)
        {
            var categories = await categoryService.GetAllAsync();
            ViewBag.Categories = categories;

            if (id == null)
            {
                return NotFound();
            }

            var thing = await thingService.GetByIdAsync(id.Value);

            if (thing == null)
            {
                return NotFound();
            }

            return View(thing.ToViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        async public Task<IActionResult> Edit(int id, EditThingViewModel thingViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("Edit", thingViewModel);
            }

            var thing = await thingService.GetByIdAsync(id);
            var category = await categoryService.GetByIdAsync(thingViewModel.Category);
            var newThing = new Thing
            {
                ID = id,
                Description = thingViewModel.Description,
                //No se porque thingViewModel.CreationDate me devuele 1/1/0001 00:00:00
                CreationDate = thing.CreationDate,
                Category = category!
            };

            thingService.Update(thing);
            await thingService.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var thing = await thingService.GetByIdAsync(id.Value);
            if (thing == null)
            {
                return NotFound();
            }

            return View(thing.ToViewModel());
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var thing = await thingService.GetByIdAsync(id);
            if (thing is null)
            {
                return NotFound();
            }

            thingService.Delete(thing);
            await thingService.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
