using ApplicationCore.Entities;
using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Specifications
{
    public class ShoppingListFilterSpecification : Specification<ShoppingList>
    {
        public ShoppingListFilterSpecification(string ownerId, List<int?> memberIds)
        {
            Query.Where(x => x.OwnerId == ownerId || memberIds.Contains(x.Id)).Include(x => x.ShoppingListItems).Include(x=>x.ShoppingListMembers);
        }
        public ShoppingListFilterSpecification(int? listId)
        {
            Query.Where(x => x.Id == listId);
        }
    }
}
