using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Areas.Admin.Interfaces;
using Web.Areas.Admin.Models;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        private readonly IProductViewModelService _productService;
        public ProductController(IProductViewModelService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _productService.GetProductsByCategoriesAsync());
        }

        public async Task<IActionResult> Create()
        {
            return View(await _productService.GetProductListForAddAsync());
        }
        [HttpPost]
        public async Task<IActionResult> Create(NewProductViewModel product)
        {
            bool isSuccess = await _productService.AddProductViewModelAsync(product);
            if (isSuccess)
            {
                return RedirectToAction("Index");

            }
            TempData["InfoMessage"] = "This Product already Exist";
            return RedirectToAction("Create");

        }

        public async Task<IActionResult> Update(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            return View(product);
        }
        [HttpPost]
        public async Task<IActionResult> Update(UpdateProductViewModel product)
        {
            var model = await _productService.UpdateProductViewModelAsync(product);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _productService.DeleteProductAsync(id);
            return RedirectToAction("Index");
        }
    }
}
