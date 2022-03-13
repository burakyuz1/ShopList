using ApplicationCore.Entities;
using ApplicationCore.Enums;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Web.Interfaces;
using Web.Models;

namespace Web.Services
{
    public class ShoppingListViewModelService : IShoppingListViewModelService
    {
        private readonly IRepository<ShoppingList> _shoppingListRepository;
        private readonly IRepository<ShoppingListMember> _shoppingListMemberRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContext;

        public ShoppingListViewModelService(IRepository<ShoppingList> shoppingListRepository, IHttpContextAccessor httpContext, IRepository<ShoppingListMember> shoppingListMemberRepository, UserManager<ApplicationUser> userManager)
        {
            _shoppingListRepository = shoppingListRepository;
            _httpContext = httpContext;
            _shoppingListMemberRepository = shoppingListMemberRepository;
            _userManager = userManager;
        }

        public async Task<List<ShoppingListViewModel>> GetShoppingListViewModelAsync()
        {
            string userId = _httpContext.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        // var user = (await _userManager.GetUserAsync(_httpContext.HttpContext.User));

            var shoppingListWithMembers = await _shoppingListMemberRepository.GetAllAsync(new ShoppingListMemberSpecification(userId));
            List<int?> ids = shoppingListWithMembers.Select(x => x.ShoppingListId).ToList();


            var shoppingList = await _shoppingListRepository.GetAllAsync(new ShoppingListFilterSpecification(userId, ids));


            var result = new List<ShoppingListViewModel>();
            foreach (var item in shoppingList)
            {
                result.Add(new ShoppingListViewModel()
                {
                    Id = item.Id,
                    Members = item.ShoppingListMembers,
                    OwnerId = item.OwnerId,
                    OwnerName = (await _userManager.FindByIdAsync(item.OwnerId.ToString())).Name,
                    ListName = item.Name
                }) ;
            }

            return result;
        }

        public async Task<bool> IsListAvialable(int? listId)
        {
            var shoppingList = await _shoppingListRepository.GetByIdAsync(listId??0);
            if(shoppingList != null)
                return shoppingList.ListStatu == ListStatu.Available ? true : false;
            return true;
        }
    }
}
