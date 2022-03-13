using ApplicationCore.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Interfaces;

namespace Web.Controllers
{
    [Authorize]
    public class ShoppingListController : Controller
    {
        private readonly IShoppingListViewModelService _shoppingService;

        public ShoppingListController(IShoppingListViewModelService shoppingService)
        {
            _shoppingService = shoppingService;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _shoppingService.GetShoppingListViewModelAsync();
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> AddMemberToList(int listId)
        {
            var model = await _shoppingService.GetMemberViewModelAsync(listId);
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> AddMemberToList(int listId, string memberId)
        {
            var model = await _shoppingService.AddMemberAsync(listId, memberId);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveList(int listId)
        {
            await _shoppingService.RemoveList(listId);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> AddNewList(string listName)
        {
            var model = await _shoppingService.AddNewList(listName);
            return PartialView("_NewListPartial",model);
        }
    }
}
