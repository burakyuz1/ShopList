using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Interfaces;

namespace Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IShoppingListViewModelService _shoppingService;
        private readonly IShoppingViewModelService _productService;

        public ProductController(IShoppingListViewModelService shoppingService, IShoppingViewModelService productService)
        {
            _shoppingService = shoppingService;
            _productService = productService;
        }

        public async Task<IActionResult> Index(int? listId,int? categoryId)
        {
            bool isAvailable = await _shoppingService.IsListAvialable(listId);
            if (!isAvailable) return RedirectToAction("Index", "ShoppingList");
            var model = await _productService.GetProductViewModelAsync(listId,categoryId);
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> AddToList(int productId, int listId)
        {
            var model = await _productService.AddToListAsync(productId,listId);
            return Json(model);
        }
    }
}
