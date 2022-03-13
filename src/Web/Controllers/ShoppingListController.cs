using ApplicationCore.Enums;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Interfaces;

namespace Web.Controllers
{
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
    }
}
