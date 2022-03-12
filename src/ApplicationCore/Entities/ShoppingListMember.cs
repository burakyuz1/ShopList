using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities
{
    public class ShoppingListMember : BaseEntity
    {
        public ShoppingList ShoppingList { get; set; }
        public int? ShoppingListId { get; set; }
        public string UserId { get; set; }
    }
}
