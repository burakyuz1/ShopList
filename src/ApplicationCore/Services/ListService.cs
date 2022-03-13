using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications;
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
        private readonly IRepository<ShoppingListMember> _membersRepository;

        public ListService(IRepository<ShoppingList> shoppingListRepo, IRepository<ShoppingListItem> shoppingListItemRepo, IRepository<Product> productRepository, IRepository<ShoppingListMember> membersRepository)
        {
            _shoppingListRepo = shoppingListRepo;
            _shoppingListItemRepo = shoppingListItemRepo;
            _productRepository = productRepository;
            _membersRepository = membersRepository;
        }
        public async Task<ShoppingListItem> AddItemToShoppingListAsync(int? productId, int? listId, string description)
        {
            if (!productId.HasValue)
                throw new ArgumentNullException("Product can not be empty");

            var list = await _shoppingListRepo.GetByIdAsync(listId.Value);
            if (list == null)
                throw new ArgumentNullException("List can not be found!");

            var product = await _productRepository.GetByIdAsync(productId.Value);
            if (product == null)
                throw new ArgumentNullException("Product can not be found!");

            ShoppingListItem item = new() { ShoppingListId = list.Id, ProductId = product.Id, Description = description };
            list.ShoppingListItems.Add(item);
            await _shoppingListRepo.UpdateAsync(list);
            return item;
        }

        public async Task<ShoppingListMember> AddMemberToShopingListAsync(string memberId, int listId)
        {
            return  await _membersRepository.AddAsync(new ShoppingListMember()
            {
                ShoppingListId = listId,
                UserId = memberId
            });
        }

        public async Task RemoveItemFromShoppingListAsync(int listItemId, int listId)
        {
            var shoppingItems = await _shoppingListItemRepo.GetAllAsync(new ShoppingListItemsSpecification(listId));
            var deleteItem = shoppingItems.FirstOrDefault(x => x.Id == listItemId);
            await _shoppingListItemRepo.DeleteAsync(deleteItem);
        }
    }
}
