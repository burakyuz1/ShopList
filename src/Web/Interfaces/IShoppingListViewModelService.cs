using ApplicationCore.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models;

namespace Web.Interfaces
{
    public interface IShoppingListViewModelService
    {
        Task<List<ShoppingListViewModel>> GetShoppingListViewModelAsync();
        Task<AddMemberViewModel> GetMemberViewModelAsync(int listId);
        Task<AddMemberViewModel> AddMemberAsync(int listId, string memberId);
        Task RemoveList(int listId);
        Task<ShoppingListViewModel> AddNewList(string listName);
    }
}
