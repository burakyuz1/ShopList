using ApplicationCore.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities
{
    public class ShoppingList : BaseEntity
    {
        public string Name { get; set; }
        public string OwnerId { get; set; }
        public string ShopperId { get; set; }
        public List<ShoppingListItem> ShoppingListItems { get; set; } = new List<ShoppingListItem>();
        public List<ShoppingListMember> ShoppingListMembers { get; set; } = new List<ShoppingListMember>();
    }
}
