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
        public List<ShoppingListItem> Items { get; set; } = new();
        public List<ShoppingListMember> Members { get; set; } = new();
        public int TotalItemsCount => Items.Count;
        public int TotalMembersCount => Members.Count;
    }
}
