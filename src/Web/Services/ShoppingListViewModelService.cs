using ApplicationCore.Entities;
using ApplicationCore.Enums;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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
        private readonly IListService _listService;

        public ShoppingListViewModelService(
            IRepository<ShoppingList> shoppingListRepository,
            IHttpContextAccessor httpContext,
            IRepository<ShoppingListMember> shoppingListMemberRepository,
            UserManager<ApplicationUser> userManager, IListService listService)
        {
            _shoppingListRepository = shoppingListRepository;
            _httpContext = httpContext;
            _shoppingListMemberRepository = shoppingListMemberRepository;
            _userManager = userManager;
            _listService = listService;
        }

        public async Task<AddMemberViewModel> AddMemberAsync(int listId, string memberId)
        {
            if (memberId != "0")
            {
                await _listService.AddMemberToShopingListAsync(memberId, listId);
            }
            var addMemberVm = await GetMemberViewModelAsync(listId);
            return addMemberVm;
        }

        public async Task<ShoppingListViewModel> AddNewList(string listName)
        {
            string loggedUserId = _httpContext.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var shopList = await _listService.AddNewShoppingListAsync(listName, loggedUserId);
            return new()
            {
                Id = shopList.Id,
                ListName = shopList.Name,
                OwnerId = shopList.OwnerId,
                OwnerName = (await _userManager.FindByIdAsync(loggedUserId)).Name,
                Items = new(),
                Members = new(),
                IsAuthToDelete = true,
                IsFreeToShopping = true
            };
        }

        public async Task<AddMemberViewModel> GetMemberViewModelAsync(int listId)
        {
            var memberList = await _shoppingListMemberRepository.GetAllAsync(new ShoppingListMemberSpecification(listId));
            var ownerId = (await _shoppingListRepository.GetByIdAsync(listId)).OwnerId;

            List<string> memberIds = memberList.Select(x => x.UserId).ToList();

            Dictionary<string, string> nonMembers = new();

            List<string> allMemberIds = await _userManager.Users.Select(x => x.Id).ToListAsync();

            foreach (var item in allMemberIds)
            {
                if (!memberIds.Contains(item) && item != ownerId)
                {
                    string userName = (await _userManager.FindByIdAsync(item)).UserName;
                    nonMembers.Add(item, userName);
                }
            }
            Dictionary<string, string> members = new();
            foreach (var item in memberIds)
            {
                string userName = (await _userManager.FindByIdAsync(item)).UserName;
                members.Add(item, userName);
            }

            return new()
            {
                ListId = listId,
                NonMembers = nonMembers.Select(x => new SelectListItem() { Text = x.Value, Value = x.Key }).ToList(),
                Members = members.Select(x => new Member() { Name = x.Value }).ToList()
            };
        }

        public async Task<List<ShoppingListViewModel>> GetShoppingListViewModelAsync()
        {
            string userId = _httpContext.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var shoppingListWithMembers = await _shoppingListMemberRepository.GetAllAsync(new ShoppingListMemberSpecification(userId));
            List<int?> memberIds = shoppingListWithMembers.Select(x => x.ShoppingListId).ToList();

            var shoppingList = await _shoppingListRepository.GetAllAsync(new ShoppingListFilterSpecification(userId, memberIds));

            var result = new List<ShoppingListViewModel>();
            foreach (var item in shoppingList)
            {
                result.Add(new ShoppingListViewModel()
                {
                    Id = item.Id,
                    Items = item.ShoppingListItems,
                    Members = item.ShoppingListMembers,
                    OwnerId = item.OwnerId,
                    OwnerName = (await _userManager.FindByIdAsync(item.OwnerId.ToString())).Name,
                    ListName = item.Name,
                    IsFreeToShopping = item.ShopperId == null ? true : false,
                    IsAuthToDelete = item.OwnerId == userId ? true : false
                });
            }

            return result;
        }

        public async Task RemoveList(int listId)
        {
            var list = await _shoppingListRepository.GetByIdAsync(listId);
            await _shoppingListRepository.DeleteAsync(list);
        }
    }
}
