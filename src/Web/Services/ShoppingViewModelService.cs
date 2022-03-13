using ApplicationCore.Entities;
using ApplicationCore.Enums;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
        private readonly IRepository<ShoppingList> _shoppingListRepository;
        private readonly IHttpContextAccessor _httpContext;
        private readonly IListService _listService;

        public ShoppingViewModelService(IRepository<Product> productRepository, IRepository<Category> categoryRepository, IListService listService, IRepository<ShoppingListItem> itemsRepository, IRepository<ShoppingList> shoppingListRepository, IHttpContextAccessor httpContext)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _listService = listService;
            _itemsRepository = itemsRepository;
            _shoppingListRepository = shoppingListRepository;
            _httpContext = httpContext;
        }

        public async Task<ListAsideViewModel> AddToListAsync(int productId, int listId, string description)
        {
            var item = await _listService.AddItemToShoppingListAsync(productId, listId, description);
            return new()
            {
                ItemId = item.Id,
                ItemName = item.Product.Name,
                Description = item.Description
            };
        }
        public async Task DeleteFromListAsync(int productId, int listId)
              => await _listService.RemoveItemFromShoppingListAsync(productId, listId);
        public async Task<ShoppingViewModel> GetProductViewModelAsync(int? listId, int? categoryId, UserPreference pageSelect)
        {
            bool isAddingProduct = true;
            bool isBusyToShopping = false;
            var categoryList = await _categoryRepository.GetAllAsync();
            var productList = await _productRepository.GetAllAsync(new ProductSpecification(categoryId));
            var items = await _itemsRepository.GetAllAsync(new ShoppingListItemsSpecification(listId));
            var shoppingList = await _shoppingListRepository.FirstOrDefaultAsync(new ShoppingListFilterSpecification(listId));
            string loggedUserId = _httpContext.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (pageSelect == UserPreference.StartShopping)
            {
                isAddingProduct = false;
                if (shoppingList.ShopperId == null || loggedUserId == shoppingList.ShopperId)
                {
                    shoppingList.ShopperId = loggedUserId;
                    await _shoppingListRepository.UpdateAsync(shoppingList);
                    isBusyToShopping = false;
                }
                else
                {
                    isBusyToShopping = true;
                }
            }
            else if(pageSelect == UserPreference.AddProduct && shoppingList.ShopperId != null)
            {
                isAddingProduct = false;
                isBusyToShopping = true;
            }
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
                Items = items,
                IsBusyToShopping = isBusyToShopping,
                IsAddingProduct = isAddingProduct,
                ListName = shoppingList.Name
            };
        }
        public async Task MakeShoppingListFreeAsync(int listId)
        {
            var list = await _shoppingListRepository.GetByIdAsync(listId);
            list.ShopperId = null;
            await _shoppingListRepository.UpdateAsync(list);
        }
    }
}
