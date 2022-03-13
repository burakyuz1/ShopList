using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IListService
    {
        Task<ShoppingListItem> AddItemToShoppingListAsync(int? productId, int? listId,string description);
        Task RemoveItemFromShoppingListAsync(int listItemId, int listId);
        Task<ShoppingListMember> AddMemberToShopingListAsync(string memberId, int listId);
    }
}
