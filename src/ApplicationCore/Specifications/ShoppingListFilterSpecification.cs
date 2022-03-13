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
        public ShoppingListFilterSpecification(string ownerId, List<int?> ids)
        {
            Query.Where(x => x.OwnerId == ownerId || ids.Contains(x.Id));
        }
    }
}
