using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Web.Areas.Admin.Interfaces;
using Web.Areas.Admin.Models;

namespace Web.Areas.Admin.Services
{
    public class ProductViewModelService : IProductViewModelService
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<Category> _categoryRepository;

        private readonly IWebHostEnvironment _env;

        public ProductViewModelService(IRepository<Product> productRepository, IWebHostEnvironment env, IRepository<Category> categoryRepository)
        {
            _productRepository = productRepository;
            _env = env;
            _categoryRepository = categoryRepository;
        }

        private async Task<List<SelectListItem>> GetListItemCategory()
        {
            var categoryList = await _categoryRepository.GetAllAsync();
            return categoryList.Select(x => new SelectListItem() { Text = x.Name, Value = x.Id.ToString() }).ToList();
        }
        public async Task<NewProductViewModel> GetProductListForAddAsync()
        {
            var categories = await GetListItemCategory();
            return new NewProductViewModel()
            {
                Categories = categories
            };
        }
        public async Task<List<Product>> GetProductsByCategoriesAsync()
            => await _productRepository.GetAllAsync(new ProductWithCategorySpecification());
        public async Task<bool> AddProductViewModelAsync(NewProductViewModel model)
        {
            if (await IsProductExists(model.Name)) return false;
            Product product = new()
            {
                PictureUri = UploadImage(model.Image),
                CategoryId = model.CategoryId,
                Name = model.Name,

            };
            await _productRepository.AddAsync(product);
            return true;
        }
        public async Task<UpdateProductViewModel> UpdateProductViewModelAsync(UpdateProductViewModel model)
        {
            var product = await _productRepository.GetByIdAsync(model.Id);
            if (model.Image != null)
            {
                DeleteImage(model.ImagePath);
                product.PictureUri = UploadImage(model.Image);
            }
            await _productRepository.UpdateAsync(product);
            return await ProductToProductVmAsync(product);
        }
        public async Task<UpdateProductViewModel> GetProductByIdAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            return await ProductToProductVmAsync(product);
        }
        public async Task DeleteProductAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            await _productRepository.DeleteAsync(product);
        }
        private async Task<UpdateProductViewModel> ProductToProductVmAsync(Product product) => new()
        {
            Categories = await GetListItemCategory(),
            Name = product.Name,
            ImagePath = product.PictureUri,
            CategoryId = product.CategoryId,
            Id = product.Id
        };
        private string UploadImage(IFormFile file)
        {
            string fileName = null;
            if (file != null)
            {
                fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                string destinationPath = Path.Combine(_env.WebRootPath, "img", fileName);
                using (var fs = new FileStream(destinationPath, FileMode.Create))
                {
                    file.CopyTo(fs);
                }
            }
            return fileName;
        }
        private void DeleteImage(string fileName)
        {
            if (fileName == null) return;
            string deletePath = Path.Combine(_env.WebRootPath, "img", fileName);
            try
            {
                System.IO.File.Delete(deletePath);
            }
            catch (Exception)
            {
            }
        }
        private async Task<bool> IsProductExists(string name)
        {
            var product = await _productRepository.FirstOrDefaultAsync(new ProductSpecification(name));
            if (product != null)
            {
                return true;
            }
            return false;
        }

    }
}
