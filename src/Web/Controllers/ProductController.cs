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
    public class ProductController : Controller
    {
        private readonly IShoppingViewModelService _productService;

        public ProductController(IShoppingViewModelService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> Index(int? listId, int? categoryId, UserPreference pageSelect)
        {
            var model = await _productService.GetProductViewModelAsync(listId, categoryId, pageSelect);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddToList(int productId, int listId, string description)
        {
            var model = await _productService.AddToListAsync(productId, listId, description);
            return Json(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteFromList(int productId, int listId)
        {
            await _productService.DeleteFromListAsync(productId, listId);
            return NoContent();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FinishShopping(int listId)
        {
            await _productService.MakeShoppingListFreeAsync(listId);
            return RedirectToAction("Index", "ShoppingList");
        }
    }
}
