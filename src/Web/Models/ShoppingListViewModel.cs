using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{
    public class ShoppingListViewModel
    {
        public int Id { get; set; }
        public string ListName { get; set; }
        public string OwnerId { get; set; }
        public string OwnerName { get; set; }
        public bool IsAuthToDelete { get; set; }
        public bool IsFreeToShopping { get; internal set; }
        public List<ShoppingListItem> Items { get; set; } 
        public List<ShoppingListMember> Members { get; set; }
        public int TotalItemsCount => Items.Count;
        public int TotalMembersCount => Members.Count() + 1;

    }
}
