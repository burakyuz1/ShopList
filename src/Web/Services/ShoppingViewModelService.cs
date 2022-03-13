using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Interfaces;
using Web.Models;

namespace Web.Services
{
    public class ShoppingViewModelService : IShoppingViewModelService
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<Category> _categoryRepository;
        private readonly IRepository<ShoppingListItem> _itemsRepository;
        private readonly IListService _listService;

        public ShoppingViewModelService(IRepository<Product> productRepository, IRepository<Category> categoryRepository, IListService listService, IRepository<ShoppingListItem> itemsRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _listService = listService;
            _itemsRepository = itemsRepository;
        }

        public async Task<ListAsideViewModel> AddToListAsync(int productId, int listId)
        {
            var item = await _listService.AddItemToShoppingListAsync(productId, listId);
            return new()
            {
                ItemName = item.Product.Name,
                Description = item.Description
            };
        }

        public async Task<ShoppingViewModel> GetProductViewModelAsync(int? listId, int? categoryId)
        {
            var categoryList = await _categoryRepository.GetAllAsync();
            var productList = await _productRepository.GetAllAsync(new ProductSpecification(categoryId));
            var items = await _itemsRepository.GetAllAsync(new ShoppingListItemsSpecification(listId));
            return new()
            {
                Categories = categoryList.Select(x => new SelectListItem()
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }).ToList(),
                Products = productList.Select(x => new ProductViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    PictureUri = x.PictureUri
                }).ToList(),
                ShoppingListId = listId,
                Items = items
            };
        }
    }
}
