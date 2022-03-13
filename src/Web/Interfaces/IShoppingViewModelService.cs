using ApplicationCore.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models;

namespace Web.Interfaces
{
    public interface IShoppingViewModelService
    {
        Task<ShoppingViewModel> GetProductViewModelAsync(int? listId, int? categoryId, UserPreference pageSelect);
        Task<ListAsideViewModel> AddToListAsync(int productId, int listId,string description);
        Task DeleteFromListAsync(int productId, int listId);
        Task MakeShoppingListFreeAsync(int listId);
    }
}
