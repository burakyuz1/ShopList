using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ListService : IListService
    {
        private readonly IRepository<ShoppingList> _shoppingListRepo;
        private readonly IRepository<ShoppingListItem> _shoppingListItemRepo;
        private readonly IRepository<Product> _productRepository;

        public ListService(IRepository<ShoppingList> shoppingListRepo, IRepository<ShoppingListItem> shoppingListItemRepo, IRepository<Product> productRepository)
        {
            _shoppingListRepo = shoppingListRepo;
            _shoppingListItemRepo = shoppingListItemRepo;
            _productRepository = productRepository;
        }
        public async Task<ShoppingListItem> AddItemToShoppingListAsync(int? productId, int? listId)
        {
            if (!productId.HasValue)
                throw new ArgumentNullException("Product can not be empty");

            var list = await _shoppingListRepo.GetByIdAsync(listId.Value);
            if (list == null)
                throw new ArgumentNullException("List can not be found!");

            var product = await _productRepository.GetByIdAsync(productId.Value);
            if (product == null)
                throw new ArgumentNullException("Product can not be found!");

            ShoppingListItem item = new() { ShoppingListId = list.Id, ProductId = product.Id };
            list.ShoppingListItems.Add(item);
            await _shoppingListRepo.UpdateAsync(list);
            return item;
        }
    }
}
